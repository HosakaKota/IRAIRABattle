using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  

public class EndObject : MonoBehaviour
{

    public string stageName;

    PlayTimeManager timeManager;
    StartObject start;

    GameObject playCountDown;
    GameObject playCountDown1;

    GameObject EndEffect;
    GameObject EndEffect1;

    TextMeshPro EndText;
    TextMeshPro EndText1;

    GameObject EndTextParents;
    GameObject EndTextParents1;

    GameObject mesh;

    GameObject saveDataID;
    GameObject saveDataID1;

    public void Start()
    {
       
        timeManager = GameObject.FindObjectOfType<PlayTimeManager>();
       // if (SceneManager.GetActiveScene().name !="play")
       // {
            playCountDown = GameObject.Find("---------------UIs---------------------------").transform.Find("PlayCountDown").gameObject;
            playCountDown1 = GameObject.Find("---------------UIs---------------------------").transform.Find("PlayCountDown (1)").gameObject;

       // }

        EndEffect = GameObject.Find("---------------UIs---------------------------").transform.Find("EndEffect").gameObject;
        EndEffect1 = GameObject.Find("---------------UIs---------------------------").transform.Find("EndEffect1").gameObject;

        saveDataID = GameObject.Find("---------------UIs---------------------------").transform.Find("DATANUMBER").gameObject;
        saveDataID1 = GameObject.Find("---------------UIs---------------------------").transform.Find("DATANUMBER1").gameObject;


        EndTextParents = GameObject.Find("---------------UIs---------------------------").transform.Find("EndTextParents").gameObject;
        EndTextParents1 = GameObject.Find("---------------UIs---------------------------").transform.Find("EndTextParents (1)").gameObject;

        EndText = EndTextParents.transform.Find("EndText").GetComponent<TextMeshPro>();
        EndText1 = EndTextParents1.transform.Find("EndText").GetComponent<TextMeshPro>();

        mesh = GameObject.Find("mesh");

        start = GameObject.FindObjectOfType<StartObject>();
    }

