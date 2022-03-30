using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateOrJoinFirestoreRoom : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update 

    public void CreateRoom() {
        PhotonNetwork.CreateRoom("FirestoreRoom");
    }

    public void JoinRoom(){
        PhotonNetwork.JoinRoom("FirestoreRoom");
    }

    public override void OnJoinedRoom(){
        PhotonNetwork.LoadLevel("Template2"); 
    }
}

