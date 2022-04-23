using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using UnityEngine.UI;

public class spawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] offlinePlayers;
    public GameObject[] photonPlayers; 
    public int spawnPlayerIndex = 0;


    [Header("Custom Spawn Coordinates")]
    public bool useCustomSpawnCoordinates = false;
    public int xSpawnPosition = 0;
    public int ySpawnPosition = 0;
    
    void Start()
    {

        Vector2 pos = GetSpawnCoordinate();
        PhotonView view = GetComponent<PhotonView>();

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Player Created in Photon Room.");
            PhotonNetwork.Instantiate(photonPlayers[spawnPlayerIndex].name, pos, Quaternion.identity);
        }
        else
        {
            Instantiate(offlinePlayers[spawnPlayerIndex], pos, Quaternion.identity);
            Debug.LogError("Not Connected To Photon! Open game from Intro Scene!"); 
        }
        
    }



    private Vector2 GetSpawnCoordinate()
    {
        Vector2 pos;
        if (useCustomSpawnCoordinates)
            pos = new Vector2(xSpawnPosition, ySpawnPosition);
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
