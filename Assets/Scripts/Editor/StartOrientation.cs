using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOrientation : MonoBehaviour
{
    [Header("Start Offset")]
    [SerializeField] GameObject Player;
    [SerializeField] float X_offset;
    [SerializeField] float Y_offset;
    [SerializeField] float Z_offset;
    Vector3 offset;

    void Start()
    {


        offset = new(X_offset, Y_offset, Z_offset);
        Invoke("Follow", 1);

    }

    void Follow()
    {
        transform.position = Player.transform.position + offset;
    }

    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.B))
        {
            Follow();
        }
    }
}
