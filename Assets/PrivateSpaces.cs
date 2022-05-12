using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PrivateSpaces : MonoBehaviour
{

    private PhotonView localPhotonView;
    private List<PhotonView> participants;


    private void Start()
    {
        participants = new List<PhotonView>();          // A list to store all users inside the room. Is synchronised everywhere using RPC calls.
        localPhotonView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PhotonView newParticipantPV = collision.gameObject.GetComponent<PhotonView>();
            if (newParticipantPV.IsMine)    // Only CALL stuff if you are the one walking into the room.
            {
                localPhotonView.RPC("AddParticipant", RpcTarget.AllBuffered, newParticipantPV.ViewID);                      // RPC Call
                localPhotonView.RPC("TogglePACOfNewUserEverywhere", RpcTarget.AllBuffered, newParticipantPV.ViewID, false); // RPC Call
                SubscribeToAllExistingParticipants();                                                                       // Local Call
                CallSubscribeForParticipants(newParticipantPV.Owner.NickName);                                              // RPC Call actually.
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
 
            PhotonView newParticipantPV = collision.gameObject.GetComponent<PhotonView>();
            if (newParticipantPV.IsMine)
            {
                localPhotonView.RPC("RemoveParticipant", RpcTarget.AllBuffered, newParticipantPV.ViewID);                   // RPC Call
                localPhotonView.RPC("TogglePACOfNewUserEverywhere", RpcTarget.AllBuffered, newParticipantPV.ViewID, true);  // RPC Call
                UnSubscribeToAllExistingParticipants();                                                                     // Local Call
                CallUnSubscribeForParticipants(newParticipantPV.Owner.NickName);                                            // RPC Call actually.
            }
        }
    }


    /// 
    /// Other functions.
    /// 

    [PunRPC]
    void TogglePACOfNewUserEverywhere(int newUser, bool isEnabled)
    {
        /* Toggles the PAC of the target user on all remote clients. Once disabled, 
         * they will no longer take new subscribers and is automatically 
         * unsubscribed for all existing. */
        PhotonView newUserPV = PhotonView.Find(newUser);
        newUserPV.gameObject.GetComponentInChildren<ProximityAudioController>().ToggleSubscribableState(isEnabled);
    }

    [PunRPC]    
    private void AddParticipant(int newUserID)              // These are buffered RPC call to synchronise this array among all the users.
    {
        PhotonView newUser = PhotonView.Find(newUserID);
        participants.Add(newUser);
    }

    [PunRPC]
    private void RemoveParticipant(int newUserID)
    {
        PhotonView newUser = PhotonView.Find(newUserID);
        participants.Remove(newUser);
    }



    private void CallSubscribeForParticipants(string playerID) // Makes all participants subscribe to new visitor.
    {
        foreach (PhotonView p in participants)
            localPhotonView.RPC("SubscribeRPC", p.Owner, playerID);

    }
    [PunRPC]
    void SubscribeRPC(string playerID)          // RPC Child of above function.
    {
        localPhotonView.gameObject.GetComponentInChildren<ProximityAudioController>().SubscribeToPlayerID(playerID);
    }


    [PunRPC]
    private void CallUnSubscribeForParticipants(string playerID) // Makes all participants unsubscribe to leaver.
    {
        foreach (PhotonView p in participants)
            localPhotonView.RPC("UnSubscribeRPC", p.Owner, playerID);

    }
    [PunRPC]
    void UnSubscribeRPC(string playerID)        // RPC Child of above function.
    {
        localPhotonView.gameObject.GetComponentInChildren<ProximityAudioController>().UnSubscribeToPlayerID(playerID);
    }


    private void SubscribeToAllExistingParticipants()       // When you walk into a private space, you subscribe to everyone already part of the room.
    {
        ProximityAudioController localPAC = localPhotonView.gameObject.GetComponentInChildren<ProximityAudioController>();
        foreach (PhotonView p in participants)
            localPAC.SubscribeToPlayerID(p.Owner.NickName);
    }

    private void UnSubscribeToAllExistingParticipants()       // When you walk out of a private space, you unsubscribe to everyone part of the room.
    {
        ProximityAudioController localPAC = localPhotonView.gameObject.GetComponentInChildren<ProximityAudioController>();
        foreach (PhotonView p in participants)
            localPAC.UnSubscribeToPlayerID(p.Owner.NickName);
    }
}
