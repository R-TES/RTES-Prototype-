using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;



public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject dropdown;
    public InputField createInput;
    public InputField joinInput;
    public InputField playerName; 

    public void CreateRoom()
    {
        if(!NameCheck()) return;


        RoomOptions roomOptions = SetRoomOptions();

        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom(){
        if(!NameCheck()) return;

        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom(){
        PhotonNetwork.LocalPlayer.NickName = playerName.text;
        string mapSelected = getDropDownMapSelection(dropdown);
        Debug.Log("Loading: " + mapSelected);
        PhotonNetwork.LoadLevel(mapSelected); 
    }

    
    private string getDropDownMapSelection(GameObject dropdownMenu)
    {
        int menuIndex = dropdownMenu.GetComponent<TMP_Dropdown>().value;
        List<TMP_Dropdown.OptionData> menuOptions = dropdownMenu.GetComponent<TMP_Dropdown>().options;
        string item = menuOptions[menuIndex].text;
        return item;
    }


    private RoomOptions SetRoomOptions()
    {
        ExitGames.Client.Photon.Hashtable room_details = new ExitGames.Client.Photon.Hashtable();
        room_details.Add("Ag", "AGORA_ID");
        room_details.Add("m", getDropDownMapSelection(dropdown));

        RoomOptions roomOptions = new RoomOptions
        {
            PlayerTtl = 0,
            CustomRoomProperties = room_details
        };


        return roomOptions;
    }
    private bool NameCheck()
    {
        if (playerName.text == "")
        {
            Debug.LogError("Please provide player name.");
            return false;
        }
        return true;
    }

    // Update is called once per frame
}
