using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectInstanceController : MonoBehaviour
{
    SpriteRenderer sr;
    public GameObject objectDetailsPanel;
    public string ownerName;
    public bool isSolid = false;

    void Start()
    {
        AttachComponenetsOnInstantiate();

        sr = GetComponent<SpriteRenderer>();
        objectDetailsPanel = GameObject.Find("ObjectDetailWindow");

        if(PhotonNetwork.IsConnected && ownerName == null)
            ownerName = gameObject.GetPhotonView().Owner.NickName;
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

    void AttachComponenetsOnInstantiate()
    {
        if (GetComponent<Collider2D>() == null)
        {
            Debug.Log("Adding a collider.");
            BoxCollider2D bc = gameObject.AddComponent<BoxCollider2D>();
            if(!isSolid)
                bc.isTrigger = true;
        }

        if (GetComponent<MouseClickActionScript>() == null)
        {
            MouseClickActionScript actionScript = gameObject.AddComponent<MouseClickActionScript>();
            actionScript.leftClickEvent = new();
            actionScript.leftClickEvent.AddListener(OpenObjectDetailsPanel);
        }

        if (GetComponent<PhotonView>() == null)
        {
            gameObject.AddComponent<PhotonView>();
        }
        if (GetComponent<Rigidbody>() && GetComponent<Rigidbody>().isKinematic)
        {
            gameObject.AddComponent<PhotonTransformViewClassic>();
        }

        if(gameObject.layer != LayerMask.NameToLayer("Players"))
        gameObject.layer = LayerMask.NameToLayer("Placeable");
        

    }

}
