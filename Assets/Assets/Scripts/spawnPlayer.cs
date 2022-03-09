using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using UnityEngine.UI;

public class spawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject onlinePlayer;
    public GameObject offlineDebugPlayer;

    private PhotonView view;
    public int xSpawnPosition = 0;
    public int ySpawnPosition = 0;

    void Start()
    {
        Vector2 pos = new Vector2(xSpawnPosition, ySpawnPosition);
        view = GetComponent<PhotonView>();

        //PlayerPopulate();
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Player Created in Photon Room.");
            PhotonNetwork.Instantiate(onlinePlayer.name, pos, Quaternion.identity);
        }
        else
        {
            Instantiate(offlineDebugPlayer, pos, offlineDebugPlayer.transform.rotation);
            Debug.LogError("Not Connected To Photon! Testing Purposes only guys."); 
        }
        
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
