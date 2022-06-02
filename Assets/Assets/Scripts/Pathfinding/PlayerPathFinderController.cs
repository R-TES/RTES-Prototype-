using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Pathfinding;
using MouseRelated;

public class PlayerPathFinderController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;

    bool pathFindingEnabled = false;
    public float wayPointBufferDistance = 1f;

    PlayerMovement playerMover;

    public Seeker seeker;
    Path path;
    int waypoint = 0;
    bool reachedEndOfPath = false;


    //public GameObject pathFinderTargetDummyObject;
    void Start()
    {
        if (PhotonNetwork.IsConnected && !gameObject.GetComponent<PhotonView>().IsMine)           // Disable script on online players.
            this.enabled = false;

        seeker = GetComponent<Seeker>();

        playerMover = GetComponent<PlayerMovement>();

        GameObject[] dummy = GameObject.FindGameObjectsWithTag("Finish");
        target = dummy[0] ;

        //InvokeRepeating("PathFinderLoop", 0f, 0.5f);
    }

    // Update is called once per frame

    void Update()
    {
        PlayerPathFinderMouse();
        PlayerMover();
    }


    void CalculatePath()
    {
        seeker.StartPath(gameObject.transform.position, target.transform.position, OnDestinationReached);
    }

    void PlayerMover()
    {
        
        if (!pathFindingEnabled) return;
        if (path == null) return;
       
        if(waypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            playerMover.ApplyMotion(Vector2.zero);
            return;
        }
        //else 
        reachedEndOfPath = false;
        

        Vector2 direction = (path.vectorPath[waypoint] - gameObject.transform.position);
        float distance = Vector2.Distance(gameObject.transform.position, path.vectorPath[waypoint]);
        
        if (distance < wayPointBufferDistance)
            waypoint++;

        playerMover.ApplyMotion(direction.normalized);
    }

    void OnDestinationReached(Path p)
    {
        if (!p.error)
        {
            path = p;
            waypoint = 0;
            Debug.Log("Shortest Path Found!");
        }

        else
        {
            Debug.Log("No Path found.");
        }
    }


    public static Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z = 0f)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        Vector3 pos = ray.GetPoint(distance);
        pos.z = 0;
        return pos;
    }


    
    void SetPathFinderTarget()
    {
        target.transform.position = GetWorldPositionOnPlane(Input.mousePosition);
        pathFindingEnabled = true;
        CalculatePath();
    }
    
    private void DisablePathFinder()
    {
        pathFindingEnabled = false;
    }
    

    void PlayerPathFinderMouse()
    {
        //if (!pathFindingEnabled) return;

        if (Input.GetMouseButtonDown(0))
        {
            SetPathFinderTarget();
        }
        else if (Input.anyKeyDown)
        {
            DisablePathFinder();
        }
    }



}
