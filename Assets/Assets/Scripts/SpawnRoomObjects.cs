using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Scripts {
    public class SpawnRoomObjects : MonoBehaviour
    {
        private void spawn(){
            var fetchedRoom = Storage.room; 
            foreach(RoomObjectPosition obj in fetchedRoom.roomObjects){
                var roomObject = Resources.Load(obj.name) as GameObject;
                var pos = new Vector2(obj.xVal, obj.xVal);
                PhotonNetwork.InstantiateRoomObject(obj.name, pos, Quaternion.identity);
            }
        }
        private async void Start(){
            if (PhotonNetwork.IsMasterClient) { spawn(); }
        }
    }
}

