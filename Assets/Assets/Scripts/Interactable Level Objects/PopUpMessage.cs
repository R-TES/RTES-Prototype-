using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMessage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PopUpMessageObject;


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PopUpMessageObject.SetActive(true); 
        }
            
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PopUpMessageObject.SetActive(false);
        }
            
    }

}
