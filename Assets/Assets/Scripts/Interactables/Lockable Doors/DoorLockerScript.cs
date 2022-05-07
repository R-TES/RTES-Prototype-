using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DoorLockerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public activateObjectAnimationIfNear animator;
    public KeyCode interact_key = KeyCode.E;
    public bool onlyOnePersonCanLock = true; 
    public Collider2D solidDoorCollider;

    private Collider2D personWhoLocked;
    private bool interacting = false;
    private Collider2D interacter; 
    private bool locked = false;
    private PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (interacting)
        {
            if (Input.GetKeyDown(interact_key))
            {
                Debug.Log("Key Pressed: " + interact_key.ToString());
                if (!locked)
                {
                    if (interacter.GetComponent<PhotonView>().IsMine || !PhotonNetwork.IsConnected)
                    {
                        LockDoor();
                        photonView.RPC("LockDoor", RpcTarget.All);
                        personWhoLocked = interacter; 
                    }
                }
                else if (locked)
                {
                    if (!onlyOnePersonCanLock && interacter.GetComponent<PhotonView>().IsMine || interacter == personWhoLocked)
                    {
                        UnlockDoor();
                        photonView.RPC("UnlockDoor", RpcTarget.All);
                    }
                }
            }
        }
    }


    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interacting = true;
            interacter = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interacting = false;
            interacter = null;
        }
    }



    [PunRPC]
    void LockDoor()
    {
        Debug.Log("Locked!");
        locked = true;
        animator.TriggerDeactivateObjectAnimation();
        animator.DisableObject();

        //PersonWhoLocked = player;
        solidDoorCollider.enabled = true;


    }

    [PunRPC]
    void UnlockDoor()
    {
        Debug.Log("Unlocked!");
        animator.EnableObject();
        animator.TriggerActivateObjectAnimation(); 
        locked = false;
        solidDoorCollider.enabled = false;

    }


}
