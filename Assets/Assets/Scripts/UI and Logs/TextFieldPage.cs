using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFieldPage : MonoBehaviour
{
    TMP_Text textfield;
    int currentpage = 0;
    private void Start()
    {
        textfield = GetComponent<TMP_Text>();
    }


    public void IncrementPage()
    {
        currentpage++;
        textfield.pageToDisplay = currentpage;

    }

    public void DecrementPage()
    {
        currentpage--;
        textfield.pageToDisplay = currentpage;

    }

}
