using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Scripts{
    public class AVController : MonoBehaviour {

        // uid to sub unsub
        public InputField uid;

        public void subscribe(){
            Agora.Subscribe(uid.text);
        }

        public void unsubscribe(){
            Agora.Unsubscribe(uid.text);
        }

        public void toggleMic(){
            //Agora.ToggleMic();
            StartCoroutine(DumbResubscribeFix());
        }

        public void toggleVideo(){
            //Agora.ToggleVideo();
            StartCoroutine(DumbResubscribeFix());
        }

        IEnumerator DumbResubscribeFix() {
            GameObject localplayer = GameObject.FindGameObjectsWithTag("Player")[0];
            Vector2 originalPos = localplayer.transform.position;
            localplayer.transform.position = new(10000000, -10000000, 10);
            yield return new WaitForSeconds(0.001f);
            Debug.Log(GameObject.FindGameObjectsWithTag("Player")[0].transform.position);
            localplayer.transform.position = originalPos;
        }
    }
}

