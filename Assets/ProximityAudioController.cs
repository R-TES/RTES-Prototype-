using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Scripts; // Agora

namespace Scripts
{
    public class ProximityAudioController : MonoBehaviour
    {
        // Start is called before the first frame update
        public bool allowingSubscribers = true;

        private void Start()
        {
            BindEntityaWithLocalPlayer();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
           
            
            if (collision.gameObject.CompareTag("Player"))
            {
                
                string playerID = collision.gameObject.GetComponent<PhotonView>().Owner.NickName;
                Debug.Log("Entering" + playerID + " " + PhotonNetwork.LocalPlayer.NickName);
                if (playerID == PhotonNetwork.LocalPlayer.NickName) return;
                
                if (playerID != null && playerID.Length > 3)
                {
                    Debug.Log("Calling Subscribe here");
                    Debug.Log("Player ID: [" + playerID + "]");
                    Agora.Subscribe(playerID);
                }
                    
            }


        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Exiting");
                string playerID = collision.gameObject.GetComponent<PhotonView>().Owner.NickName;
                Debug.Log("Entering" + playerID + " " + PhotonNetwork.LocalPlayer.NickName);
                if (playerID == PhotonNetwork.LocalPlayer.NickName) return;

                if (playerID != null && playerID.Length > 3)
                {
                    Debug.Log("Calling Unsubscribe here");
                    Debug.Log("Player ID: [" + playerID + "]");
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


}