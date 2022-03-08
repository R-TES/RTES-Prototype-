using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using UnityEngine.UI;

public class spawnPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player; 
    private PhotonView view;

    void Start()
    {
        Vector2 pos = new Vector2(0, 0);
        view = GetComponent<PhotonView>();

        //PlayerPopulate();
        PhotonNetwork.Instantiate(player.name, pos, Quaternion.identity);
    }

    void PlayerPopulate() {
        string nickName = PhotonNetwork.LocalPlayer.NickName;
        PhotonNetwork.LocalPlayer.NickName = nickName;

        GameObject playerNameText = player.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        Debug.Log("GameObject: " + playerNameText.name);
        playerNameText.GetComponent<Text>().text = PhotonNetwork.LocalPlayer.NickName;
    }

}
