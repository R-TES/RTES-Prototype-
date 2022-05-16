using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RevealHiddenArea : MonoBehaviour
{
    public float duration = 1f;
    public float pulsePercent = 0.16f; 
    SpriteRenderer sr;
    Color defaultColor;
    static Color transparentColor = new Color(0f, 0f, 0f, 0f);

    Color startColor;
    Color endColor;
    float step = 0;
    int dir = 1;

    void Start()
    {
       
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;
        startColor = transparentColor;
        endColor = defaultColor;
    }

    void FixedUpdate()
    {
        sr.color = Color.Lerp(transparentColor , defaultColor, step + pulsePercent * Mathf.Sin(Time.time));
        step = Mathf.Clamp(step + dir * Time.deltaTime / duration, -1, 1);
        Debug.Log("STEP: " + (0.1f * Mathf.Sin(Time.time)).ToString());
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PhotonView newParticipantPV = collision.gameObject.GetComponent<PhotonView>();
            if (newParticipantPV.IsMine || !PhotonNetwork.IsConnected)
            {
                dir = -1;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            PhotonView newParticipantPV = collision.gameObject.GetComponent<PhotonView>();
            if (newParticipantPV.IsMine || !PhotonNetwork.IsConnected)
            {
                dir = 1;
            }
        }
    }


}
