using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    private void Update()
    {
        if (interacting)
        {
            if (Input.GetKeyDown(interact_key))
            {
                Debug.Log("Keypressed");
                if (!locked)
                {
                    Debug.Log("Testing");
                    if (interacter.CompareTag("Player") || interacter.CompareTag("Admin"))
                        LockDoor(interacter.gameObject);
                }
                else if (locked)
                {
                    if (!onlyOnePersonCanLock || interacter.gameObject == PersonWhoLocked)
                    {
                        UnlockDoor();
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




    void LockDoor(GameObject player)
    {
        Debug.Log("Locked!");
        locked = true;
        animator.TriggerDeactivateObjectAnimation();
        animator.DisableObject();

        PersonWhoLocked = player;
        solidDoorCollider.enabled = true;
    }

    void UnlockDoor()
    {
        Debug.Log("Unlocked!");
        animator.EnableObject();
        animator.TriggerActivateObjectAnimation(); 
        locked = false;
        solidDoorCollider.enabled = false;

    }
}
