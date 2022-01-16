using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using UnityEngine.UI; 
public class PlayerNameSet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playernamefield;

    void Start()
    {   
        playernamefield.GetComponent<Text>().text = "Player"; 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}

