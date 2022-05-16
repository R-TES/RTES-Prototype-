using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectInstanceController : MonoBehaviour
{
    SpriteRenderer sr;
    public GameObject objectDetailsPanel;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        objectDetailsPanel = GameObject.Find("ObjectDetailWindow");
        OnInstantiate();

    }

    // Update is called once per frame
    public void DestroyObject()
    {
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Destroy(gameObject);
        else
            Destroy(gameObject);
    }

    public void OpenObjectDetailsPanel()
    {
        objectDetailsPanel.GetComponent<Animator>().SetTrigger("open");
        objectDetailsPanel.GetComponent<ObjectDetailsPanelController>().SetInfo(gameObject);
        //objectDetailsPanel.SendInformation(gameObject);
    }

    void OnInstantiate()
    {
        Debug.Log("On INSTANTIATE WORKS");
 
        MouseClickActionScript actionScript = gameObject.AddComponent<MouseClickActionScript>();
        actionScript.leftClickEvent = new();
        actionScript.leftClickEvent.AddListener(OpenObjectDetailsPanel);
    }

}
