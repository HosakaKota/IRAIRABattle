using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using NCMB;
using System;
using UnityEditor.PackageManager;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using System.Linq;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using System.Drawing;
using System.Linq.Expressions;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEditor.SceneManagement;

[System.Serializable]
public class Wrapper
{
    //クラス単位で保存されるためクラス作成
    public List<Vector3> pos;    
}

public class SavePositions : MonoBehaviour
{
    public static SavePositions sp;
    public string[] Pathes;
    public NewQuadScript NQs;

    public List<Vector3> vec3pos = new List<Vector3>();
    public Vector3 pre_ncmb = new Vector3(1000, 1000, 1000);
    public int stage_num = 30;  //ステージ数上限
    public int demo_num = 2;  //デモステージ数上限
    public bool saveFlag = false;
    public string defaultserch = "Stage";
    public string saveID = "";
    public string savename = "";
    public string serchname = "";
    public int StageID;
    public int StageIDStand;

    public bool EndDrawing;

    private void Awake()
    {
        //ステージ保存先の決定

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Stages");
        Hashtable hTable = new Hashtable();
        string str = "Stage...";
        hTable.Add("$regex", str);
        query.WhereEqualTo("StageName", hTable);
        query.OrderByDescending("StageName");
        query.CountAsync((int count, NCMBException e) =>
        {
            if (e != null)
            {
                //エラー
                Debug.Log("saveserch error");
            }
            else
            {
                if(count < stage_num)
                {
                    savename = defaultserch + count.ToString();
                    StageID = count;
                    //Debug.Log("--------StagesCount = " + count);
                    //Debug.Log("savename = " + savename);
                    saveFlag = true;
                }
                else
                {
                    StageID = count;
                    //Debug.Log("more than stage_num. count = " + count);
                }
            }
        });

        
    }


    void Update()
    {
        //動かしていない時の座標を取得しないように
        if (SceneManager.GetActiveScene().name!="Title"&&!EndDrawing&&SceneManager.GetActiveScene().name!= "play")
        {
            NQs = FindObjectOfType<NewQuadScript>();
            if (pre_ncmb != NQs.ncmbs && NQs.ncmbs != new Vector3(100, 100, 100))
                vec3pos.Add(NQs.ncmbs);
            pre_ncmb = NQs.ncmbs;
        }
    }

    public List<float> Vec3ToFloat(List<Vector3> v3)
    {
        List<float> fl = new List<float>();
        for(int i = 0; i < v3.Count; i++)
        {
            fl.Add(v3[i].x);
            fl.Add(v3[i].y);
            fl.Add(v3[i].z);
        }
        return fl;
    }

    public static List<Vector3> DoubleArrayListToVector3(ArrayList value)
    {
        //NCMBにfloat型を保存するとdouble型になってしまうため読み込み時に変える
        float vf0;
        float vf1;
        float vf2;
        List<Vector3> v3 = new List<Vector3>();
        if (value.Count % 3 != 0)
        {
            Debug.LogError("Invalid Data");
            return default;
        }
        else
        {
            //Debug.Log("converting now");
            for (int i = 0; i < (value.Count / 3); i++)
            {
                vf0 = (float)System.Convert.ToDouble(value[i * 3]);
                vf1 = (float)System.Convert.ToDouble(value[(i * 3) + 1]);
                vf2 = (float)System.Convert.ToDouble(value[(i * 3) + 2]);
                v3.Add(new Vector3(vf0, vf1, vf2));
            }

            return v3;
        }
    }

    public void NewSaveNCMB(string stageName, List<float> fp)
    {
        NCMBObject ncmb_pos = new NCMBObject("Stages");

        ncmb_pos.AddRangeToList("vec3List", fp);
        ncmb_pos.Add("StageName", stageName);
        

        ncmb_pos.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                // 失敗
                //Debug.Log("newsave error");
            }
            else
            {
                //成功
                //Debug.Log("newsave success");
            }
        });
    }

    public void OverSaveNCMB(string stageID, List<float> fp)
    {
        ArrayList al = new ArrayList();
        NCMBObject ncmb_pos = new NCMBObject("Stages");
        ncmb_pos.ObjectId = stageID;
        ncmb_pos["vec3List"] = fp;
        ncmb_pos.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                // 失敗
                //Debug.Log("oversave error");
            }
            else
            {
                // 成功
                //Debug.Log("oversave success");
            }
        });
    }

    public void LoadNCMB(string ver, int num)
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Stages");

        string version = ver;
        string str = version + num.ToString("D3");
        query.WhereEqualTo("StageName", str);
        query.OrderByDescending("StageName");

        query.Limit = 1;

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                // 失敗
                //Debug.Log("download error");
            }
            else
            {
                //成功
                ArrayList al = objList[0]["vec3List"] as ArrayList;
                NQs = FindObjectOfType<NewQuadScript>();
                NQs.LoadStage(DoubleArrayListToVector3(al));
                //Debug.Log("download success");
            }
        });
    }

    
    public void RefreshOnlineStageID()
    {
        //続けて遊ぶ時に保存先を再検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Stages");
        Hashtable where = new Hashtable();
        string str = "Stage...";
        where.Add("$regex", str);
        query.WhereEqualTo("StageName", where);
        query.OrderByDescending("StageName");
        query.CountAsync((int count, NCMBException e) =>
        {
            if (e != null)
            {
                //失敗
                //Debug.Log("serch error");
            }
            else
            {
                if (count < stage_num)
                {
                    savename = defaultserch + count.ToString();
                    StageID = count;
                    //Debug.Log("StagesCount = " + count);
                    //Debug.Log("savename = " + savename);
                    saveFlag = true;
                    //Debug.Log("1--------------------------------------------------------------------------" + saveFlag);
                }
                else
                {
                    StageID = count;
                    //Debug.Log("more than stage_num. count = " + count);
                }
            }
        });
        if (StageID > 0)
        {
            query.OrderByAscending("updateDate");   //更新順でソート
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                saveID = objList[0].ObjectId;       //最も古いデータのIDを取得
                string name = objList[0]["StageName"].ToString();
                StageIDStand = ((name[name.Length - 2] - '0') * 10) + (name[name.Length - 1] - '0');
                //Debug.Log("saveID = " + saveID);
            });
        }
    }
}