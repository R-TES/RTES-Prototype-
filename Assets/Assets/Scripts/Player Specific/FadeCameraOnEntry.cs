using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FadeCameraOnEntry : MonoBehaviour
{
    [Header("Set Fade")]
    public Color cameraFadeColorOnEntry = new Color(0f, 0f, 0f, 0.15f);
    public Color cameraFadeColorOnExit = new(0f, 0f, 0f, 0f);
    public Camera maincamera;
    

    private void Start()
    {
        if(!maincamera)
            maincamera = Camera.main; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("IgnoreTriggers")) return;
        if (!collision.gameObject.CompareTag("Player")) return;
        if (PhotonNetwork.IsConnected && !collision.gameObject.GetComponent<PhotonView>().IsMine) return;
        
        maincamera.GetComponent<CameraFollow>().CameraFade(cameraFadeColorOnEntry);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("IgnoreTriggers")) return;
        if (!collision.gameObject.CompareTag("Player")) return;
        if (PhotonNetwork.IsConnected && !collision.gameObject.GetComponent<PhotonView>().IsMine) return;

        maincamera.GetComponent<CameraFollow>().CameraFade(cameraFadeColorOnExit);
    }

}
