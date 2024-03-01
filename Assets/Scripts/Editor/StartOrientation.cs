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

   
    //Vector3 handOffset;

    // [Header("Hand Offset")]
    // [SerializeField] GameObject HandGrabInteractor;
    // [SerializeField] float X_HandOffset;
    // [SerializeField] float Y_HandOffset;
    // [SerializeField] float Z_HandOffset;

    void Start()
    {
        

        offset = new(X_offset, Y_offset, Z_offset);
        // handOffset = new(X_HandOffset, Y_HandOffset, Z_HandOffset);
        //HandGrabInteractor.transform.position = handOffset;
        //GameObject.Find("HandGrabInteractable").transform.position -= handOffset;
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
