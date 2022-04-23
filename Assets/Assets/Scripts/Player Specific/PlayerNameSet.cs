using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using UnityEngine.UI; 
public class PlayerNameSet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playernamefield;

    void Start()
    {
        SetPlayerNames(); 
    }

    void SetPlayerNames()
    {
        // Sets Player Name for all characters in the room. 
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");
        foreach (var ch in characters)
        {
            GameObject playernameText = ch.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;

            PhotonView pv = ch.GetComponent<PhotonView>();
            playernameText.GetComponent<Text>().text =  pv.Owner.NickName; 

        }

    }
}

