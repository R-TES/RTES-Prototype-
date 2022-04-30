using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonLogScript : MonoBehaviourPunCallbacks
{
    public TMP_Text textfeed;
    
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player otherplayer)
    {
        StartCoroutine(DelayedPlayerGreet(otherplayer));
    }

    IEnumerator DelayedPlayerGreet(Photon.Realtime.Player otherplayer)
    {
        yield return new WaitForSeconds(2);
        textfeed.text = "[<i><color=\"grey\">A player [" + otherplayer.NickName + "] has joined the lobby.</color></i>]\n" + textfeed.text;

    }

    public override void  OnPlayerLeftRoom(Photon.Realtime.Player otherplayer)
    {
        textfeed.text = "<color=\"grey\">[<i>A player [" + otherplayer.NickName + "] has left the lobby, cheerio!</i>]</color>\n" + textfeed.text;
    }
    
}
