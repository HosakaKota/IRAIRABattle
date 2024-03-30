using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTest_V2 : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float lineRadius;
    private Dictionary<int, Mesh> _meshMap = new Dictionary<int, Mesh>();
    public Material _material;
    private bool _castShadows = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        drawCylinder(startPosition, endPosition);
    }

    public void SetV1(Vector3 v1)
    {
        startPosition = v1;
    }

    public void SetV2(Vector3 v2)
    {
        endPosition = v2;
    }


    public void drawCylinder(Vector3 a, Vector3 b)
    {

        if (isNaN(a) || isNaN(b)) { return; }

        float length = (a - b).magnitude;

        if ((a - b).magnitude > 0.001f)
        {
            Graphics.DrawMesh(getCylinderMesh(length),
                              Matrix4x4.TRS(a,
                                            Quaternion.LookRotation(b - a),
                                            new Vector3(transform.lossyScale.x, transform.lossyScale.x, 1)),
                              _material,
                              gameObject.layer,
                              null, 0, null, _castShadows);
             
        }
    }

    private Mesh getCylinderMesh(float length)
    {
        const float CYLINDER_MESH_RESOLUTION = 0.1f;
        int lengthKey = Mathf.RoundToInt(length * 100 / CYLINDER_MESH_RESOLUTION);

        Mesh mesh;
        if (_meshMap.TryGetValue(lengthKey, out mesh))
        {
            return mesh;
        }

        mesh = new Mesh();
        mesh.name = "GeneratedCylinder";
        mesh.hideFlags = HideFlags.DontSave;

        List<Vector3> verts = new List<Vector3>();
        List<Color> colors = new List<Color>();
        List<int> tris = new List<int>();

        Vector3 p0 = Vector3.zero;
        Vector3 p1 = Vector3.forward * length;
        int _cylinderResolution = 12;
        float _cylinderRadius = lineRadius;
        for (int i = 0; i < _cylinderResolution; i++)
        {
            float angle = (Mathf.PI * 2.0f * i) / _cylinderResolution;
            float dx = _cylinderRadius * Mathf.Cos(angle);
            float dy = _cylinderRadius * Mathf.Sin(angle);

            Vector3 spoke = new Vector3(dx, dy, 0);

            verts.Add(p0 + spoke);
            verts.Add(p1 + spoke);

            colors.Add(Color.white);
            colors.Add(Color.white);

            int triStart = verts.Count;
            int triCap = _cylinderResolution * 2;

            tris.Add((triStart + 0) % triCap);
            tris.Add((triStart + 2) % triCap);
            tris.Add((triStart + 1) % triCap);

            tris.Add((triStart + 2) % triCap);
            tris.Add((triStart + 3) % triCap);
            tris.Add((triStart + 1) % triCap);
        }

        mesh.SetVertices(verts);
        mesh.SetIndices(tris.ToArray(), MeshTopology.Triangles, 0);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.UploadMeshData(true);

        _meshMap[lengthKey] = mesh;

        return mesh;
    }
    private bool isNaN(Vector3 v)
    {
        return float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);
    }

}