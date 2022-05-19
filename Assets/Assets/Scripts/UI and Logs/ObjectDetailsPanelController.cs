using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
public class ObjectDetailsPanelController : MonoBehaviour
{
    Image thumbnail;
    TMP_Text infoTextField;
    GameObject selectedObject;
    private void Start()
    {
        thumbnail = gameObject.transform.Find("Thumbnail").GetComponent<Image>();
        infoTextField = gameObject.transform.Find("InfoText").GetComponent<TMP_Text>();
    }


    public void SetInfo(GameObject g)
    {
        selectedObject = g;
        thumbnail.sprite = selectedObject.GetComponent<SpriteRenderer>().sprite;
        thumbnail.transform.localScale = Vector2.one;
        string info = "";

        info += "<b>Name:</b> " + selectedObject.name.Replace("(Clone)", "");

        if (PhotonNetwork.IsConnected) info += "\n<b>Placed by User:</b> " + selectedObject.GetComponent<ObjectInstanceController>().ownerName;
        else info += "\n<b> Played By: </b>You"; 

        infoTextField.text = info;
    }

    public void DestroyTheGameObject()
    {
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Destroy(selectedObject);
        else
            Destroy(selectedObject);
    }
}
