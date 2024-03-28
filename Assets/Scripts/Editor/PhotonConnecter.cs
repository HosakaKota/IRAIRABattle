
using UnityEngine;

public class PhotonConnecter : MonoBehaviour
{



    void Awake()
    {


        //Vector3 posM = pos_1P.transform.position;
        //Vector3 posS = pos_2P.transform.position;
    }

    void Update()
    {
        OVRInput.Update();
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Button A down");

        }
        if (Input.GetKeyDown(KeyCode.B))
        {

        }
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Debug.Log("Button one down");


            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                Debug.Log("Trigger down");

            }
        }






    }
}