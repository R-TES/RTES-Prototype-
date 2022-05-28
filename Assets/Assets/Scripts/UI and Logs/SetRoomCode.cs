using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class SetRoomCode : MonoBehaviour
{
    TMP_Text textfield;

    private void Start()
    {
        textfield = GetComponent<TMP_Text>();
        if(PhotonNetwork.IsConnected)
            textfield.text = "<b>Room Code:</b>\n" + PhotonNetwork.CurrentRoom.Name;
    }

}
