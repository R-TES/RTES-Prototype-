using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorTeleportControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> blockList = new List<GameObject>();


    public void addToBlockList(GameObject g)
    {
        blockList.Add(g);
        blockList.Add(g);
    }


    public bool checkIfAlreadyBlocked(GameObject g)
    {   
        
        return blockList.Contains(g);
    }

    public void removeFromBlockList(GameObject g)
    {
        blockList.Remove(g);
    }


}
