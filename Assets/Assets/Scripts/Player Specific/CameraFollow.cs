using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Parameters")]
    
    public float speed = .1f;
    public float teleportIfDistanceGreaterThan = 100f;
    public Vector3 offset;

    [Header("Special Parameters")]
    public bool followTargetObject = true;
    public GameObject alternateTargetObject;

    private Vector2 lerpVectors;
    private GameObject targetObject;

    void Start()
    {
        if(alternateTargetObject == null)
            BindCameraWithLocalPlayer();
        else
            targetObject = alternateTargetObject;
    }

    void FixedUpdate()
    {
        if(followTargetObject)
            FluidCameraFollow();
    }

    void FluidCameraFollow()
    {
        float distance = Vector3.Distance(transform.position, targetObject.transform.position + offset);
        if (distance < teleportIfDistanceGreaterThan)
            lerpVectors = Vector3.Lerp(transform.position, targetObject.transform.position + offset, speed);
        else
            lerpVectors = targetObject.transform.position + offset;
        transform.position = new Vector3(lerpVectors.x, lerpVectors.y, transform.position.z);
    }

    void BindCameraWithLocalPlayer(){
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        targetObject = players[0];
    }

}
