using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionTest : MonoBehaviour
{

    [SerializeField] OutCollider OutCollider;

    GameObject CounterObject;
    [SerializeField] float frequency = 10f;
    [SerializeField] float amplitude = 10f;
    [SerializeField]Material defaultColor;
    [SerializeField]Material damgeColor;

    private void Awake()
    {
        //OutCollider = GameObject.Find("OutCollider").GetComponent<OutCollider>();
        defaultColor = GetComponent<MeshRenderer>().material;
    }
    
    private void OnTriggerExit(Collider other)
    {
        //initiate particle system
    
        if (other.tag == "Stick")
        {
            //GameObject.Find("HitEffect").GetComponent<ParticleSystem>().Stop();
            //GetComponent<MeshRenderer>().material = defaultColor;
            OVRInput.SetControllerVibration(0, 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stick")
        {
            GetComponent<MeshRenderer>().material = damgeColor;
            CounterEvent();
            OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RTouch);
        }
           
       // WaitForSeconds(3, null);
       //OutCollider.outRange = false;

    }

    static IEnumerator WaitForSeconds(float duration, Action action = null)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }

    //count the hittime


    void CounterEvent()
    {
        GameObject.Find("HitTimeCounter").GetComponent<Counter>().publicHitTimes++;
    }

    
}
