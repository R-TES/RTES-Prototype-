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
        if (col.gameObject.CompareTag("IgnoreTriggers")) return; // Don't teleport objects with this tag.

        if(!doorController.checkIfAlreadyBlocked(col.gameObject)){
            col.gameObject.transform.position = destination.transform.position; 
            doorController.addToBlockList(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("IgnoreTriggers")) return;
        doorController.removeFromBlockList(col.gameObject);
    }
}
