using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{

   
    private Vector3 tempPos;
    private Transform localplayer; 
    private bool notFound = true; 
    public float leftBound, rightBound, upperBound, lowerBound; 

    void LateUpdate()
    {   
        if(notFound){
            InstantiateCamera();
        }
        else{        
            tempPos.x = localplayer.position.x;
            tempPos.y = localplayer.position.y;
            transform.position = tempPos;
        }
        //if(tempPos.x <= leftBound) tempPos.x =leftBound ;
        //if(tempPos.y <= lowerBound) tempPos.y =lowerBound ;
        //if(tempPos.y >= upperBound) tempPos.y =upperBound ;
        //if(tempPos.x >= rightBound) tempPos.x =rightBound ;

        
        
    }


    void InstantiateCamera(){
        
        tempPos = transform.position ;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        localplayer = players[0].transform ;
        notFound = false; 
    }

}
