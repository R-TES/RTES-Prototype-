
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using TMPro;

public class Interactable : MonoBehaviour
{

    [Header("Modify the Collider attached to this object to control the range. You can even swap out the Circle Collider with another Collider.\nJust remmeber to set 'isTrigger' to true!")]
    public bool isInRange = false;
    public bool autoTrigger = false;
    public bool triggerAgainOnExit = true; 
    public KeyCode triggerOnInteractKey = KeyCode.E;
    public string customPromptMessage = "";
    public UnityEvent interactEvent;




    private string playerTag = "Player";
    private bool autoTriggerLock = false;
    private bool activeTriggerState = false;

    private Canvas interactPromptBanner;
    private TMP_Text interactPromptText; 




    private void Start()
    {
        interactPromptText = GetComponentInChildren<TMP_Text>();
        if (customPromptMessage == "")
            interactPromptText.text = "Press [" + triggerOnInteractKey.ToString() + "] to interact.";
        else
            interactPromptText.text = customPromptMessage;


        interactPromptBanner = GetComponentInChildren<Canvas>();
        interactPromptBanner.enabled = false;
    }

    private void Update()
    {
        if (isInRange && !autoTriggerLock)
        {
            if (autoTrigger || Input.GetKeyDown(triggerOnInteractKey)) {
                interactEvent.Invoke();
                interactPromptBanner.enabled = false;
                autoTriggerLock = autoTrigger;          // To Prevent triggering infinitely.
                activeTriggerState = !activeTriggerState;
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("IgnoreTriggers")) return;

        interactPromptBanner.enabled = true;
        if (other.gameObject.CompareTag(playerTag))
        {
            if (!PhotonNetwork.IsConnected || other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                isInRange = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("IgnoreTriggers")) return;

        interactPromptBanner.enabled = false;
        if (other.gameObject.CompareTag(playerTag))
        {
            if ( !PhotonNetwork.IsConnected || other.gameObject.GetComponent<PhotonView>().IsMine)
            {
                isInRange = false;
                if(activeTriggerState && triggerAgainOnExit)
                {
                    interactEvent.Invoke();
                }
            }
        }
        autoTriggerLock = false;
        activeTriggerState = false;
    }



}
