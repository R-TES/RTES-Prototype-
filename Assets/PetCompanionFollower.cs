using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PetCompanionFollower : MonoBehaviour
{
    AIDestinationSetter aiDestination;
    
    void Start()
    {
        aiDestination = GetComponent<AIDestinationSetter>();
        aiDestination.target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
    }

}
