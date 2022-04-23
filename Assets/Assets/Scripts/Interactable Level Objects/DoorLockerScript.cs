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
    public string adminTag = "Admin";
    public Collider2D solidDoorCollider;

    private GameObject PersonWhoLocked;

    private bool interacting = false;
    private Collider2D interacter; 
    private bool locked = false;
    PhotonView photonView;
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
                Debug.Log(locked);
                if (!locked)
                {
                    if (interacter.CompareTag("Player") || interacter.CompareTag("Admin"))
                    {
                        LockDoor();
                        photonView.RPC("LockDoor", RpcTarget.All);
                    }
                }
                else if (locked)
                {
                    if (!onlyOnePersonCanLock || interacter.gameObject == PersonWhoLocked)
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
        interacting = true;
        interacter = other;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        interacting = false;
        interacter = null; 
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
