using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;



public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    Dictionary<string, string> Levels = new Dictionary<string, string>(){
      {"Default", "DemoRoom1"},
      {"lijo", "mec"},
    };

    // Start is called before the first frame update
    public GameObject dropdown;
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
        string mapSelected = getDropDownItem(dropdown);
        Debug.Log("Loading: " + mapSelected);
        PhotonNetwork.LoadLevel(mapSelected); 
    }

    private string getDropDownItem(GameObject dropdownMenu)
    {
        int menuIndex = dropdownMenu.GetComponent<TMP_Dropdown>().value;
        List<TMP_Dropdown.OptionData> menuOptions = dropdownMenu.GetComponent<TMP_Dropdown>().options;
        string item = menuOptions[menuIndex].text;
        return item;
    }

    // Update is called once per frame
}
