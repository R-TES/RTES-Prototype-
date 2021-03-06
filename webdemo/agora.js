var client = AgoraRTC.createClient({mode: "rtc", codec: "vp8"});

// RTM Global Vars
var isLoggedIn = false;

var localTracks = {
    videoTrack: null,
    audioTrack: null
};

var remoteUsers = {};
var subscribedRemoteUsers = {};
// Agora client options
var options = {
    appid: "e84579e080a145ec9fc8312297fa075f",
    channel: null,
    token: null,
    uid: null,
    accountName: null
};

function init (channel, userId, name="")
{
    console.log( "init called" );
    options.channel = channel;
    options.uid = userId;
    console.log("userid in options: " + options.uid)
    console.log( channel + " setChannel: " + options.channel);
    callJoin();
}

async function callJoin(){
    $("#join").attr("disabled", true);
    try
    {
        console.log( "call join: " + options.uid );
        await join();
    } catch (error) {
        console.error(error);
    } finally {
        $("#leave").attr("disabled", false);
    }
}

$("#leave").click(function (e) {
    leave();
})

async function join() {
    $("#mic-btn").prop("disabled", false);
    $("#video-btn").prop("disabled", false);
    $("#near").prop("disabled", false);
    $("#far").prop("disabled", false);
    // RTMJoin();
    // Event Listener to play remote streams as and when published
    client.on("user-published", handleUserPublished);
    client.on("user-left", handleUserLeft);
    // Join a channel and create local tracks
    // Promise.all to runs join and create funcn's concurrently
    
    [options.uid, localTracks.audioTrack, localTracks.videoTrack] = await Promise.all([
        // join the channel
        client.join(options.appid, options.channel, options.token, options.uid),
        // create local tracks, using microphone and camera
        AgoraRTC.createMicrophoneAudioTrack(
            {AEC: true, ANS: true} // to suppress echo.
        ),
        AgoraRTC.createCameraVideoTrack()
    ]);
    // Plays Local video track
    localTracks.videoTrack.play("localplayer");
    $("#local-player-name").text(`localVideo(${
        options.accountName
    })`);
    // Publishes Localtracks so that other user's can subscribe
    await client.publish(Object.values(localTracks));
    console.log("publish success");
}
async function leave() {
    for (trackName in localTracks) {
        var track = localTracks[trackName];
        if (track) {
            track.stop();
            track.close();
            $('#mic-btn').prop('disabled', true);
            $('#video-btn').prop('disabled', true);
            localTracks[trackName] = undefined;
        }
    }
    // To remove remote-users and their player view's
    remoteUsers = {};
    $("#remote-playerlist").html("");
    // leave the channel
    await client.leave();
    $("#local-player-name").text("");
    $("#join").attr("disabled", false);
    $("#leave").attr("disabled", true);
    console.log("client leaves channel success");
}
// Agora RTM Client
async function RTMJoin() { 
    const clientRTM = AgoraRTM.createInstance(options.appid, {enableLogUpload: false});
    var accountName = options.accountName;
    // Login
    clientRTM.login({uid: accountName, token: options.token}).then(() => {
        console.log('AgoraRTM client login success. Username: ' + accountName);
        isLoggedIn = true;
        // RTM Channel Join
        var channelName = options.channel;
        channel = clientRTM.createChannel(channelName);
        channel.join().then(() => {
            console.log('AgoraRTM client channel join success.');
            // Get all members in RTM Channel
            channel.getMembers().then((memberNames) => {
                console.log("------------------------------");
                console.log("All members in the channel are as follows: ");
                console.log(memberNames);
                var newHTML = $.map(memberNames, function (singleMember) {
                    if (singleMember != accountName) {
                        return(`<li class="mt-2">
                  <div class="row">
                      <p>${singleMember}</p>
                   </div>
                   <div class="mb-4">
                     <button class="text-white btn btn-control mx-3 remoteMicrophone micOn" id="remoteAudio-${singleMember}">Toggle Mic</button>
                     <button class="text-white btn btn-control remoteCamera camOn" id="remoteVideo-${singleMember}">Toggle Video</button>
                    </div>
                 </li>`);
                    }
                });
                $("#insert-all-users").html(newHTML.join(""));
            });
            // Send peer-to-peer message for audio muting and unmuting
            $(document).on('click', '.remoteMicrophone', function () {
                fullDivId = $(this).attr('id');
                peerId = fullDivId.substring(fullDivId.indexOf("-") + 1);
                console.log("Remote microphone button pressed.");
                let peerMessage = "audio";
                clientRTM.sendMessageToPeer({
                    text: peerMessage
                }, peerId,).then(sendResult => {
                    if (sendResult.hasPeerReceived) {
                        console.log("Message has been received by: " + peerId + " Message: " + peerMessage);
                    } else {
                        console.log("Message sent to: " + peerId + " Message: " + peerMessage);
                    }
                })
            });
            // Send peer-to-peer message for video muting and unmuting
            $(document).on('click', '.remoteCamera', function () {
                fullDivId = $(this).attr('id');
                peerId = fullDivId.substring(fullDivId.indexOf("-") + 1);
                console.log("Remote video button pressed.");
                let peerMessage = "video";
                clientRTM.sendMessageToPeer({
                    text: peerMessage
                }, peerId,).then(sendResult => {
                    if (sendResult.hasPeerReceived) {
                        console.log("Message has been received by: " + peerId + " Message: " + peerMessage);
                    } else {
                        console.log("Message sent to: " + peerId + " Message: " + peerMessage);
                    }
                })
            });
            // Display messages from peer
            clientRTM.on('MessageFromPeer', function ({
                text
            }, peerId) {
                console.log(peerId + " muted/unmuted your " + text);
                if (text == "audio") {
                    console.log("Remote video toggle reached with " + peerId);
                    if ($("#remoteAudio-" + peerId).hasClass('micOn')) {
                        localTracks.audioTrack.setEnabled(false);
                        console.log("Remote Audio Muted for: " + peerId);
                        $("#remoteAudio-" + peerId).removeClass('micOn');
                    } else {
                        localTracks.audioTrack.setEnabled(true);
                        console.log("Remote Audio Unmuted for: " + peerId);
                        $("#remoteAudio-" + peerId).addClass('micOn');
                    }
                } else if (text == "video") {
                    console.log("Remote video toggle reached with " + peerId);
                    if ($("#remoteVideo-" + peerId).hasClass('camOn')) {
                        localTracks.videoTrack.setEnabled(false);
                        console.log("Remote Video Muted for: " + peerId);
                        $("#remoteVideo-" + peerId).removeClass('camOn');
                    } else {
                        localTracks.videoTrack.setEnabled(true);
                        console.log("Remote Video Unmuted for: " + peerId);
                        $("#remoteVideo-" + peerId).addClass('camOn');
                    }
                }
            })
            // Display channel member joined updated users
            channel.on('MemberJoined', function () { // Get all members in RTM Channel
                channel.getMembers().then((memberNames) => {
                    console.log("New member joined so updated list is: ");
                    console.log(memberNames);
                    var newHTML = $.map(memberNames, function (singleMember) {
                        if (singleMember != accountName) {
                            return(`<li class="mt-2">
                      <div class="row">
                          <p>${singleMember}</p>
                       </div>
                       <div class="mb-4">
                         <button class="text-white btn btn-control mx-3 remoteMicrophone micOn" id="remoteAudio-${singleMember}">Toggle Mic</button>
                         <button class="text-white btn btn-control remoteCamera camOn" id="remoteVideo-${singleMember}">Toggle Video</button>
                        </div>
                     </li>`);
                        }
                    });
                    $("#insert-all-users").html(newHTML.join(""));
                });
            })
            // Display channel member left updated users
            channel.on('MemberLeft', function () { // Get all members in RTM Channel
                channel.getMembers().then((memberNames) => {
                    console.log("A member left so updated list is: ");
                    console.log(memberNames);
                    var newHTML = $.map(memberNames, function (singleMember) {
                        if (singleMember != accountName) {
                            return(`<li class="mt-2">
                      <div class="row">
                          <p>${singleMember}</p>
                       </div>
                       <div class="mb-4">
                         <button class="text-white btn btn-control mx-3 remoteMicrophone micOn" id="remoteAudio-${singleMember}">Toggle Mic</button>
                         <button class="text-white btn btn-control remoteCamera camOn" id="remoteVideo-${singleMember}">Toggle Video</button>
                        </div>
                     </li>`);
                        }
                    });
                    $("#insert-all-users").html(newHTML.join(""));
                });
            });
        }).catch(error => {
            console.log('AgoraRTM client channel join failed: ', error);
        }).catch(err => {
            console.log('AgoraRTM client login failure: ', err);
        });
    });
    // Logout
    document.getElementById("leave").onclick = async function () {
        console.log("Client logged out of RTM.");
        await clientRTM.logout();
    }
}

