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
        textfeed.text = "[<i><color=\"grey\">Somebody has joined the lobby.</color></i>]\n" + textfeed.text;
    }

    public override void  OnPlayerLeftRoom(Photon.Realtime.Player otherplayer)
    {
        textfeed.text = "<color=\"grey\">[<i>" + otherplayer.NickName + " has left the lobby, cheerio!</i>]<color>\n" + textfeed.text;
    }
    
}
