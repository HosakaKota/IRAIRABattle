using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollowing : MonoBehaviour
{
    [SerializeField] private OVRCameraRig overCameraRig;

    void Start()
    {
        Vector3 cameraPos = GetCameraPos();
    }

    Vector3 GetCameraPos()
    {
        
        overCameraRig = GameObject.Find("OVRCameraRig").GetComponent<OVRCameraRig>();
        return overCameraRig.centerEyeAnchor.position;
    }
}
