using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TriggerAudioOnEnter : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip audioClip;
    bool alreadyPlayed=false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (alreadyPlayed) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            PhotonView newParticipantPV = collision.gameObject.GetComponent<PhotonView>();
            if (newParticipantPV.IsMine || !PhotonNetwork.IsConnected)
            {
                Debug.LogError("Run!");
                audioSource.PlayOneShot(audioClip);
                alreadyPlayed = true;
            }
        }
    }
}
