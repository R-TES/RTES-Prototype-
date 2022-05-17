using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pathfinding;

public class ObjectTracker : MonoBehaviour
{


    public Transform target;  // didn't find ?

	public float speed = 200f;
	public float nextWaypointDisance = 3f;

	public Tranform enemyGFX; //reference to the enemy graphics

	Path path; // didn;t find 
	int currentWaypoint = 0;
	bool reachedEndOfPath = false;

	Seeker seeker; // didn't find the Seeker
	Rigidbody2D rb;

	 // Start is called before the first frame update
    void Start()
	{
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();

		InvokeRepeating("UpdatePath", 0f, .5f);

	}

	void UpdatePath()
	{
		if(seeker.IsDone())
			seeker.StartPath(rb.position, target.position, OnPathComplete);
	}


	void OnPathComplete(Path p)
	{
		if(!p.error)
		{
			path = p;
			currentWaypoint = 0;
		}
	}

   // Update is called once per frame
	void Update()
	{
		if(path == null)
			return;

		if(currentWaypoint >= path.vectorPath.Count)
		{
			reachedEndOfPath = true;
			return;
		}
		else
		{
			reachedEndOfPath = false;
		}

		Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

		Vector2 force = direction*speed*Time.deltaTime;

		rb.AddForce(force);

		float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

		if(distance < nextWaypointDistance)
		{
			currentWaypoint++;
		}

		
		//code block to flip the charcter around
		if(force.velocity.x >= 0.01f)
		{
			enemyGFX.localScale = new Vector3(-1f,1f,1f);
		}
		else if(force.velocity.x <= -0.01f)
		{
			enemyGFX.localScale = new Vector3(1f, 1f, 1f);
		}


	}
   


 

}
