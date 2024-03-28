using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EightScript : MonoBehaviour
{
    const float pai = 0.71f;

    //基礎的な変数
    const float cons = 2;//半径の2倍(直径)

    public BoxMove tes;
    public float slash = 1.0f;//斜辺の長さ（ラインの正確具合）
    Vector3 tmp = new Vector3(0, 0, 0)
        , tp = new Vector3(0, 0, 0);//tpは開始地点
    public Vector3 VecFront, VecBack;//次のオブジェクト生成　への　ベクトルの角度
    public float time, SetTime = 3;//Debugように使ってた変数


    //ポリゴンからのオブジェクト生成時に必要な変数
    public Material mat;
    Mesh mesh;
    Vector3 Vofset;//移動ベクトル
    Vector3 points = new Vector3();
    List<Vector3> vertices = new List<Vector3>();//ポリゴンの座標リスト

    List<Vector2> uvs = new List<Vector2>();//ポリゴン描写の
    float xoffset = 0;

    List<int> tris = new List<int>();//uv貼り付けリスト
    int offset = 0;

    float ofset = 0.0f,
           xofset = 1.2f;//半径の大きさ



    void Start()
    {
        //makebox();
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
            float slashTmp = DistanceDifference(tp.x - tmp.x, tp.y - tmp.y);//次のオブジェクト生成までの距離


            //VecBack = VecQy(tp, tmp);//奥行ベクトルベクトルの角度

            Debug.Log("VecQx" + VecFront);
            if (Mathf.Abs(slashTmp) > slash)//一定範囲内であれば
            {

                VecFront = VecQx(tp, tmp);//

                // Debug.Log(tes.targetPosition());
                makeLine(tp);
                tp = tmp;//開始ポジションを更新
            }
        }

    }

    public Vector3 VecQx(Vector3 tp, Vector3 tmp)
    {
        Vector3 dt = tmp - tp;
        /*
        float rad = Mathf.Atan2(dt.y, dt.x);
        float vecX = rad * Mathf.Rad2Deg;
        return (int)vecX;
        */
        Vector3 vec = dt.normalized;
        return vec;
    }//正面ベクトルの角度を返す。
    public int VecQy(Vector3 tp, Vector3 tmp)
    {
        Vector3 dt = tp - tmp;
        float rad = Mathf.Atan2(dt.z, dt.x);
        float vecY = rad * Mathf.Rad2Deg;
        return (int)vecY;
    }//奥行ベクトルの角度を返す。

    public Vector3 VecD(Vector3 tp, Vector3 tmp)
    {
        Vector3 vec = tp + tmp;
        return vec;
    }//二点間ベクトルを返す

    public float DistanceDifference(float X, float Y)
    {
        float Distance = Mathf.Sqrt(Mathf.Pow(X, 2) + Mathf.Pow(Y, 2));
        return Distance;
    }//斜辺の値を返す

    float a = 1.2f, UVoffset = 0;
    public void makebox()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[] {
        new Vector3 (-a, 0, a),
        new Vector3 (a,  0, a),
        new Vector3 (-a, a*pai, a*pai),
        new Vector3 (a, a*pai, a*pai),
        new Vector3 (-a , a, 0),
        new Vector3 (a ,  a, 0),
        new Vector3 (-a, a*pai, -a*pai),
        new Vector3 (a, a*pai, -a*pai),

        new Vector3 (-a, 0, -a),
        new Vector3 (a, 0, -a),
        new Vector3 (-a, -a*pai, -a*pai),
        new Vector3 (a, -a*pai, -a*pai),
        new Vector3 (-a , -a, 0),
        new Vector3 (a , -a, 0),
        new Vector3 (-a, -a*pai, a*pai),
        new Vector3 (a, -a*pai, a*pai),
    };

        mesh.uv = new Vector2[] {
            new Vector2(0, 0),
            new Vector2(0, 1),

            new Vector2(1, 0),
            new Vector2(1, 1),

            new Vector2(2, 0),
            new Vector2(2, 1),

            new Vector2(3, 0),
            new Vector2(3, 1),

            new Vector2(4, 0),
            new Vector2(4, 1),

            new Vector2(5, 0),
            new Vector2(5, 1),

            new Vector2(6, 0),
            new Vector2(6, 1),

            new Vector2(7, 0),
            new Vector2(7, 1),

                };



        mesh.triangles = new int[] {
        0, 8, 1,
        8,9,1,


        1,9,2,
        9,10,2,

        2,10,3,
        10,11,3,

        3,11,4,
        11,12,4,

        4,12,5,
        12,13,5,

        5,13,6,
        13,14,6,

       6,14,7,
        14,15,7,

       7,15,0,
        15,8,0,

        };

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshRenderer>().material = mat;

    }
    //↑使わないけど参考に

    public void makeFirstLine(Vector3 tp)
    {
        Vofset = tp;//開始地点のベクトル
        //Debug.Log(Vofset);
        // 開始点を保存
        points = Vofset;

        // 頂点を4つ生成
        //一定の幅の頂点を制作
        this.vertices.Add(Vofset + new Vector3(0, 0, xofset));
        this.vertices.Add(Vofset + new Vector3(0, xofset * pai, xofset * pai));
        this.vertices.Add(Vofset + new Vector3(0, xofset, 0));
        this.vertices.Add(Vofset + new Vector3(0, xofset * pai, -xofset * pai));
        this.vertices.Add(Vofset + new Vector3(0, 0, -xofset));
        this.vertices.Add(Vofset + new Vector3(0, -xofset * pai, -xofset * pai));
        this.vertices.Add(Vofset + new Vector3(0, -xofset, 0));
        this.vertices.Add(Vofset + new Vector3(0, -xofset * pai, xofset * pai));

        /*
           new Vector3 (-a, 0, a),
        new Vector3 (a,  0, a),
        new Vector3 (-a , a, 0),
        new Vector3 (a ,  a, 0),

        new Vector3 (-a, 0, -a),
        new Vector3 (a, 0, -a),
        new Vector3 (-a , -a, 0),
        new Vector3 (a , -a, 0), 
         */

        // uv座標を設定
        this.uvs.Add(new Vector2(0, 0));
        this.uvs.Add(new Vector2(0, 1));

        this.uvs.Add(new Vector2(1, 0));
        this.uvs.Add(new Vector2(1, 1));

        this.uvs.Add(new Vector2(2, 0));
        this.uvs.Add(new Vector2(2, 1)); 
        
        this.uvs.Add(new Vector2(3, 0));
        this.uvs.Add(new Vector2(3, 1));

        xoffset += 4;


        this.offset = 0;

        // メッシュ生成
        this.mesh = new Mesh();
    }
    public void makeLine(Vector3 Vofset3)
    {
        Vector3 Zplus90 = Vofset3 + new Vector3(xofset * cons,       0, xofset);
        Vector3 ZY = Vofset3 + new Vector3(xofset * cons,      xofset*pai, xofset*pai);
        Vector3 Yplus90 = Vofset3 + new Vector3(xofset * cons,      xofset, 0);
        Vector3 ZmY = Vofset3 + new Vector3(xofset * cons,      xofset * pai, -xofset * pai);
        Vector3 Zminus90 = Vofset3 + new Vector3(xofset * cons,      0, -xofset);
        Vector3 mZmY = Vofset3 + new Vector3(xofset * cons,      -xofset * pai, -xofset * pai);
        Vector3 Yminus90 = Vofset3 + new Vector3(xofset * cons,     -xofset, 0);
        Vector3 mZY = Vofset3 + new Vector3(xofset * cons,      -xofset * pai, xofset * pai);
        //元のベクトル＋X軸

        Vofset += new Vector3(xofset * cons, xofset * cons, 0);//移動ベクトル



        //  Debug.Log("plus90 =" + plus90);
        //  Debug.Log("minus90 =" + minus90);


        // 頂点を追加
        this.vertices.Add(Zplus90);
        this.vertices.Add(ZY);
        this.vertices.Add(Yplus90);
        this.vertices.Add(ZmY);
        this.vertices.Add(Zminus90); 
        this.vertices.Add(mZmY);
        this.vertices.Add(Yminus90); 
        this.vertices.Add(mZY);

        // UVを追加
        this.uvs.Add(new Vector2(xoffset, 0));
        this.uvs.Add(new Vector2(xoffset, 1));
        xoffset += 1;
        this.uvs.Add(new Vector2(xoffset, 0));
        this.uvs.Add(new Vector2(xoffset, 1));
        xoffset += 1;
        this.uvs.Add(new Vector2(xoffset, 0));
        this.uvs.Add(new Vector2(xoffset, 1));
        xoffset += 1;
        this.uvs.Add(new Vector2(xoffset, 0));
        this.uvs.Add(new Vector2(xoffset, 1));
        xoffset += 1;

        ////uScrollSpeed; 
        // Debug.Log(xoffset);
        /*
        mesh.uv = new Vector2[] {
        new Vector2 (0, 0),
        new Vector2 (0, 1),
        new Vector2 (1, 0),
        new Vector2 (1, 1),
    };*/


        // インデックスを追加
        this.tris.Add(offset + 0);
        this.tris.Add(offset + 8);
        this.tris.Add(offset + 1);
        this.tris.Add(offset + 8);
        this.tris.Add(offset + 9);
        this.tris.Add(offset + 1);

        this.tris.Add(offset + 1);
        this.tris.Add(offset + 9);
        this.tris.Add(offset + 2);
        this.tris.Add(offset + 9);
        this.tris.Add(offset + 10);
        this.tris.Add(offset + 2);

        this.tris.Add(offset + 2);
        this.tris.Add(offset + 10);
        this.tris.Add(offset + 3);
        this.tris.Add(offset + 10);
        this.tris.Add(offset + 11);
        this.tris.Add(offset + 3);

        this.tris.Add(offset + 3);
        this.tris.Add(offset + 11);
        this.tris.Add(offset + 4);
        this.tris.Add(offset + 11);
        this.tris.Add(offset + 12);
        this.tris.Add(offset + 4);

        this.tris.Add(offset + 4);
        this.tris.Add(offset + 12);
        this.tris.Add(offset + 5);
        this.tris.Add(offset + 12);
        this.tris.Add(offset + 13);
        this.tris.Add(offset + 5);

        this.tris.Add(offset + 5);
        this.tris.Add(offset + 13);
        this.tris.Add(offset + 6);
        this.tris.Add(offset + 13);
        this.tris.Add(offset + 14);
        this.tris.Add(offset + 6);

        this.tris.Add(offset + 6);
        this.tris.Add(offset + 14);
        this.tris.Add(offset + 7);
        this.tris.Add(offset + 14);
        this.tris.Add(offset + 15);
        this.tris.Add(offset + 7);

        this.tris.Add(offset + 7);
        this.tris.Add(offset + 15);
        this.tris.Add(offset + 0);
        this.tris.Add(offset + 15);
        this.tris.Add(offset + 8);
        this.tris.Add(offset + 0);

        offset += 8;

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
            //Debug.Log("verticesName = " + verticesName);
        }/*
        foreach (Vector2 uvName in uvs)
        {
            Debug.Log("uvName = " + uvName);
        }*/
        foreach (int trisName in tris)
        {
            // Debug.Log("trisvName = " + trisName);
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