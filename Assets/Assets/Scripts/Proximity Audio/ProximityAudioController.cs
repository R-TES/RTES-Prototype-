using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Scripts; // Agora


public class ProximityAudioController : MonoBehaviour
{
    private string[] blockedUsers;
    public GameObject player;
    private Collider2D selfCollider;


    void Start()
    {
        // If Online or this gameobject isn't attached to local player, destroy.
        if (PhotonNetwork.IsConnected && !player.GetComponent<PhotonView>().IsMine)
            Destroy(gameObject);

        selfCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            string playerID = collision.gameObject.GetComponent<PhotonView>().Owner.NickName;
            if (playerID == PhotonNetwork.LocalPlayer.NickName) return; // Probably can remove this. Too lazy to check if it'll break.
            Debug.Log("UNITY DEBUG LOG:\n A user has ENTERED your proximity: " + playerID);
            SubscribeToPlayerID(playerID);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            string playerID = GetPhotonIDFromCollider2D(other);
            if (playerID == PhotonNetwork.LocalPlayer.NickName) return;

            Debug.Log("UNITY DEBUG LOG:\n A user has EXITED your proximity: " + playerID);

            UnSubscribeToPlayerID(playerID);
        }
    }



    public void SubscribeToPlayerID(string playerID)
    {
        if (playerID != null)
        {
            Agora.Subscribe(playerID);
        }
    }
    public void UnSubscribeToPlayerID(string playerID)
    {
        if (playerID != null)
        {
            Agora.Unsubscribe(playerID);
        }
    }

    public string GetPhotonIDFromCollider2D(Collider2D col)
    {
        string playerID = col.gameObject.GetComponent<PhotonView>().Owner.NickName;
        return playerID; 
    }


    public IEnumerator ToggleProximityAudio(bool isEnabled)
    {
        Vector3 tempPos = transform.position;
        transform.position = new Vector3(1000000, 100000);      // To Trigger OnExit for all nearby players.\
        selfCollider.enabled = isEnabled;
        yield return new WaitForSeconds(0.001f);
        transform.position = tempPos;
    }
}


