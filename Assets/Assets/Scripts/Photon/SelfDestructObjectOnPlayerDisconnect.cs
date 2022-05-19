using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SelfDestructObjectOnPlayerDisconnect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    PhotonView targetObjectPV;

    private void Start()
    {
        targetObjectPV = GetComponent<PhotonView>();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherplayer)
    {
        if (targetObjectPV.Owner.NickName != otherplayer.NickName) return;    // Ignore if gameobject isn't owned by player who disconnected.
        if (targetObjectPV.gameObject != null)
        {
            
            PhotonNetwork.Destroy(targetObjectPV.gameObject);  // Claim ownership and destroy gameobject otherwise.
        }
    }

}
