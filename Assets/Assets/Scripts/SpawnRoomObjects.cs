using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Scripts {
    public class SpawnRoomObjects : MonoBehaviour
    {
        private async void Start(){
            var fetchedRoom = CreateOrJoinFirestoreRoom.room;
            foreach(RoomObjectPosition obj in fetchedRoom.roomObjects){
                var roomObject = Resources.Load(obj.name) as GameObject ;
                Vector2 pos = new Vector2(obj.xVal, obj.xVal);
                PhotonNetwork.Instantiate(obj.name, pos, Quaternion.identity);
            }
            
        }
    }
}

