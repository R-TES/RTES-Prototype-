using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfoBar : MonoBehaviour
{
    TMP_Text textfield;
    public Vector3 offset = new(1f, -1f, 0);
    private void Start()
    {
        textfield = GetComponent<TMP_Text>();
    }

    public void ResetGameInfoBarText()
    {
        textfield.text = "";
    }

    public void SetGameInfoBarText(string message, Vector3 pos)
    {
        textfield.text = message;
        gameObject.transform.position = pos + offset;
    }



}
