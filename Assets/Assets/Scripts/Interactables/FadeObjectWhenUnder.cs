using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;
using Photon;

[RequireComponent(typeof(CompositeCollider2D))]

public class FadeObjectWhenUnder : MonoBehaviour
{
    public float fadePercent = 0.825f;
    Tilemap tl;
    Color defaultColor;
    Color fadeColor;


    private void Start()
    {
        tl = GetComponent<Tilemap>();
        defaultColor = tl.color;
        fadeColor = tl.color;
        fadeColor.a = fadePercent;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Yes");
            if (collision.gameObject.GetComponent<PhotonView>().IsMine || !PhotonNetwork.IsConnected)
            {
                tl.color = fadeColor;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PhotonView>().IsMine || !PhotonNetwork.IsConnected)
            {
                tl.color = defaultColor;
            }
        }
    }

}
