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
    public InputField roomInput;
    public InputField playerName;

    public void CreateRoom()
    {
        if(!NameCheck())return;
        RoomOptions roomOptions = SetRoomOptions();
        PhotonNetwork.CreateRoom(roomInput.text, roomOptions);
    }

    public void JoinRoom(){
        if(!NameCheck()) return;

        PhotonNetwork.JoinRoom(roomInput.text);
    }

    public void CreateOrJoinRoom()
    {
        if (!NameCheck()) return;
        RoomOptions roomOptions = SetRoomOptions();
        PhotonNetwork.JoinOrCreateRoom(roomInput.text, roomOptions, TypedLobby.Default); ;

    }

    public override void OnJoinedRoom(){

        PhotonNetwork.LocalPlayer.NickName = playerName.text;
        string mapSelected = GetDropDownMapSelection(mapDropDown);
        SetDropDownPlayerSelection(characterDropDown);

        ExitGames.Client.Photon.Hashtable customProperty = new() { { "info1", "1" } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProperty);
        Debug.Log("User ID: " + PhotonNetwork.LocalPlayer.UserId);

        try
        {
            Agora.Init(roomInput.text, PhotonNetwork.LocalPlayer.NickName);
        }
        catch(System.EntryPointNotFoundException e)
        {
            Debug.LogError(e.ToString());
        }

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
        if (playerName.text.Length < 2 && playerName.text.Length > 14)
        {
            Debug.LogError("Please provide player name of length greater than 2 and less than 15");
            return false;
        }
        return true;
    }

    // Update is called once per frame
}
