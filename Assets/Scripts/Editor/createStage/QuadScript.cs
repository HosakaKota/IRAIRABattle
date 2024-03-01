using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuadScript : MonoBehaviour
{

    const float cons = 2;//半径の2倍(直径)

    public BoxMove tes;
    public float slash = 1.0f;//斜辺の長さ（ラインの正確具合）
    Vector3 tmp = new Vector3(0, 0, 0)
        , tp = new Vector3(0, 0, 0);//tpは開始地点

    public Material mat;
    Mesh mesh;
    Vector3 Vofset;//移動ベクトル
    Vector3 points = new Vector3();
    List<Vector3> vertices = new List<Vector3>();//ポリゴンの座標リスト

    List<Vector2> uvs = new List<Vector2>();//ポリゴン描写の
    float xoffset = 0;

    List<int> tris = new List<int>();//uv貼り付けリスト
    int offset = 0;

    float xofset = 1.2f;//半径の大きさ


    public float time, SetTime = 3;

    void Start()
    {
        // makebox();
    }
    void Update()
    {

        makeStage();

    }

    public void makeStage()
    {
        //左クリック最初の
        if (Input.GetMouseButtonDown(0))
        {
            tp = tes.targetPosition();//
            makeFirstLine(tp);
        }
        else if (Input.GetMouseButton(0))//その後の左クリック
        {
            tmp = tes.targetPosition();
            float slashTmp = DistanceDifference(tp.x - tmp.x, tp.y - tmp.y);//

            if (Mathf.Abs(slashTmp) > slash)//一定範囲内であれば
            {
                Debug.Log(tes.targetPosition());
                makeLine(tp);
                tp = tmp;//開始ポジションを更新
            }
        }

    }

    public float DistanceDifference(float X, float Y)
    {
        float Distance = Mathf.Sqrt(Mathf.Pow(X, 2) + Mathf.Pow(Y, 2));
        return Distance;
    }//斜辺の値を返す

    public void makebox()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[] {
        new Vector3 (-1.2f, -1.2f, 0),
        new Vector3 (-1.2f,  1.2f, 0),
        new Vector3 (1.2f , -1.2f, 0),
        new Vector3 (1.2f ,  1.2f, 0),


    };

        mesh.uv = new Vector2[] {
        new Vector2 (0, 0),
        new Vector2 (0, 1),
        new Vector2 (1, 0),
        new Vector2 (1, 1),


    };

        mesh.triangles = new int[] {
        0, 1, 2,
        1, 3, 2,

    };

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshRenderer>().material = mat;

    }

    public void makeFirstLine(Vector3 tp)
    {
        Vofset = tp;//開始地点のベクトル
        Debug.Log(Vofset);
        // 開始点を保存
        points = Vofset;

        // 頂点を２つ生成
        this.vertices.Add(Vofset + new Vector3(0, -xofset, 0));//一定の幅の頂点を制作
        this.vertices.Add(Vofset + new Vector3(0, xofset, 0));


        // uv座標を設定
        this.uvs.Add(new Vector2(0, 0));
        this.uvs.Add(new Vector2(0, 1));
        xoffset += 1;


        this.offset = 0;

        // メッシュ生成
        this.mesh = new Mesh();
    }
    public void makeLine(Vector3 Vofset2)
    {

        Vector2 plus90 = new Vector2(Vofset2.x, Vofset2.y) + new Vector2(xofset * cons, xofset);
        Vector2 minus90 = new Vector2(Vofset2.x, Vofset2.y) + new Vector2(xofset * cons, -xofset);


        Vofset += new Vector3(xofset * cons, xofset * cons, 0);//移動ベクトル



        //  Debug.Log("plus90 =" + plus90);
        //  Debug.Log("minus90 =" + minus90);


        // 頂点を追加
        this.vertices.Add(minus90);
        this.vertices.Add(plus90);

        // UVを追加
        this.uvs.Add(new Vector2(xoffset, 0));
        this.uvs.Add(new Vector2(xoffset, 1));
        xoffset += 1;////uScrollSpeed; 
                     // Debug.Log(xoffset);
        /*
        mesh.uv = new Vector2[] {
        new Vector2 (0, 0),
        new Vector2 (0, 1),
        new Vector2 (1, 0),
        new Vector2 (1, 1),
    };*/


        // インデックスを追加
        this.tris.Add(offset);
        this.tris.Add(offset + 1);
        this.tris.Add(offset + 2);

        this.tris.Add(offset + 1);
        this.tris.Add(offset + 3);
        this.tris.Add(offset + 2);

        offset += 2;

        /*
        mesh.triangles = new int[] {
        0, 1, 2,
        1, 3, 2,
    };*/


        mesh.vertices = this.vertices.ToArray();
        mesh.uv = this.uvs.ToArray();
        mesh.triangles = this.tris.ToArray();


        foreach (Vector3 verticesName in vertices)
        {
            Debug.Log("verticesName = " + verticesName);
        }/*
        foreach (Vector2 uvName in uvs)
        {
            Debug.Log("uvName = " + uvName);
        }*/
        foreach (int trisName in tris)
        {
            Debug.Log("trisvName = " + trisName);
        }


        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshRenderer>().material = mat;

    }

    /*一定秒数える関数。
    bool waitTime()
    {
        time += Time.deltaTime;

        if (time > SetTime)
        {
            time = 0;

            return true;

        }
        else
        {
            return false;
        }

    }
    */

}