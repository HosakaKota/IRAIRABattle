using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor;
using System.IO;


public class LineDrawer : MonoBehaviour
{
   // public string stageName;


    public GameObject start;
    public GameObject end;
    public GameObject Stage;


    public GameObject parentStage;
    public GameObject sphereObject;
    public GameObject trackingObject;
    public GameObject stage;
    int numbersOfSpheres;
    Quaternion quaternion = new();
    [SerializeField] GameObject TMPGameObject;
    [SerializeField] TextMeshPro text;
    Vector3 v1;
    Vector3 v2;
    public static bool firstTime = true;

    //Vector3 v3;
    DrawTest_V2 drawTest_V2;
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
    [SerializeField] TextMeshPro startCountDownText1;
    [SerializeField] TextMeshPro paintCountDownText;
    [SerializeField] TextMeshPro paintCountDownText1;
    [SerializeField] TextMeshPro playCountDownText;
    [SerializeField] TextMeshPro playCountDownText1;
    CheckIsMoved checker;

    [SerializeField] GameObject Stick;
    [SerializeField] GameObject drawer;
    [SerializeField] SABoneColliderBuilderInspector sa;
    [SerializeField] SABoneColliderBuilder builder;
    NewQuadScript newQuadScript;
    bool flag = true;

    float timer;
    public float timerDelay;
    public List<Vector3> points = new();
    public SAMeshCollider meshCollider;
    private SAMeshColliderEditorCommon.ReducerResult reducerResult;


    string defaultpath, additionalpath;
    string savepath, lastPath;
    bool sFlag = false;
    public static int lastSave = 0;

   // public PhotoTaker photoTaker;

    private void Awake()
    {
        text = TMPGameObject.GetComponent<TextMeshPro>();
        checker = GetComponent<CheckIsMoved>();
        newQuadScript = GameObject.FindObjectOfType<NewQuadScript>();
        // DrawTest_V2 = GetComponent<DrawTest_V2>();

        defaultpath = Application.dataPath + "/Positions/pos";
        additionalpath = ".json";
        lastPath = Application.dataPath + "/Positions/lastsave.json";

    }

    private void Start()
    {
        timer = timerDelay;

       
        for (int i = 0; i < 10; i++)
        {
            savepath = defaultpath + i + additionalpath;
            if (File.Exists(savepath))
                continue;
            Debug.Log("savepath = " + savepath);
            GameObject.Find("Always").GetComponent<Always>().saveSO.SetLastSaveInt();
            sFlag = true;
            break;
        }
        if (!sFlag)
        {
            savepath = defaultpath + GameObject.Find("Always").GetComponent<Always>().saveSO.GetLastSaveInt() + additionalpath;
        }
    }
    private void Update()
    {
        string datapath = defaultpath + additionalpath;
        Drawer();
    }

