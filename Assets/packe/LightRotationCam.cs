using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotationCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(new Vector3(0, 0, 45), Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
