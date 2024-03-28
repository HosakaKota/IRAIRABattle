using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField]ParticleSystem particle;

    public PlayTimeManager timeManager;
    int seconds=0;
    [SerializeField] TextMeshPro playTime;
    bool flag;
    private void Start()
    {
       
        timeManager = GameObject.FindObjectOfType<PlayTimeManager>();
        //OutCollider = GameObject.Find("OutCollider").GetComponent<OutCollider>();
        defaultColor = GetComponent<MeshRenderer>().material;
        //particleSystem = GameObject.Find("HitEffect").GetComponent<ParticleSystem>();
        StartCoroutine(CountUp());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            if (timeManager.startedGame)
            {
               // timeManager.StartProtectingSystem(1);
                Debug.Log(collision.gameObject.name + "----------------------------------------------------------------------------------------------collisionenter");
                //if (flag)
                //{
                    //CounterEvent();
                    particle.Play();
                    OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RTouch);
                    timeManager.OnMissPlay();
               // }
                //GetComponent<MeshRenderer>().material = damgeColor;
            }
        }
       

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            if (timeManager.startedGame)
            {
                Debug.Log(collision.gameObject.name + "---------collisionexit");
               // flag = true;

                particle.Stop();
                //GetComponent<MeshRenderer>().material = defaultColor;
                OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);

            }
        }
           


    }

    static IEnumerator WaitForSeconds(float duration, Action action = null)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
    }

    //count the hittime


    //hit start ----->  start count ------> 

    //hit end ----->  end count



    void CounterEvent()
    {
        GameObject.Find("HitTimeCounter").GetComponent<Counter>().publicHitTimes++;
    }

    IEnumerator CountUp()
    {
        yield return new WaitForSeconds(1);
        seconds+=1;
        playTime.text = seconds.ToString();
    }
}