    private IEnumerator ChangeTime(int time)
    {
        startCountDownText.text = time.ToString();
        startCountDownText1.text = time.ToString();
        while (time >= 0)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("____________________________________________________"+time);
            time-= 1;
            startCountDownText.text = time.ToString();
            startCountDownText1.text = time.ToString();
        }
        canPaint = true;
        FindObjectOfType<SavePositions>().EndDrawing = false;
        startCount = false;
        startCountDownText.gameObject.SetActive(false);
        startCountDownText1.gameObject.SetActive(false);
        paintCountDownText.gameObject.SetActive(true);
        paintCountDownText1.gameObject.SetActive(true);
        paintCount = true;

    }
    private IEnumerator ChangePaintTime(int time)
    {
        paintCountDownText.text = time.ToString();
        paintCountDownText1.text = time.ToString();
        while (time >= 0)
        {
            yield return new WaitForSeconds(1);
            time-= 1;
            paintCountDownText.text = time.ToString();
            paintCountDownText1.text = time.ToString();
        }
        canPaint = false;
        FindObjectOfType<SavePositions>().EndDrawing = true;
                                                                                    Instantiate(end, trackingObject.transform.position, new Quaternion(0, 0, 0, 0));   //<---------------------endobject built here.
        //newQuadScript.verticesNumPoint();//---------------------------------------------------------------------------------------------中間点のScript
        int count = newQuadScript.wp.pos.Count;
        int savepoints = count / 10;
        for (int i = 0; i < count; i++)
        {
            if (i != 0 && i % savepoints == 0 && i < count - savepoints)
            {
                newQuadScript.CreatePoints(newQuadScript.wp.pos[i]);
            }
        }
        newQuadScript.saveManager.SetActive(true);
        paintCount = false;
        paintCountDownText.gameObject.SetActive(false);
        paintCountDownText1.gameObject.SetActive(false);
        playCountDownText.gameObject.SetActive(true);
        playCountDownText1.gameObject.SetActive(true);
        Stick.SetActive(true);                                    // <--------------------------Stick off
        drawer.SetActive(false);
        SAMeshColliderInspector.Process(meshCollider);           // <--------------------------create mesh here.
        MeshCollider mesh = meshCollider.transform.GetComponentInChildren<MeshCollider>();
        mesh.tag = "Stage";
        //mesh.transform.GetChild(0).tag = "Stage";
       // mesh.transform.GetChild(0).transform.GetChild(0).tag = "Stage";
        Component.Destroy(mesh.gameObject.GetComponent<Rigidbody>());
        /*SavePositions save = FindObjectOfType<SavePositions>();
        stageName = save.defaultserch + save.matubi.ToString("D3");
        if (save.saveFlag)
        {
            save.NewSaveNCMB(stageName, save.Vec3ToFloat(save.vec3pos));
            FindObjectOfType<Always>().tempDataFromMenu.ID = save.matubi;
            //save.matubi++;
        }          
        else
        {
            save.OverSaveNCMB(save.saveID, save.Vec3ToFloat(save.vec3pos));
            FindObjectOfType<Always>().tempDataFromMenu.ID = save.matubiStand;
            //save.matubi++;
        }*/
            
        //save.NewSaveNCMB("Stage:" + save.matubi, save.Vec3ToFloat(save.vec3pos));
        //newQuadScript.SaveTest(newQuadScript.wp, savepath); // <-----------------SaVE HERE 
        //photoTaker.CapturePhoto();                          //<-----------------capture photo HERE 
                                                            // SAMeshColliderEditorCommon.CreateCollider(meshCollider, reducerResult, false);
                                                            //sa.MakeMeshContents(builder);


    }
    public void Drawer()
    {
        //If the trigger button is pushed.      
        #region Start Count Down of 3 Sec;

        if (startCount)
        {
            StartCoroutine(ChangeTime(startCountDown_Sec));
            startCount = false;
        }

        #endregion

        if (paintCount && !startCount)
        {
            
            if (flag)
            {

                StartCoroutine(ChangePaintTime(paintCountDown_Sec));
                                                                                              Instantiate(start, trackingObject.transform.position,new Quaternion(0,0,0,0));   //<---------------------startobject built here.
                flag = false;
            }
            
        }

        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))   //&&checker.painterIsMoved
        {
            if (firstGrab)
            {
                startCount = true;
                firstGrab = false;
            }

            if (canPaint)
            {
                newQuadScript.makeStage();
                /*
                if (firstTime)
                {
                    newQuadScript.makeFirstLine(sphereObject.transform.position);
                    firstTime = false;
                }
                
                timer -= Time.deltaTime / 2;
                if (timer <= 0)
                {
                    newQuadScript.makeLine(sphereObject.transform.position);
                }
                
                */

                /*
                ring.SetActive(false);

                points.Add(trackingObject.transform.position);
                v1 = points[0];                                                  // first position
                                                                                 //StartCoroutine(WaitOneFrame());
                timer -= Time.deltaTime / 2;
                if (timer <= 0)
                {
                    points.Add(trackingObject.transform.position);
                    v2 = points[1];

                    //StartCoroutine(WaitOneFrame());
                    Instantiate(sphereObject, parentStage.transform);
                    sphereObject.GetComponent<DrawTest_V2>().SetV1(v1);
                    sphereObject.GetComponent<DrawTest_V2>().SetV2(v2);

                    //numbersOfSpheres++;
                    points.Clear();
                    points.Add(v2);
                    timer = timerDelay;



                }

                */
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

    }
}

