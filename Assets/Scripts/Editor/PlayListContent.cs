using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayListContent : MonoBehaviour
{
    public string stageName;
    public string sceneName;
    public PhotoSO stageData;
    public bool isDemo;

    public void InitiateContent()
    {
        if (!isDemo)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = (stageData.ID + 1).ToString();
        }

    }

}
