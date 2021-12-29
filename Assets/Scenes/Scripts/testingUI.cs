using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testingUI : MonoBehaviour
{
    public int space = 0;
    public Text healthText; 


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            space++; 
            healthText.text = "Space: " + space.ToString(); 
        }
    }
}
