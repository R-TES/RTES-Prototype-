using System.Diagnostics;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

namespace Scripts{
    public class CreateOrJoinFirestoreRoom : MonoBehaviourPunCallbacks
    {
        public GameObject button;
        public InputField agoraUserId;
        public Transform panel;
        public RoomOptions roomOptions = new RoomOptions();
        public User user = Serializer<User>.toObject("{'id':'zd26igJS15tCTNchXaaQ', 'username':'aryajayadevkm','avatar':'IwZIvRqQlKXHj8hVMM76','mobile':'','rooms':[{'name':'Office 1','id':'tSSPMUrsoiU6lYPGDkme'},{'name':'Office 2','id':'vRVo1rLAtEU14Tzl5288'}],'designation':'','email':'aryajayadevkm@gmail.com'}");
        
        
        private void Start(){
            foreach(Room room in user.rooms){
                createRoomButton(room);
            }
        }

        private void createRoomButton(Room room){
            user.id = agoraUserId.text;
            GameObject roomButton = Instantiate(button);
            roomButton.transform.SetParent(panel);
            roomButton.GetComponentInChildren<Text>().text = room.name;
            roomButton.GetComponent<Button>().onClick.AddListener(delegate { JoinOrCreateRoom(room.id);});
        }

        public void JoinOrCreateRoom(string roomId) {
            Storage.room.id = roomId;
            getRoom();
            FirebaseFirestore.Init(roomId, agoraUserId.text);
            PhotonNetwork.JoinOrCreateRoom(roomId, roomOptions, TypedLobby.Default);
            UnityEngine.Debug.Log("unity roomId: " + roomId);
            
        }

        public void getRoom(){
            FirebaseFirestore.GetDocument("Rooms", Storage.room.id, gameObject.name, "SetRoom", "DisplayErrorObject");
        }

        public void SetRoom(string data){
            Storage.room = Serializer<Room>.toObject(data);
        }
        public void DisplayErrorObject(string error){
            UnityEngine.Debug.Log(error);
        }

        public override void OnJoinedRoom(){
            UnityEngine.Debug.Log("joining: " + Storage.room.id + " template: " + Storage.room.template);
            PhotonNetwork.LoadLevel(Storage.room.template); 
        }

    }

}
