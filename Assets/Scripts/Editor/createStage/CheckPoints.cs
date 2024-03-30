using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public GameObject[] points;
    public bool[] saves;
    public int size;
    //public CheckPoints CP;

    // Start is called before the first frame update
    void OnEnable()
    {
        points = new GameObject[size];
        saves = new bool[size];

        for (int i = 0; i < saves.Length; i++)
        {
            saves[i] = false;
        }
        for (int i = 0; i < points.Length; i++)
        {
            string findname = "SAVEPOINT(Clone)";
            /*if(i > 0)
            {
                findname = "SavePoints(" + i.ToString() + ")";
                Debug.Log("findname = " + findname);
            }*/
            //findname = "SavePrefab(Clone)";
            points[i] = GameObject.Find(findname);
            GameObject g = GameObject.Find(findname);
            g.name = "Check" + i.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (points[0].GetComponent<CheckPrefabs>().passed && !saves[0])
            saves[0] = true;
        for (int i = points.Length - 1; i > 0; i--)
        {
            if (points[i].GetComponent<CheckPrefabs>().passed && saves[i - 1])
            {
                saves[i] = true;
            }
        }
    }

    public void Check(GameObject[] g)//この関数の引数は上にあるGameObject型の配列pointsで
    {
        //Vector3 v3 = g[0].transform.position;//最初はスタート地点に戻す
        for (int i = g.Length - 1; i > 0; i--)//ゴールに近い中間点からtrueの確認
        {
            if (saves[i])
            {
                g[i].transform.SetAsFirstSibling();
                g[i].GetComponent<MeshRenderer>().enabled = true;
                g[i].GetComponent<MeshRenderer>().material.color = Color.red;
                g[i].GetComponent<CheckPrefabs>().onCheck = true;
                //v3 = g[i].transform.position;//trueがあればbreakしてその座標を返す
                break;
            }
        }
        //return v3;
    }
}
