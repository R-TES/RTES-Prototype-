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
        if (!PhotonNetwork.IsMasterClient) return;
        roomId = PhotonNetwork.CurrentRoom.Name;
        UnityEngine.Debug.Log(roomId);
        getRoom();
    }

    private void getRoom(){
        UnityEngine.Debug.Log("RoomBuilder: roomId " + roomId);
        FirestoreHandler.GetDocument("Rooms", roomId, gameObject.name, "SetRoom", "DisplayErrorObject");
    }

    public void SetRoom(string data){
        UnityEngine.Debug.Log("unity response: " + data);
        room = Serializer<Room>.toObject(data);
        UnityEngine.Debug.Log("room set: " + room.name);
        UnityEngine.Debug.Log("room set template: " + room.roomObjects.Count);
        spawnRoomObjects();
    }

    public void DisplayErrorObject(string error){
        UnityEngine.Debug.Log("firestore: " + error);
    }

    private void spawnRoomObjects(){
        UnityEngine.Debug.Log("Unity Debug: STARTING INSTANTIATE LOOP");
        foreach (RoomObjectPosition obj in room.roomObjects){
            UnityEngine.Debug.Log("Unity Debug: Instantiating Objects From Firebase: " + obj.name);
            GameObject roomObjectInstance = Resources.Load(obj.name) as GameObject;
            var pos = new Vector2(obj.xVal, obj.yVal);
            GameObject g = PhotonNetwork.Instantiate(roomObjectInstance.name, pos, Quaternion.identity);

            UnityEngine.Debug.Log("Created gameobject: " + g.name + "Position = " + g.transform.position.x + " : " + g.transform.position.y);

        }
    }
}