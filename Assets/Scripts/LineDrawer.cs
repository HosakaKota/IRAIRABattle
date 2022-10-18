using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LineDrawer : MonoBehaviour
{
    public GameObject sphereObject;
    public GameObject trackingObject;
    public GameObject stage;
    int numbersOfSpheres;
    Quaternion quaternion = new();
    [SerializeField] GameObject TMPGameObject;
    [SerializeField] TextMeshPro text;
    Vector3 v1;
    Vector3 v2;
    Vector3 currentPos;
    Vector3 UpPos;
    //Vector3 v3;
    DrawTest_V2 DrawTest_V2; 
    [SerializeField] GameObject ring;

    [Header("Count Down")]
    bool canPaint;
    bool canPlay;
    bool startCount;
    bool paintCount;
    bool firstGrab = true;
    [SerializeField] int startCountDown_Sec = 3;
    [SerializeField] int paintCountDown_Sec = 15;
    [SerializeField] TextMeshPro startCountDownText;
    [SerializeField] TextMeshPro paintCountDownText;
    CheckIsMoved checker;

    [SerializeField] GameObject Stick;
    [SerializeField] GameObject drawer;

    private void Awake()
    {
        text = TMPGameObject.GetComponent<TextMeshPro>();
        checker = GetComponent<CheckIsMoved>();
        
       // DrawTest_V2 = GetComponent<DrawTest_V2>();
    }

    void V2()
    {
        v2 = trackingObject.transform.position;
    }
    private IEnumerator ChangeTime(int time)
    {
        startCountDownText.text = time.ToString();
        while (time >= 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            startCountDownText.text = time.ToString("0");
        }
            canPaint = true;
            startCount = false;
            startCountDownText.gameObject.SetActive(false);
            paintCount = true;

    }
    private IEnumerator ChangePaintTime(int time)
    {
        paintCountDownText.text = time.ToString();
        while (time >= 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            paintCountDownText.text = time.ToString();
        }
        canPaint = false;
        paintCount = false;
        paintCountDownText.gameObject.SetActive(false);
        Stick.SetActive(true);
        drawer.SetActive(false);

    }
    public void Drawer()
    {
        //If the trigger button is pushed.      
        #region Start Count Down of 3 Sec;

        if (startCount)
        {
            StartCoroutine(ChangeTime(startCountDown_Sec));
        }

        #endregion

        if (paintCount&&!startCount)
        {
            StartCoroutine(ChangePaintTime(paintCountDown_Sec));
        }

        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) &&checker.painterIsMoved)
        {
            if (firstGrab)
            {
                startCount = true;
                firstGrab = false;
            }

            if (canPaint)
            {
                v1 = trackingObject.transform.position;
                InvokeRepeating("V2", 0.05f, 0);

                if (v1 != v2)
                {

                    ring.SetActive(false);

                    Instantiate(sphereObject, trackingObject.transform.position, quaternion, stage.transform);//OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch)+offset
                    numbersOfSpheres++;

                }
            }

           
        }
        else
        {
            ring.SetActive(true);
        }
        text.text = numbersOfSpheres.ToString();
    }



    //public void DrawLine()
    //  {
    //    if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
    //   {
    //lineRenderer;
    // }
    // }
    void FixedUpdate()
    {
        Drawer();
    }
}
