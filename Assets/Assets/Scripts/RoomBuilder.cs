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
    public RoomOptions roomOptions = new RoomOptions();
    public GameObject button;
    public Transform panel;
    public Room room;

    public void createButtons(List<Room> rooms, Action<string> action){
        foreach(Room room in rooms){
            GameObject roomButton = Instantiate(button);
            roomButton.transform.SetParent(panel);
            roomButton.GetComponentInChildren<Text>().text = room.name;
            roomButton.GetComponent<Button>().onClick.AddListener(delegate { 
                action(room.id);});
        }
        
    }

    public void build(Room room){
        this.room = room;
        PhotonNetwork.JoinOrCreateRoom(room.id, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom(){
        UnityEngine.Debug.Log(room.template);
        PhotonNetwork.LoadLevel(room.template); 
        if (PhotonNetwork.IsMasterClient){
            spawnRoomObjects();
        }
        
    }

    private void spawnRoomObjects(){
        foreach(RoomObjectPosition obj in room.roomObjects){
            var roomObject = Resources.Load(obj.name) as GameObject;
            var pos = new Vector2(obj.xVal, obj.xVal);
            PhotonNetwork.InstantiateRoomObject(obj.name, pos, Quaternion.identity);
        }
    }
}