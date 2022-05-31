using System.Diagnostics;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class CreateOrJoinFirestoreRoom : MonoBehaviourPunCallbacks
{
    public InputField agoraUserId;
    public Room room;
    public bool isUserSet = false;
    public bool isRoomSet = false;
    public RoomBuilder roomBuilder;
    public FirebaseFirestore firestore;
    public User user = new User();

    private void Start(){
        firestore.obj = this;
        if(GetUserInfo()){
            roomBuilder.createButtons(user.rooms, BuildRoom);
        }
    }

    public bool GetUserInfo(){
        firestore.getUserInfo(agoraUserId.text);
        while(!isUserSet) continue;
        if(user == null) return false;
        return true;
    }

    public void BuildRoom(string roomId){
        firestore.getRoomInfo(roomId);
        while(!isRoomSet) continue;
        room.id = roomId;
        roomBuilder.build(room);
    }
}


