using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debughello : MonoBehaviour
{
    public GameObject particleSystem; 

    public void Hello(){
        Vector3 temp = particleSystem.transform.position;
        if(temp.z!=-100) temp.z = -100;
        else temp.z = 0;
        particleSystem.transform.position = temp; 
    }
}
