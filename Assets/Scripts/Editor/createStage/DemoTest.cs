using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DemoTest : MonoBehaviour
{
    public Material mat;

    float a = 1.2f, UVoffset = 0;

    void Start()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[] {
        new Vector3 (-a, 0, a),
        new Vector3 (a,  0, a),
        new Vector3 (-a , a, 0),
        new Vector3 (a ,  a, 0),

        new Vector3 (-a, 0, -a),
        new Vector3 (a, 0, -a),
        new Vector3 (-a , -a, 0),
        new Vector3 (a , -a, 0),
    };

        mesh.uv = new Vector2[] {
            new Vector2(0, 0),
            new Vector2(0, 1),

            new Vector2(1, 0),
            new Vector2(1, 1),

            new Vector2(2, 0),
            new Vector2(2, 1),

            new Vector2(3, 0),
            new Vector2(3, 1),

                };

        

        mesh.triangles = new int[] {
        0, 1, 2,
        1, 3, 2,

       
        2, 3, 4,
        3, 5, 4,
        
        4, 5, 6,
        5, 7, 6,
        
        6, 7, 0,
        7, 1, 0,

        
     };

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshRenderer>().material = mat;
    }
}