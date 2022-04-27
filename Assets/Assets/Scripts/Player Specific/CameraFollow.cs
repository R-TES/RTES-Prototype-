using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{
    
    public float speed = .1f;

    private Vector3 tempPos;
    private Transform localplayer; 
    private Vector2 v;

    void Start()
    {   
       BindCameraWithLocalPlayer();    
    }

    void FixedUpdate()
    { 
        v = Vector3.Lerp(transform.position, localplayer.position, speed);
        transform.position = new Vector3(v.x, v.y, transform.position.z);
    }

    void BindCameraWithLocalPlayer(){
        
        tempPos = transform.position ;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        localplayer = players[0].transform ; 
    }

}