async function subscribe(user,mediaType) {
    const uid = user.uid;
    const remName = user.accountName;
    //const remName = "";
    // subscribe to a remote user
    if (mediaType == "AV"){
    await client.subscribe(user, "video");
    await client.subscribe(user, "audio");
    const videoTrack = user.videoTrack;
    const audioTrack = user.audioTrack;
    console.log("subscribe success");
        const player = $(`
      <div id="player-wrapper-${uid}">
        <p class="player-name"><span style="position: relative; font-size: 12px; z-index: 10;"> ${uid} </span></p>
        <div id="player-${uid}" class="player"></div>
      </div>
    `);
        $("#remote-playerlist").append(player);
        videoTrack.play(`player-${uid}`);
        audioTrack.play();
    }

    // if (mediaType == "video"){
    //     await client.subscribe(user, "video");
    //     const videoTrack = user.videoTrack;
    //     console.log("subscribe success");
    //     const player = $(`
    //   <div id="player-wrapper-${uid}">
    //     <p class="player-name">remoteUser(${remName})</p>
    //     <div id="player-${uid}" class="player"></div>
    //   </div>
    // `);
    //     $("#remote-playerlist").append(player);
    //     videoTrack.play(`player-${uid}`);
    // }

    // if (mediaType == "audio"){
    //     await client.subscribe(user, "audio");
    //     const audioTrack = user.audioTrack;
    //     audioTrack.play();

    // }
}

