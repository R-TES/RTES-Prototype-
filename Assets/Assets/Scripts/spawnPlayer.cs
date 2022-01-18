using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player; 

    void Start()
    {
        Vector2 pos = new Vector2(0, 0);
        PhotonNetwork.Instantiate(player.name, pos, Quaternion.identity);
    }

  

}
