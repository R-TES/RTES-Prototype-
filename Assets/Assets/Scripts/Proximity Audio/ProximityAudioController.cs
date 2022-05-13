using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Scripts; // Agora


public class ProximityAudioController : MonoBehaviour       // Henceforth called as "PAC"
{
    //private string[] blockedUsers;
    public GameObject player;
    private Collider2D selfCollider;
    public bool isAllowingSubscribers;

    void Start()
    {
        selfCollider = GetComponent<Collider2D>();
        isAllowingSubscribers = true;
        // If Online or this gameobject isn't attached to local player, destroy.
        if (PhotonNetwork.IsConnected && !player.GetComponent<PhotonView>().IsMine)
            selfCollider.enabled = false;


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAllowingSubscribers) return;         // If in disabled state, don't check for new users entering your space. 

        if (other.gameObject.CompareTag("Player"))
        {
            string playerID = GetPhotonIDFromCollider2D(other);         // Retrieve Other Players ID
            ProximityAudioController otherPlayersPAC = other.gameObject.GetComponentInChildren<ProximityAudioController>();     // Retrieve Other Player's PAC
            Debug.Log("UNITY DEBUG LOG:\n A user has ENTERED your proximity: " + playerID);
            
            if (otherPlayersPAC.isAllowingSubscribers)   // If collided user's PAC is in disabled state, ignore.
                SubscribeToPlayerID(playerID);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isAllowingSubscribers) return;     // If in disabled state, don't check for new users entering your space. 

        if (other.gameObject.CompareTag("Player"))
        {
            string playerID = GetPhotonIDFromCollider2D(other);
            Debug.Log("UNITY DEBUG LOG:\n A user has EXITED your proximity: " + playerID);
            if (other.gameObject.GetComponentInChildren<ProximityAudioController>().isAllowingSubscribers)
                UnSubscribeToPlayerID(playerID);
        }
    }



    public void SubscribeToPlayerID(string playerID)
    {
        if (playerID == PhotonNetwork.LocalPlayer.NickName) return; // Probably can remove this. Too lazy to check if it'll break.
        if (playerID != null)
        {
            Agora.Subscribe(playerID);
        }
        Debug.Log("Subscribed via (STPID)");
    }
    public void UnSubscribeToPlayerID(string playerID)
    {
        if (playerID == PhotonNetwork.LocalPlayer.NickName) return;
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


    public IEnumerator ToggleProximityAudio(bool isEnabled)         // Lazy Unsubscribe option.
    {
        Vector3 tempPos = transform.position;
        transform.position = new Vector3(1000000, 100000);      // To Trigger OnExit for all nearby players.
        yield return new WaitForSeconds(0.001f);
        selfCollider.enabled = isEnabled;
        GetComponent<SpriteRenderer>().enabled = isEnabled;
        yield return new WaitForSeconds(0.001f);
        transform.position = tempPos;
    }

    public void ToggleSubscribableState(bool isEnabled)
    {
        isAllowingSubscribers = isEnabled;
        StartCoroutine(ToggleProximityAudio(isEnabled));
        Debug.Log("You toggled your Subscriber State.");
    }
}


