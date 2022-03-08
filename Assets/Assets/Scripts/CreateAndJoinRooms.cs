using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;




public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    Dictionary<string, string> Levels = new Dictionary<string, string>(){
      {"Default", "DemoRoom1"},
      {"Space", "SpaceRoom1"},
    };

    // Start is called before the first frame update
    public InputField createInput;
    public InputField joinInput;
    public InputField playerName; 

    public void CreateRoom(){
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom(){
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom(){
        PhotonNetwork.LocalPlayer.NickName = playerName.text;
        string map = Levels["Default"]; 
        PhotonNetwork.LoadLevel(map); 
    }

    // Update is called once per frame
}
