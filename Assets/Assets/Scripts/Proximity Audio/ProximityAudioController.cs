using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Scripts; // Agora


public class ProximityAudioController : MonoBehaviour       // Henceforth called as "PAC"
{
    //private string[] blockedUsers;
    public GameObject player;
    private CircleCollider2D selfCollider;
    public bool isAllowingSubscribers;
    public LayerMask playerLayer;
    private static int scanCount = 0;

    void Start()
    {
        selfCollider = GetComponent<CircleCollider2D>();
        isAllowingSubscribers = true;
        // If Online or this gameobject isn't attached to local player, disable.
        if (PhotonNetwork.IsConnected)
        {
            if(!player.GetComponent<PhotonView>().IsMine) 
                selfCollider.enabled = false;                   // Remote User players.
            else 
                InvokeRepeating(nameof(PeriodicScan), 3f, 2f);  // Your player.
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAllowingSubscribers) return;         // If in disabled state, don't check for new users entering your space. 

        if (other.gameObject.CompareTag("Player"))
        {
            string playerID = GetPhotonIDFromCollider2D(other);         // Retrieve Other Players ID
            Debug.Log("UNITY DEBUG LOG:\n A user has ENTERED your proximity: " + playerID);

            if (IsAllowingSubscribers(other.gameObject))   // If collided user's PAC is in disabled state, ignore.
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
            if (IsAllowingSubscribers(other.gameObject))
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
        PhotonView ph = col.gameObject.GetComponent<PhotonView>();
        if(!ph.IsMine)
            return ph.Owner.NickName;
        return "";
    }


    public IEnumerator ToggleProximityAudio(bool isEnabled)         // Lazy Unsubscribe option.
    {
        Vector3 tempPos = transform.position;
        transform.position = new Vector3(1000000, 100000);      // To Trigger OnExit for all nearby players.
        yield return new WaitForSeconds(0.01f);
        selfCollider.enabled = isEnabled;
        GetComponent<SpriteRenderer>().enabled = isEnabled;
        yield return new WaitForSeconds(0.01f);
        transform.position = tempPos;
    }

    public void ToggleSubscribableState(bool isEnabled)
    {
        isAllowingSubscribers = isEnabled;
        StartCoroutine(ToggleProximityAudio(isEnabled));
        Debug.Log("You toggled your Subscriber State.");
    }


    public void PeriodicScan()
    {
        if (!isAllowingSubscribers) return; //Private Space Logic Guard. Coupling is bad dumbass.

        scanCount++;
        if (scanCount % 8 == 0) UnSubscribeToEveryMember(); // Just clean the bugs every 16 seconds.

        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, selfCollider.radius * 0.8f, playerLayer);
        Debug.Log(playerColliders.Length + " Players found");
        foreach (var user in playerColliders)
        {
            Debug.Log(user.gameObject.name);
            if (IsAllowingSubscribers(user.gameObject))
                SubscribeToPlayerID(GetPhotonIDFromCollider2D(user));
        }
    }


    private void UnSubscribeToEveryMember()       // Just clean up stuff every 15 seconds.
    {
        
        foreach (Photon.Realtime.Player p in PhotonNetwork.PlayerList)
            UnSubscribeToPlayerID(p.NickName);
    }

    private bool IsAllowingSubscribers(GameObject user)
    {
        return user.GetComponentInChildren<ProximityAudioController>().isAllowingSubscribers;
    }
}


