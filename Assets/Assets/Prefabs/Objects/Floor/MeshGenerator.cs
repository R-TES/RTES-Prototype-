using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(meshFilter))]
public class NewBehaviourScript : MonoBehaviour
{

    Mesh mesh;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    // Update is called once per frame

    void CreateShape()
    {
        vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(0,0,1),
            new Vector3(1,0,0)
        };

        triangles = new int[]
        {
            0,1,2
        };
    }
    
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }
}
