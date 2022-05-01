using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using UnityEngine.UI;

public class spawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] playerPrefabs; 
    public int character_index = -1;


    [Header("Custom Spawn Coordinates")]
    public bool useCustomSpawnCoordinates = false;
    public int xSpawnPositionOffset = 0;
    public int ySpawnPositionOffet = 0;
    
    void Start()
    {

        Vector2 pos = GetSpawnCoordinate();
        PhotonView view = GetComponent<PhotonView>();
        int character_index = GetPlayerCharacterIndex();

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Player Created in Photon Room.");
            PhotonNetwork.Instantiate(playerPrefabs[character_index].name, pos, Quaternion.identity);
        }
        else
        {
            Instantiate(playerPrefabs[character_index], pos, Quaternion.identity);
            Debug.LogError("Not Connected To Photon! Open game from Intro Scene!"); 
        }
        
    }


    int GetPlayerCharacterIndex()
    {
        if (character_index >= playerPrefabs.Length) {
            Debug.Log("Player Spawn Script's index is out of scope. Spawning Index 0 by default."); 
            return 0;
        };
        if (character_index == -1)   // PlayerPref mode.      
        {
            Debug.Log("Spawning from PlayerPrefs");
            return PlayerPrefs.GetInt("PlayerCharacterPresetIndex");
        }

        return character_index; 
    }


    private Vector2 GetSpawnCoordinate()
    {
        Vector2 pos;
        if (useCustomSpawnCoordinates)
            pos = new Vector2(xSpawnPositionOffset, ySpawnPositionOffet);
        else
            pos = transform.position;

        return pos;
    }
/*
    void PlayerPopulate() {
        string nickName = PhotonNetwork.LocalPlayer.NickName;
        PhotonNetwork.LocalPlayer.NickName = nickName;

        GameObject playerNameText = player.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        Debug.Log("GameObject: " + playerNameText.name);
        playerNameText.GetComponent<Text>().text = PhotonNetwork.LocalPlayer.NickName;
    }
*/
}
