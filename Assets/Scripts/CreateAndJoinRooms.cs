using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public InputField createInput;
    public InputField joinInput;

    public void CreateRoom(){
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom(){
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom(){
        PhotonNetwork.LoadLevel("DemoGame"); 
    }

    // Update is called once per frame
}