async function unsubscribe(user){
    remoteVanish(user);
    await client.unsubscribe(user);
    console.log('unsubscribe successs');

}

// Handle user publish
function handleUserPublished(user) {
    const id = user.uid;
    remoteUsers[id] = user;
    // console.log('***************')
    // console.log(remoteUsers)
    // console.log('****************')
    //subscribe(user, mediaType);
}

// function dummy(user){
//     const id = user.id;
//     remoteUsers[id] = user;
//     subscribe(user,"video");
// }

// Handle user left
function handleUserLeft(user) {
    const id = user.uid;
    delete remoteUsers[id];
    $(`#player-wrapper-${id}`).remove();
}

function remoteVanish(user) {
    const id = user.uid;
    //delete remoteUsers[id];
    $(`#player-wrapper-${id}`).remove();
}
// Initialise UI controls
enableUiControls();

// Action buttons
function enableUiControls() {
    // $("#mic-btn").click(function () {
    //     toggleMic();
    // });
    // $("#video-btn").click(function () {
    //     toggleVideo();
    // });
    // $("#near").click(function () {
    //     subscribeWhenNear();
    // });
    // $("#far").click(function () {
    //     unsubscribeWhenFar();
    // });

}

// Toggle Mic
async function toggleMic() {
    if ($("#mic-icon").hasClass('fa-microphone')) {
        localTracks.audioTrack.setVolume(0);
        console.log("Audio Muted.");
    } else {
        localTracks.audioTrack.setVolume(100);
        
        // console.log(remoteUsers)
        // console.log(localTracks)
        //console.log(client.remoteUsers)
        // for (const rmUser in subscribedRemoteUsers){
        //     console.log("SENT NEW AUDIO SUB");
        //     subscribe(subscribedRemoteUsers[rmUser],"audio");
        // }
        console.log('Audio Unmuted');
    }
    $("#mic-icon").toggleClass('fa-microphone').toggleClass('fa-microphone-slash');
}

// Toggle Video
async function toggleVideo() {
    if ($("#video-icon").hasClass('fa-video')) {
        localTracks.videoTrack.setEnabled(false);
        console.log("Video Muted.");
    } else {
        localTracks.videoTrack.setEnabled(true);
        console.log("Video Unmuted.");
    }
    $("#video-icon").toggleClass('fa-video').toggleClass('fa-video-slash');
}

function subscribeWhenNear(uid){
    console.log("SWN " + uid);
   // console.log(client.remoteUsers)
    //console.log(remoteUsers)
    // ids = Object.keys(remoteUsers);
    // userAdded = remoteUsers[ids[ids.length * Math.random() << 0]];
    // subscribedRemoteUsers[userAdded.uid] = userAdded;
    // console.log(subscribedRemoteUsers);
    console.log( "id fetched: " + uid + ", logging remoteUser[uid] from subscribewhennear: " + remoteUsers[uid] );
    
    userAdded = remoteUsers[uid];
    if(!userAdded){
        console.log("AGORA DEBUG LOG:\nFUNCTION: subscribeWhenNear \nUSER UNDEFINED FOR ID: " + uid); 
        return; 
    }              // Sandy: Added Null Guards.
    console.log(userAdded); 
    subscribe(userAdded,"AV");
}

function unsubscribeWhenFar(uid){
    console.log("USWF");
    console.log( "id fetched: " + uid + ", logging remoteUser[uid] from unsubscribeWhenFar: " + remoteUsers[uid] );
    // ids = Object.keys(remoteUsers);
    // userKickedOut = remoteUsers[ids[ids.length * Math.random() << 0]];
    userKickedOut = remoteUsers[uid];
    if(!userKickedOut){ 
        console.log("AGORA DEBUG LOG:\nFUNCTION: unsubscribeWhenFar \nUSER UNDEFINED FOR ID: " + uid); 
        return;          // Sandy: Added Null Guards.
    }
    //console.log(userKickedOut)
    delete subscribedRemoteUsers[userKickedOut.uid];
    unsubscribe(userKickedOut);


    //client.muteRemoteVideoStream(userKickedOut, true);
}