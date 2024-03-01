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
    //public string name;
    //public string path;
    public List<Vector3> pos;    
}

public class SavePositions : MonoBehaviour
{
    public static SavePositions sp;
    public string[] Pathes;
    public NewQuadScript NQs;

    //曐懚梡.
    //public Wrapper wp = new Wrapper();
    //string defaultpath, additionalpath, savepath, lastPath;
    //public Vector3 zure = new Vector3(0, 0, 0);
    //bool sFlag = false;
    //int lastSave = 0;

    public List<Vector3> vec3pos = new List<Vector3>();
    public List<double> dblpos = new List<double>();
    public Vector3 pre_ncmb = new Vector3(1000, 1000, 1000);
    public int stage_num = 30;  //僗僥乕僕梡
    public int demo_num = 2;  //僨儌梡
    public bool saveFlag = false;
    public string defaultserch = "Stage";
    public string saveID = "";
    public string savename = "";
    public string serchname = "";
    public int matubi;
    public int matubiStand;

    public bool EndDrawing;

    private void Awake()
    {
        //曐懚愭偺寁嶼

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
                // 幐攕
                Debug.Log("saveserch error");
            }
            else
            {
                if(count < stage_num)
                {
                    savename = defaultserch + count.ToString();
                    matubi = count;
                    Debug.Log("--------StagesCount = " + count);
                    Debug.Log("savename = " + savename);
                    saveFlag = true;
                    Debug.Log("1--------------------------------------------------------------------------" + saveFlag);
                }
                else
                {
                    matubi = count;
                    Debug.Log("more than stage_num. count = " + count);
                }
            }
        });

        
    }

    /*private void Start()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Stages");
        Debug.Log("2--------------------------------------------------------------------------"+saveFlag);
        if (!saveFlag)
        {
            Hashtable where = new Hashtable();
            string str = "Stage...";
            where.Add("$regex", str);
            query.WhereEqualTo("StageName", where);
            query.OrderByAscending("updateDate");   //峏怴擔帪偺徃弴
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                saveID = objList[0].ObjectId;       //最も古いデータのIDを取得
                string name = objList[0]["StageName"].ToString();
                matubiStand = (ChartoInt(name[name.Length - 2]) * 10) + ChartoInt(name[name.Length - 1]);
                Debug.Log("saveID = " + saveID);
            });
        }
    }*/
    int ChartoInt(char c)
    {
        switch (c)
        {
            case '0': return 0;
            case '1': return 1;
            case '2': return 2;
            case '3': return 3;
            case '4': return 4;
            case '5': return 5;
            case '6': return 6;
            case '7': return 7;
            case '8': return 8;
            case '9': return 9;
            default: return 0;
        }
    }
    #region
    void Update()
    {
        if (SceneManager.GetActiveScene().name!="Title"&&!EndDrawing&&SceneManager.GetActiveScene().name!= "play")
        {
            NQs = FindObjectOfType<NewQuadScript>();
            if (pre_ncmb != NQs.ncmbs && NQs.ncmbs != new Vector3(100, 100, 100))
                vec3pos.Add(NQs.ncmbs);
            pre_ncmb = NQs.ncmbs;
        }
        
        /*
        //Debug.Log("saveFlag = " + saveFlag);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (saveFlag)
                NewSaveNCMB(savename, Vec3ToFloat(vec3pos));
            else
            {
                OverSaveNCMB(saveID, Vec3ToFloat(vec3pos));
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            LoadNCMB("Stage", 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadNCMB("Stage", 1);
        }*/
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    LoadNCMB("Stage", 2);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    LoadNCMB("Stage", 3);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    LoadNCMB("Stage", 4);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    LoadNCMB("Stage", 5);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    LoadNCMB("Stage", 6);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    LoadNCMB("Stage", 7);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //    LoadNCMB("Stage", 8);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    LoadNCMB("Stage", 9);
        //}
    }

    //void SaveToCloud(List<Vector3> v3)
    //{
    //    float[,] flpos = new float[v3.Count,3];
    //    for(int i = 0; i < v3.Count; i++)
    //    {
    //        flpos[i, 0] = v3[i].x;
    //        flpos[i, 1] = v3[i].y;
    //        flpos[i, 2] = v3[i].z;
    //    }

    //    byte[,] posdata = new byte[v3.Count,3];
    //    posdata[0,2] = BitConverter.GetBytes(flpos[0, 1]);
    //}
    #endregion
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

    //List<Vector3> FloatToVec3(List<float> fl)
    //{
    //    int v_size = fl.Count / 3;
    //    List<Vector3> v3 = new List<Vector3>();
    //    for(int i = 0; i < v_size; i++)
    //    {
    //        v3.Add(new Vector3(fl[i], fl[i + 1], fl[i + 2]));
    //    }
    //    return v3;
    //}

    //List<Vector3> ArrayToVec3(ArrayList al)
    //{
    //    int v_size = al.Count / 3;
    //    List<Vector3> v3 = new List<Vector3>();
    //    for (int i = 0; i < v_size; i++)
    //    {
    //        v3.Add(new Vector3((float)al[i], (float)al[i + 1], (float)al[i + 2]));
    //    }
    //    return v3;
    //}

    public static List<Vector3> DoubleArrayListToVector3(ArrayList value)
    {
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
            //(float)(double)value[1]偲傗傞偲抣偑0偺応崌巰偸//

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
                // 幐攕
                Debug.Log("newsave error");
            }
            else
            {
                //惉岟
                Debug.Log("newsave success");
            }
        });
    }

    public void OverSaveNCMB(string stageID, List<float> fp)
    {
        ArrayList al = new ArrayList();
        double[] dl = new double[al.Count];
        NCMBObject ncmb_pos = new NCMBObject("Stages");
        ncmb_pos.ObjectId = stageID;
        ncmb_pos["vec3List"] = fp;
        ncmb_pos.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                // 幐攕
                Debug.Log("oversave error");
            }
            else
            {
                //惉岟
                Debug.Log("oversave success");
            }
        });
    }

    public void LoadNCMB(string D_or_S, int num)
    {
        Debug.Log("------------num" + num);
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Stages");

        //Hashtable where = new Hashtable();
        string version = D_or_S;
        /*string sub = ".";
        if (num > 9)
            sub = ".." + num.ToString();
        else
            sub = "." + num.ToString();*/
        string str = version + num.ToString("D3");
        //where.Add("$regex", str);
        query.WhereEqualTo("StageName", str);
        query.OrderByDescending("StageName");

        query.Limit = 1;

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                // 幐攕
                Debug.Log("download error");
            }
            else
            {
                //惉岟
                ArrayList al = objList[0]["vec3List"] as ArrayList;//(ArrayList)objList[0]["vec3List"];
                NQs = FindObjectOfType<NewQuadScript>();
                NQs.LoadStage(DoubleArrayListToVector3(al));
                Debug.Log("download success");
            }
        });
    }

    void WhereSave(int i)
    {
        //曐懚愭偺寁嶼
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Stages");
        string savename = "";
        string serchname = "";
        string defaultserch = "Stage00";
        int listcount = 100;
        serchname = defaultserch + i.ToString();
        Debug.Log(i + ":" + "serchname = " + serchname);
        query.WhereEqualTo("StageName", serchname);
        query.CountAsync((int count, NCMBException e) =>
        {
            if (e != null)
            {
                // 幐攕
                Debug.Log("saveserch error");
            }
            else
            {
                listcount = count;
                Debug.Log("i = " + i + " count :" + count);
            }
        });
        if (listcount == 0)
        {
            savename = serchname;
            saveFlag = true;
            Debug.Log("saveneme = " + savename);
            //return savename;
        }
        if (listcount != 0)
        {
            Debug.Log("stage00" + i + " = not 0");
        }
        //return "";
    }

    public void RefreshOnlineMatubi()
    {
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
                // 幐攕
                Debug.Log("saveserch error");
            }
            else
            {
                if (count < stage_num)
                {
                    savename = defaultserch + count.ToString();
                    matubi = count;
                    //Debug.Log("StagesCount = " + count);
                    //Debug.Log("savename = " + savename);
                    saveFlag = true;
                    //Debug.Log("1--------------------------------------------------------------------------" + saveFlag);
                }
                else
                {
                    matubi = count;
                    //Debug.Log("more than stage_num. count = " + count);
                }
            }
        });
        if (matubi > 0)
        {
            //Hashtable where = new Hashtable();
            //string str = "Stage...";
            //where.Add("$regex", str);
            //query.WhereEqualTo("StageName", where);
            query.OrderByAscending("updateDate");   //峏怴擔帪偺徃弴
            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {
                saveID = objList[0].ObjectId;       //最も古いデータのIDを取得
                string name = objList[0]["StageName"].ToString();
                matubiStand = (ChartoInt(name[name.Length - 2]) * 10) + ChartoInt(name[name.Length - 1]);
                //Debug.Log("saveID = " + saveID);
            });
        }
    }
}