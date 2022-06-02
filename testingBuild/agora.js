var client = AgoraRTC.createClient({mode: "rtc", codec: "vp8"});

// Flags for Cam and Mic
var isCamera = true
var isMic = true

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
    $("#screen-share-btn").prop("disabled", false);
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
            $('#screen-share-btn').prop('disabled', true);
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

async function screenJoin(){
    client.on("user-published", handleUserPublished);
    client.on("user-left", handleUserLeft);
    //localTracks.videoTrack.setEnabled(false);
    let screenTrack;
    await client.unpublish(localTracks.videoTrack)
    localTracks.videoTrack.stop()
    localTracks.videoTrack.close()

    screenTrack = await AgoraRTC.createScreenVideoTrack({
        encoderConfig: {
          framerate: 15,
          height: 640,
          width: 480
        }
        //extensionId: 'minllpmhdgpndnkomcoccfekfegnlikg',
      }, "auto")
    
    if(screenTrack instanceof Array){
        localTracks.videoTrack = screenTrack[0];
      }
    else{
        localTracks.videoTrack = screenTrack;
      }
    console.log("^^^^^^^^^^^^^^^^^^^^^")
    console.log("Attempted to play track")
    console.log(remoteUsers)

    localTracks.videoTrack.play("localplayer");
    $("#local-player-name").text(`localVideo(${
        options.accountName
    })`);

    

    localTracks.videoTrack.on("track-ended", () => {
        alert(`Screen-share track ended, stop sharing screen ` + localTracks.videoTrack.getTrackId());
        localTracks.videoTrack && localTracks.videoTrack.close();
        afterScreenShareStops();

        
        //localScreenTracks.screenAudioTrack && localScreenTracks.screenAudioTrack.close();
        //localTracks.audioTrack && localTracks.audioTrack.close();
      });
    
      
      await client.publish([localTracks.videoTrack]);
      console.log("<><><><><><><><><><><><>");
      console.log("publish success");
}

async function afterScreenShareStops(){
    await client.unpublish(localTracks.videoTrack)
    localTracks.videoTrack.stop()
    localTracks.videoTrack.close()
    localTracks.videoTrack = await AgoraRTC.createCameraVideoTrack();
    localTracks.videoTrack.play("localplayer");
    $("#local-player-name").text(`localVideo(${
    options.accountName
    })`);
    await client.publish([localTracks.videoTrack]);
    console.log("Camera re-published after screen share");
}


async function subscribe(user,mediaType) {
    const uid = user.uid;
    const remName = user.accountName;
    //const remName = "";
    // subscribe to a remote user
    if (mediaType == "AV"){
    await client.subscribe(user, "video");
    await client.subscribe(user, "audio");
    subscribedRemoteUsers[uid] = user;
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
    $("#screen-share-btn").click(function () {
        toggleScreen();
    });

}

// Toggle Mic
async function toggleMic() {
    if (isMic == true){
        isMic = false;
        console.log(isMic)
        await localTracks.audioTrack.setEnabled(false);
        console.log("<><> Audio Muted <><>");
    }
    else if (isMic == false){
        isMic = true;
        await localTracks.audioTrack.setEnabled(true);
        console.log("<><> Audio Unmuted <><>");
    }
}

// Toggle Video
async function toggleVideo() {
    if (isCamera == true){
        isCamera = false;
        console.log(isCamera)
        await localTracks.videoTrack.setEnabled(false);
        console.log("<><> Video Muted <><>");
    }
    else if (isCamera == false){
        isCamera = true;
        await localTracks.videoTrack.setEnabled(true);
        console.log("<><> Video Unmuted <><>");
    }
}

async function toggleScreen(){
    await screenJoin();
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
    if(!(uid in subscribedRemoteUsers)){
    console.log(userAdded); 
    subscribe(userAdded,"AV");
    }
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
    console.log("DEBUG DEBUG DEBUG WHO WANTS THEIR DEBUG??" + subscribedRemoteUsers);
    unsubscribe(userKickedOut);


    //client.muteRemoteVideoStream(userKickedOut, true);
}