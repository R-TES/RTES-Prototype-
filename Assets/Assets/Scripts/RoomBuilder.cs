using System.Runtime.Serialization;
using System;
using System.Diagnostics;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomBuilder : MonoBehaviourPunCallbacks {
    private string roomId;
    private Room room;

    public void Start(){
        roomId = PhotonNetwork.CurrentRoom.Name;
        UnityEngine.Debug.Log(roomId);
        getRoom();
    }

    private void getRoom(){
        UnityEngine.Debug.Log("RoomBuilder: roomId " + roomId);
        FirestoreHandler.GetDocument("Rooms", "tSSPMUrsoiU6lYPGDkme", gameObject.name, "SetRoom", "DisplayErrorObject");
    }

    public void SetRoom(string data){
        room = Serializer<Room>.toObject(data);
        UnityEngine.Debug.Log("room set: " + room.id);
        spawnRoomObjects();
    }

    public void DisplayErrorObject(string error){
        UnityEngine.Debug.Log("firestore: " + error);
    }

    private void spawnRoomObjects(){
    
        foreach(RoomObjectPosition obj in room.roomObjects){
            var roomObject = Resources.Load(obj.name) as GameObject;
            var pos = new Vector2(obj.xVal, obj.xVal);
            PhotonNetwork.InstantiateRoomObject(obj.name, pos, Quaternion.identity);
        }
    }
}