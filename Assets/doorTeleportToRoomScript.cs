using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTeleportToRoomScript : MonoBehaviour
{
    public GameObject destination;
    public doorTeleportControllerScript doorController;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Hi");
        if(!doorController.checkIfAlreadyBlocked(col.gameObject)){
            col.gameObject.transform.position = destination.transform.position; 
            doorController.addToBlockList(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col){
        Debug.Log("Bye");
        doorController.removeFromBlockList(col.gameObject);
    }
}
