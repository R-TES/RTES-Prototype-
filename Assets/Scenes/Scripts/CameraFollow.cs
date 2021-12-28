using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform player;
    private Vector3 tempPos;

    public float leftBound, rightBound, upperBound, lowerBound; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        tempPos = transform.position ;
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        tempPos.x = player.position.x;
        tempPos.y = player.position.y;
        if(tempPos.x <= leftBound) tempPos.x =leftBound ;
        if(tempPos.y <= lowerBound) tempPos.y =lowerBound ;
        if(tempPos.y >= upperBound) tempPos.y =upperBound ;
        if(tempPos.x >= rightBound) tempPos.x =rightBound ;

        transform.position = tempPos; 
    }
}
