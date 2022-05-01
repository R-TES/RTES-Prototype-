using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Scripts;   // Agora

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public TMP_Dropdown mapDropDown;
    public TMP_Dropdown characterDropDown;
    public InputField createInput;
    public InputField joinInput;
    public InputField playerName;

    [Header("Disable this when you're buildng!!")]
    public bool localTest = false;

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
        string mapSelected = GetDropDownMapSelection(mapDropDown);
        SetDropDownPlayerSelection(characterDropDown);

        ExitGames.Client.Photon.Hashtable customProperty = new() { { "info1", "1" } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperty);
        Debug.Log("User ID: " + PhotonNetwork.LocalPlayer.UserId);
        if(!localTest)
            Agora.Init(createInput.text, PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.LoadLevel(mapSelected); 
    }

    
    private string GetDropDownMapSelection(TMP_Dropdown dropdownMenu)
    {
        int menuIndex = dropdownMenu.value;
        List<TMP_Dropdown.OptionData> menuOptions = dropdownMenu.GetComponent<TMP_Dropdown>().options;
        string item = menuOptions[menuIndex].text;
        PlayerPrefs.SetString("map", item);
        return item;
    }

    private int SetDropDownPlayerSelection(TMP_Dropdown dropdownMenu)
    {
        int characterIndex = dropdownMenu.value;
        Debug.Log("Character Index" + characterIndex.ToString());

        PlayerPrefs.SetInt("PlayerCharacterPresetIndex", characterIndex);
        return characterIndex;
    }


    private RoomOptions SetRoomOptions()
    {
        ExitGames.Client.Photon.Hashtable room_details = new ExitGames.Client.Photon.Hashtable();
        room_details.Add("Ag", "AGORA_ID");
        room_details.Add("m", GetDropDownMapSelection(mapDropDown));

        RoomOptions roomOptions = new RoomOptions
        {
            PlayerTtl = 0,
            CustomRoomProperties = room_details
        };
        roomOptions.PublishUserId = true; 


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
