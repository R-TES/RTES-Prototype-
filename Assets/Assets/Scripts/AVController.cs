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
            Agora.ToggleMic();
        }

        public void toggleVideo(){
            Agora.ToggleVideo();
        }
    }
}

