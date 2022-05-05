using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Scripts; // Agora


public class ProximityAudioController : MonoBehaviour
{
    private string[] blockedUsers;
    public GameObject player; 


    void Start()
    {   // If Online or this gameobject isn't attached to local player, destroy.
        if (PhotonNetwork.IsConnected  && !player.GetComponent<PhotonView>().IsMine)
            Destroy(gameObject);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            string playerID = collision.gameObject.GetComponent<PhotonView>().Owner.NickName;
            if (playerID == PhotonNetwork.LocalPlayer.NickName) return; // Probably can remove this. Too lazy to check if it'll break.

            Debug.Log("UNITY DEBUG LOG:\n A user has ENTERED your proximity: " + playerID);
            if (playerID != null)
            {
                Agora.Subscribe(playerID);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            string playerID = collision.gameObject.GetComponent<PhotonView>().Owner.NickName;
            if (playerID == PhotonNetwork.LocalPlayer.NickName) return;

            Debug.Log("UNITY DEBUG LOG:\n A user has EXITED your proximity: " + playerID);
            if (playerID != null)
            {
                Agora.Unsubscribe(playerID);
            }
        }
    }


}


