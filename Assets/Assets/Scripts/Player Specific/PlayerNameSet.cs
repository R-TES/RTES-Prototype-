using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class PlayerNameSet : MonoBehaviour
{
    void Start()
    {
        SetPlayerNames(); 
    }

    void SetPlayerNames()
    {
        if (!PhotonNetwork.IsConnected) return;
        // Sets Player Name for all characters in the room. 
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
        foreach (var ch in characters)
        {
            GameObject playernameText = ch.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
            // Oh god. Not proud of this one.

            PhotonView pv = ch.GetComponent<PhotonView>();
            playernameText.GetComponent<TMP_Text>().text =  pv.Owner.NickName; 

        }

    }
}

