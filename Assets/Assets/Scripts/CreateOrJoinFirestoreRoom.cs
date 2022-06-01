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
    }

    public void getUserAndRooms(){
        firestore.getUserInfo(agoraUserId.text);
        UnityEngine.Debug.Log(agoraUserId.text);
        while(!isUserSet) continue;
        if(user != null) roomBuilder.createButtons(user.rooms, buildRoom);
    }

    public void buildRoom(string roomId){
        firestore.getRoomInfo(roomId);
        while(!isRoomSet) continue;
        room.id = roomId;
        roomBuilder.build(room);
    }
}


