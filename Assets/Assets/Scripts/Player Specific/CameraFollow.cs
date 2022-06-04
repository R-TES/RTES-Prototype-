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

    [Header("Edge Panning Parameters")]
    public float edgePanMarginVertical = 100f;
    public float edgePanMarginHorizontal = 150f;
    public float deadZone = 20f;
    public float edgePanSpeed = 5f;
    public bool followTargetObject = true;
    public GameObject alternateTargetObject;

    private Vector2 lerpVectors;
    private GameObject targetObject;
    private SpriteRenderer fade;


    void Start()
    {
        if(alternateTargetObject == null)
            BindCameraWithLocalPlayer();
        else
            targetObject = alternateTargetObject;
        fade = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        EdgePan();
        ResetEdgePan();

        if (!followTargetObject) return;
        FluidCameraFollow();

        if (Input.GetKeyDown(KeyCode.Space))
            CenterCamera();
    }

    void FluidCameraFollow()
    {
        float distance = Vector3.Distance(transform.position, targetObject.transform.position + offset);
        if (distance < teleportIfDistanceGreaterThan)
            lerpVectors = Vector3.Lerp(transform.position, targetObject.transform.position + offset, speed * Time.deltaTime);
        else
            lerpVectors = targetObject.transform.position + offset;
        transform.position = new Vector3(lerpVectors.x, lerpVectors.y, transform.position.z);
    }

    void BindCameraWithLocalPlayer(){
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        targetObject = players[0];
        
    }

    void CenterCamera()
    {
        transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, transform.position.z); 
    }

    void EdgePan()
    {
        if (!Input.GetMouseButton(2)) return;

        if (Input.mousePosition.x > Screen.width - edgePanMarginHorizontal && Input.mousePosition.x < Screen.width - deadZone)
        {
            followTargetObject = false;
            transform.position += edgePanSpeed * Time.deltaTime * Vector3.right ;
        }
        else if (Input.mousePosition.x < edgePanMarginHorizontal && Input.mousePosition.x > deadZone)
        {
            followTargetObject = false;
            transform.position += edgePanSpeed * Time.deltaTime * Vector3.left;
        }
        if (Input.mousePosition.y > Screen.height - edgePanMarginVertical && Input.mousePosition.y < Screen.height - deadZone)
        {
            followTargetObject = false;
            transform.position += edgePanSpeed * Time.deltaTime * Vector3.up;
        }
        else if (Input.mousePosition.y < edgePanMarginVertical && Input.mousePosition.y > deadZone)
        {
            followTargetObject = false;
            transform.position += edgePanSpeed * Time.deltaTime * Vector3.down;
        }
    }


    void ResetEdgePan()
    {
        if (Input.GetMouseButton(2)) return;
        else if (Input.anyKey)
        {
            followTargetObject = true;
        }
    }


    public void CameraFade(Color color)
    {
        fade.color = color;
    }
}
