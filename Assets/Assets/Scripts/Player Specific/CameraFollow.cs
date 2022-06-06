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

    [Header("Camera Zoom Setting")]
    public float maxZoomDelta;
    public float minZoomDelta;
    public float sensitivity = 10;
    private float currentZoom;

    private Vector2 lerpVectors;
    private GameObject targetObject;
    private SpriteRenderer fade;
    private Camera cam;

    void Start()
    {
        if(alternateTargetObject == null)
            BindCameraWithLocalPlayer();
        else
            targetObject = alternateTargetObject;
        fade = GetComponentInChildren<SpriteRenderer>();
        cam = GetComponent<Camera>();
        currentZoom = cam.fieldOfView;
    }

    void Update()
    {
        EdgePan();
        ResetEdgePan();
        if (Input.GetKeyDown(KeyCode.Space))
            CenterCamera();

        if (!followTargetObject) return;
        FluidCameraFollow();
        ChangeZoom(Input.GetAxis("Mouse ScrollWheel"), sensitivity);
    }


    void ChangeZoom(float i = 1, float sens = 1f)
    {
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - i * sensitivity, currentZoom - minZoomDelta, currentZoom + maxZoomDelta);
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
        if (!Input.GetMouseButton(0)) return;

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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxisRaw("Horizontal")!= 0 || Input.GetAxisRaw("Vertical")!= 0 )
            followTargetObject = true;   
    }


    public void CameraFade(Color color)
    {
        fade.color = color;
    }
}