    public bool CheckIfOkayToEnd()
    {
      CheckPrefabs[] checkPrefabs = GameObject.Find("parent").GetComponentsInChildren<CheckPrefabs>();
        int number = checkPrefabs.Length;

        int passTime = 0;
        for (int i = 0; i < checkPrefabs.Length; i++)
        {
            if (checkPrefabs[i].passed)
            {
                passTime++; 
            }
        }
        if (number==passTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stick"&& timeManager.startedGame&& CheckIfOkayToEnd())
        {
            if (SceneManager.GetActiveScene().name == "myScene")
            {
                SavePositions save = FindObjectOfType<SavePositions>();
                stageName = save.defaultserch + save.matubi.ToString("D3");
                if (save.saveFlag)
                {
                    save.NewSaveNCMB(stageName, save.Vec3ToFloat(save.vec3pos));
                    //save.NewSaveNCMB("Demo101", save.Vec3ToFloat(save.vec3pos));
                    Always always = FindObjectOfType<Always>();
                    always.tempDataFromMenu = always.photoSOs[save.matubi];
                    always.tempDataFromMenu.ID = save.matubi;
                    //save.matubi++;
                }
                else
                {
                    save.OverSaveNCMB(save.saveID, save.Vec3ToFloat(save.vec3pos));
                    Always always = FindObjectOfType<Always>();
                    always.tempDataFromMenu = always.photoSOs[save.matubiStand];
                    always.tempDataFromMenu.ID = save.matubiStand;
                    //save.matubi++;
                }
            }
            End();
        }
    }

   public void End()
    {
        timeManager.OnEndPlay();
       // if (SceneManager.GetActiveScene().name != "play")
       // {
            playCountDown.SetActive(false);
            playCountDown1.SetActive(false);
      //  }
        EndEffect.SetActive(true);
        EndEffect1.SetActive(true);

        TextMeshPro[] texts = EndTextParents.GetComponentsInChildren<TextMeshPro>();
        TextMeshPro[] texts1 = EndTextParents1.GetComponentsInChildren<TextMeshPro>();

        texts[1].text = timeManager.time.ToString();
        texts1[1].text = timeManager.time.ToString();
        EndTextParents.SetActive(true);
        EndTextParents1.SetActive(true);
        //show the data int here by UI;
        // }
        //  else
        //  {
        //      timeManager.Win();
        //   }

        //mesh.GetComponent<MeshRenderer>().enabled = false;
        //mesh.transform.GetChild(0).transform.GetChild(0).GetComponent<MeshCollider>().enabled = false;
        if (SceneManager.GetActiveScene().name != "play")
        {
            ShowIDAfterCreateStage();
        }
        //FindObjectOfType<CollisionTest>().enabled = false;
        mesh.SetActive(false);
        GameObject.Find("Stick (1)").SetActive(false);
        Destroy(gameObject);
    }

    public void WinGameDisplay()
    {
        EndEffect = GameObject.Find("---------------UIs---------------------------").transform.Find("EndEffect").gameObject;
        EndEffect1 = GameObject.Find("---------------UIs---------------------------").transform.Find("EndEffect1").gameObject;

        saveDataID = GameObject.Find("---------------UIs---------------------------").transform.Find("DATANUMBER").gameObject;
        saveDataID1 = GameObject.Find("---------------UIs---------------------------").transform.Find("DATANUMBER1").gameObject;


        EndTextParents = GameObject.Find("---------------UIs---------------------------").transform.Find("EndTextParents").gameObject;
        EndTextParents1 = GameObject.Find("---------------UIs---------------------------").transform.Find("EndTextParents (1)").gameObject;

        EndText = EndTextParents.transform.Find("EndText").GetComponent<TextMeshPro>();
        EndText1 = EndTextParents1.transform.Find("EndText").GetComponent<TextMeshPro>();
        EndEffect.GetComponent<TextMeshPro>().color = Color.blue;
        EndEffect1.GetComponent<TextMeshPro>().color = Color.blue;
        EndEffect.SetActive(true);
        EndEffect1.SetActive(true);

        EndText.color = Color.blue;
        EndText1.color = Color.blue;

        TextMeshPro[] texts = EndTextParents.GetComponentsInChildren<TextMeshPro>();
        TextMeshPro[] texts1 = EndTextParents1.GetComponentsInChildren<TextMeshPro>();

        texts[1].text = timeManager.time.ToString();
        texts1[1].text = timeManager.time.ToString();
        EndTextParents.SetActive(true);
        EndTextParents1.SetActive(true);
    }

    public void LoseGameDisplay()
    {
        EndEffect = GameObject.Find("---------------UIs---------------------------").transform.Find("EndEffect").gameObject;
        EndEffect1 = GameObject.Find("---------------UIs---------------------------").transform.Find("EndEffect1").gameObject;

        saveDataID = GameObject.Find("---------------UIs---------------------------").transform.Find("DATANUMBER").gameObject;
        saveDataID1 = GameObject.Find("---------------UIs---------------------------").transform.Find("DATANUMBER1").gameObject;


        EndTextParents = GameObject.Find("---------------UIs---------------------------").transform.Find("EndTextParents").gameObject;
        EndTextParents1 = GameObject.Find("---------------UIs---------------------------").transform.Find("EndTextParents (1)").gameObject;

        EndText = EndTextParents.transform.Find("EndText").GetComponent<TextMeshPro>();
        EndText1 = EndTextParents1.transform.Find("EndText").GetComponent<TextMeshPro>();
        EndEffect.GetComponent<TextMeshPro>().color = Color.red;
        EndEffect1.GetComponent<TextMeshPro>().color = Color.red;
        EndEffect.GetComponent<TextMeshPro>().text = "Unfortunately";
        EndEffect1.GetComponent<TextMeshPro>().text = "Unfortunately";
        EndEffect.SetActive(true);
        EndEffect1.SetActive(true);


        EndText.color = Color.red;
        EndText1.color = Color.red;

        TextMeshPro[] texts = EndTextParents.GetComponentsInChildren<TextMeshPro>();
        TextMeshPro[] texts1 = EndTextParents1.GetComponentsInChildren<TextMeshPro>();

        texts[1].text = timeManager.time.ToString();
        texts1[1].text = timeManager.time.ToString();

        EndTextParents.SetActive(true);
        EndTextParents1.SetActive(true);
    }


    public void ShowIDAfterCreateStage()
    {
        TextMeshPro[] texts = saveDataID.GetComponentsInChildren<TextMeshPro>();
        TextMeshPro[] texts1 = saveDataID1.GetComponentsInChildren<TextMeshPro>();
        SavePositions save = FindObjectOfType<SavePositions>();
        if (save.saveFlag)
        {
            int number = FindObjectOfType<SavePositions>().matubi+1;//-2
            texts[1].text = "Stage: " + number.ToString();
            texts1[1].text = "Stage: " + number.ToString();
        }
        else
        {
            int number = FindObjectOfType<SavePositions>().matubiStand+1;
            texts[1].text = "Stage: " + number.ToString();
            texts1[1].text = "Stage: " + number.ToString();
        }
        
        

        saveDataID.SetActive(true);
        saveDataID1.SetActive(true);
    }


  
}
