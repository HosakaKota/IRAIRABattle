using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayTimeManager : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [SerializeField] TextMeshPro text1;
    public int time = 0;
    public int missTime = 5;
    public int protectTime = 1;
    public bool playing = true;
    bool protectionFlag;
    public bool startedGame;

    public Material defualtColor;
    public Material missColor;
    public MeshRenderer mesh;

    public PhotoSO photoSO;

    bool winTheGame;

    public GameObject[] hints;
    bool CanJumpToMenu;

    SavePositions savePositions;
    private void Start()
    {
        photoSO = FindObjectOfType<Always>().tempDataFromMenu;
        savePositions = FindObjectOfType<SavePositions>();
        if (SceneManager.GetActiveScene().name == "play")
        {
            if (photoSO.ID > 99)
            {
                savePositions.LoadNCMB("Demo", photoSO.ID);
            }
            else
            {
                savePositions.LoadNCMB("Stage", photoSO.ID - 1);
            }
            text.gameObject.SetActive(true);
            text1.gameObject.SetActive(true);
        }

    }

    public void CreateCollider()
    {
        SAMeshCollider meshCollider = FindObjectOfType<SAMeshCollider>();
        SAMeshColliderInspector.Process(meshCollider);
        MeshCollider mesh = meshCollider.transform.GetComponentInChildren<MeshCollider>();
        mesh.tag = "Stage";
    }

    public void OnBeginPlay()
    {
        StartCoroutine(RecordPlayTime());
    }

    IEnumerator RecordPlayTime()
    {
        while (playing)
        {
            yield return new WaitForSeconds(1);
            time += 1;
            text.text = time.ToString();
            text1.text = time.ToString();
        }

    }

    public void OnEndPlay()
    {
        playing = false;
        StopCoroutine(RecordPlayTime());
        if (SceneManager.GetActiveScene().name == "play")
        {
            if (time < photoSO.ClearTime)
            {
                winTheGame = true;
            }
            else
            {
                winTheGame = false;
            }
        }
        else
        {
            photoSO.ClearTime = time;

        }
        ShowHints();
    }

    public void OnMissPlay()
    {
        if (!protectionFlag)
        {
            mesh.material = missColor;
            protectionFlag = true;
            time += missTime;
            text.text = time.ToString();
            text1.text = time.ToString();
            Transform parent = GameObject.Find("parent").transform;
            int childnumber = parent.childCount;
            GameObject g1 = parent.GetChild(childnumber - 1).gameObject;
            g1.transform.SetAsFirstSibling();
            g1.GetComponent<CheckPrefabs>().onCheck = true;
            g1.GetComponent<MeshRenderer>().enabled = true;
            g1.GetComponent<MeshRenderer>().material.color = Color.red;

        }

    }

    public void StartProtectingSystem(int number)
    {
        StartCoroutine(ProtectTime());
    }

    IEnumerator ProtectTime()
    {
        ColliderNon non = FindObjectOfType<ColliderNon>();
        non.Disabled_Collider();
        yield return new WaitForSeconds(2);
        non.Enabled_Collider();
    }

    public void OnInvinsible()            //無敵
    {
        CapsuleCollider[] colliders = FindObjectsOfType<CapsuleCollider>();
        List<CapsuleCollider> stickColliders = new();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Collider")
            {
                stickColliders.Add(colliders[i]);
            }
        }
        for (int j = 0; j < stickColliders.Count; j++)
        {
            stickColliders[j].enabled = false;
        }
    }

    public void OutInvinsible()　　　　　//無敵じゃない
    {
        CapsuleCollider[] colliders = FindObjectsOfType<CapsuleCollider>();
        List<CapsuleCollider> stickColliders = new();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Collider")
            {
                stickColliders.Add(colliders[i]);
            }
        }
        for (int j = 0; j < stickColliders.Count; j++)
        {
            stickColliders[j].enabled = true;
        }
    }

    public void UnPortected()
    {
        protectionFlag = false;
        mesh.material = defualtColor;
        StartCoroutine(ProtectTime());
    }


    public void Win()
    {
        EndObject end = GameObject.FindObjectOfType<EndObject>();
        if (winTheGame)
        {
            end.WinGameDisplay();
            ShowHints();
        }
        else
        {
            end.LoseGameDisplay();
            ShowHints();
        }
    }

    public void ShowHints()
    {
        for (int i = 0; i < hints.Length; i++)
        {
            hints[i].SetActive(true);
        }
        CanJumpToMenu = true;
    }


    private void Update()
    {
        if (CanJumpToMenu)
        {
            if (OVRInput.Get(OVRInput.RawButton.B))
            {
                //unload

                SceneManager.LoadScene(0);
                CanJumpToMenu = false;
                savePositions.RefreshOnlineMatubi();
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            FindObjectOfType<EndObject>().End();
        }
    }
}