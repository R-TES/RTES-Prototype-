using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Scripts {
    public class SpawnRoomObjects : MonoBehaviour
    {
        public static int x = 0;
        private void spawn(){
            var fetchedRoom = CreateOrJoinFirestoreRoom.room; 
            foreach(RoomObjectPosition obj in fetchedRoom.roomObjects){
                var roomObject = Resources.Load(obj.name) as GameObject ;
                var pos = new Vector2(obj.xVal + x, obj.xVal + x);
                x += 1;
                PhotonNetwork.InstantiateRoomObject(obj.name, pos, Quaternion.identity);
            }
        }
        private async void Start(){
            spawn();
        }
    }
}

