using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayListManager : MonoBehaviour
{
    public Vector2 vectorR_PlayList;
    List<PlayListContent> datas = new();
    public static int stageID = 0;
    List<GameObject> UIs = new();
    int page = 0;  // 0,1,2,3→4,5,6,7→8,9,10,11...
    public int number = 0;
    public float playListTime;
    public float SetTime = 0.1f;
    public float buttonSpeed;

    [Header("Colors")]
    [SerializeField] Color initiateColor;
    [SerializeField] Color choiceColor;

    private void Start()
    {
        InitiateList();
        RefreshPlayList();
    }

    public void AddtoPlayList(PlayListContent playListContent)
    {
        datas.Add(playListContent);
    }

    public void InitiateList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AddtoPlayList(transform.GetChild(i).gameObject.GetComponent<PlayListContent>());
            UIs.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void RefreshPlayList()
    {
        // reset all childs
        for (int J = 0; J < transform.childCount; J++)
        {
            transform.GetChild(J).gameObject.SetActive(false);
        }


        for (int i = 0; i < 4; i++)
        {

            if (page * 4 + i < transform.childCount)
            {
                transform.GetChild(page * 4 + i).gameObject.SetActive(true);
                datas[page * 4 + i].InitiateContent();
            }
            else
            {
                page--;
                RefreshPlayList();
            }

        }
    }

    private void Update()
    {
        vectorR_PlayList = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        if (PlayListWaitTime())
        {
            if (vectorR_PlayList.y < -buttonSpeed)
            {
                VerticleChoose(true);
                return;
            }

            if (vectorR_PlayList.y > buttonSpeed)
            {
                VerticleChoose(false);
                return;
            }

            if (vectorR_PlayList.x < -buttonSpeed)
            {
                HorizontalChoose(false);
                return;
            }

            if (vectorR_PlayList.x > buttonSpeed)
            {
                HorizontalChoose(true);
                return;
            }
        }
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            Debug.Log(number + 4 * page - 2 + "-------------------" + FindObjectOfType<SavePositions>().StageID);
            if (number + 4 * page - 2 < FindObjectOfType<SavePositions>().StageID || number + 4 * page - 2 == 0)  //  0  2
            {
                SceneManager.LoadScene("play");
                FindObjectOfType<Always>().tempDataFromMenu = transform.GetChild(number + page * 4 + 1).GetComponent<PlayListContent>().stageData;
            }
            else
            {
                //Debug.Log("reddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd");
                transform.GetChild(number + 4 * page).GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            }

        }
    }

    void VerticleChoose(bool UpOrDown)
    {
        if (UpOrDown)
        {
            number += 2;
            if (number > 3)
            {
                page++;
                RefreshPlayList();
                number -= 4;
            }
        }
        else
        {
            number -= 2;
            if (number < 0)
            {
                page--;
                if (page < 0)
                {
                    page = 0;
                }
                RefreshPlayList();
                number += 4;
            }
        }
        ColoringChoice(number);
    }

    void HorizontalChoose(bool LeftOrRight)
    {
        if (LeftOrRight) // to right
        {
            if (number == 0)
            {
                number = 1;
            }
            else if (number == 2)
            {
                number = 3;
            }
        }
        else  // to left
        {

            if (number == 1)
            {
                number = 0;
            }
            else if (number == 3)
            {
                number = 2;
            }
        }
        ColoringChoice(page * 4 + number);
    }


    bool PlayListWaitTime()
    {
        playListTime += Time.deltaTime;

        if (playListTime > SetTime)
        {
            playListTime = 0;

            return true;

        }
        else
        {
            return false;
        }

    }

    void ColoringChoice(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)   // is this gameobject active?
            {
                if (i - page * 4 == number)
                {
                    transform.GetChild(i).gameObject.GetComponent<Image>().color = choiceColor;
                }
                else
                {
                    transform.GetChild(i).gameObject.GetComponent<Image>().color = initiateColor;
                }
            }
        }
    }
}
