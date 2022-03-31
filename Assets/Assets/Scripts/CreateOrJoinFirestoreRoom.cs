using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Scripts{
    public class CreateOrJoinFirestoreRoom : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update 

        // public static Room room = Serializer<Room>.toObject("{'roomObjects':['Tree Plant']}");
        public static Room room;
        public static string template = "Template2";
        public string roomId = "tSSPMUrsoiU6lYPGDkme";
        public void CreateRoom() {
            getRooms();
            PhotonNetwork.CreateRoom(roomId);
        }

        public void getRooms(){
            FirebaseFirestore.GetDocument("Rooms", roomId, gameObject.name, "SetRoom", "DisplayErrorObject");
        }

        public void SetRoom(string data){
            room = Serializer<Room>.toObject(data);
        }

        public void getTemplate(){
            FirebaseFirestore.GetDocument("Templates", room.template, gameObject.name, "SetTemplate", "DisplayErrorObject");
        }

        public void setTemplate(string data){
            var temp = Serializer<Template>.toObject(data);
            template = temp.scene;
        }
        public void DisplayErrorObject(string error){
            Debug.Log(error);
        }

        public void JoinRoom(){
            PhotonNetwork.JoinRoom(roomId);
        }

        public override void OnJoinedRoom(){
            PhotonNetwork.LoadLevel(template); 
        }
    }

}
