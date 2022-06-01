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

    PlayerMovement movementScript;

    public Seeker seeker;
    Rigidbody2D rb;
    Path path;
    int waypoint = 0;
    bool reachedEndOfPath = false;


    //public GameObject pathFinderTargetDummyObject;
    void Start()
    {
        if (PhotonNetwork.IsConnected && !gameObject.GetComponent<PhotonView>().IsMine)           // Disable script on online players.
            this.enabled = false;

        seeker = GetComponent<Seeker>();

        movementScript = GetComponent<PlayerMovement>();

        GameObject[] dummy = GameObject.FindGameObjectsWithTag("Finish");
        target = dummy[0] ;


        //InvokeRepeating("PathFinderLoop", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPathFinderMouse();
    }

    void FixedUpdate()
    {
        PlayerMover();
    }


    void PathFinderLoop()
    {
        //if (!pathFindingEnabled) return;
        seeker.StartPath(gameObject.transform.position, target.transform.position, OnDestinationReached);
        Debug.Log("Hello");
    }

    void PlayerMover()
    {
        
        if (!pathFindingEnabled) return;
        Debug.Log("Hey");
        if (path == null) return;
        Debug.Log("Works");

        if(waypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            movementScript.ApplyMotion(Vector2.zero);
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = (path.vectorPath[waypoint] - gameObject.transform.position);
        float distance = Vector2.Distance(gameObject.transform.position, path.vectorPath[waypoint]);
        if (distance < wayPointBufferDistance)
            waypoint++;

        
        Debug.Log(direction);
        movementScript.ApplyMotion(direction.normalized);
    }

    void OnDestinationReached(Path p)
    {
        if (!p.error)
        {
            path = p;
            waypoint = 0;
            Debug.Log("Shortest Path Found!");
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
        PathFinderLoop();
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
            Debug.Log("yes");
        }
        else if (Input.anyKeyDown)
        {
            DisablePathFinder();
        }
    }



}
