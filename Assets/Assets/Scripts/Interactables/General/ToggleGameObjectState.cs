using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObjectState : MonoBehaviour
{


    public void ToggleGameObject()
    {
        Debug.Log("Running");
        if (!gameObject.activeSelf)
            EnableGameObject();
        else
            DisableGameObject();
        
    }

    public void EnableGameObject()
    {
        gameObject.SetActive(true);
    }

    public void DisableGameObject()
    {
        gameObject.SetActive(false);
    }

}
