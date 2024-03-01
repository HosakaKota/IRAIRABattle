using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData:", menuName = "New StageData")]
public class PhotoSO : ScriptableObject
{
    string stageName;
    [SerializeField]int clearTime;
    [SerializeField]int id;

    public string StageName
    {
        get { return stageName; }
        set { stageName = value; }
    }

    public int ClearTime
    {
        get { return clearTime; }
        set { clearTime = value; }
    }

    public int ID
    {
        get { return id; }
        set { id = value; }
    }



}
