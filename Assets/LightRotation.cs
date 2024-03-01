using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    public float amount;
    // Update is called once per frame
    void start()
    {
        //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y+amount, transform.rotation.z, 1);
        transform.Rotate(new Vector3(0, 1, 0), Space.World);
    }
    void Update()
    {
        //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y+amount, transform.rotation.z, 1);
        transform.Rotate(new Vector3(0, 1, 0),Space.World);
    }
}
