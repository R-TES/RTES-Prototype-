using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;


public class ChatScript : MonoBehaviour
{
    public TMP_InputField inputfield;
    public TMP_Text textfeed;

    private PhotonView photonView;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendTextMessage();
        }
    }

    public void SendTextMessage()
    {

        if (AllowedTextTest(inputfield.text))
        {
            photonView.RPC("RecieveMessageRPC", RpcTarget.All, inputfield.text, PhotonNetwork.LocalPlayer.NickName);
            ClearInputField();
        }
    }

    [PunRPC]
    private void RecieveMessageRPC(string message, string author,PhotonMessageInfo info)
    {
        string prepend_text = System.DateTime.Now.ToString("[HH:mm]") 
                                + " <b><color=\"green\">"
                                + author + ":</color></b> "
                                + message + "\n";
        textfeed.text = prepend_text + textfeed.text;

    }


    private bool AllowedTextTest(string t)
    {
        //do other checks here
        if (t.Length < 1)
        {
            Debug.Log("Message too small or something.");
            return false;
        }

        return true;
    }

    public void ClearInputField()
    {
        inputfield.text = "";
    }

}
