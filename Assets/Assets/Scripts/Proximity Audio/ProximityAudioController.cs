using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Scripts; // Agora


    public class ProximityAudioController : MonoBehaviour
    {
        private bool allowingSubscribers = true;
        private string[] blockedUsers; 

        private void Start()
        {
            BindEntityaWithLocalPlayer();   
            // Bind as child the Script object onto the player object controlled by current user.
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                string playerID = collision.gameObject.GetComponent<PhotonView>().Owner.NickName;
                if (playerID == PhotonNetwork.LocalPlayer.NickName) return; // Probably can remove this. Too lazy to check if it'll break.
                if (playerID != null)
                {
                    Agora.Subscribe(playerID);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                string playerID = collision.gameObject.GetComponent<PhotonView>().Owner.NickName;
                if (playerID == PhotonNetwork.LocalPlayer.NickName) return;
                if (playerID != null)
                {
                    Agora.Unsubscribe(playerID);
                }
            }
        }

        void BindEntityaWithLocalPlayer()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            gameObject.transform.parent = players[0].transform;
            gameObject.transform.position = gameObject.transform.parent.position;
        }

    }


