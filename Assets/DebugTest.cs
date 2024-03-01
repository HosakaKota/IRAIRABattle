using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
   public  GameObject start;
    public GameObject end;
    public GameObject playStick;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            start.gameObject.SetActive(true);
            end.gameObject.SetActive(true);
            
        }
    }
}
