using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class NewQuadScript : MonoBehaviour
{
    public GameObject targetGameObject;     // インスペクタで参照を設定する

    public GameObject START;
    public GameObject END;

    public GameObject parent;

    public bool ld = false;
    public Wrapper wp = new Wrapper();
    string defaultpath, additionalpath;
    public Vector3 zure = new Vector3(10, 10, 0);
    public string[] Pathes;

    public GameObject[] savesprefab ;//足した部分
    public GameObject singlesaveprefab;
    public GameObject saveManager;

    string savepath, lastPath;
    bool sFlag = false;
    int lastSave = 0;
    public Vector3 ncmbs = new Vector3(100, 100, 100);


    private void Awake()
    {
        //保存先の計算
        defaultpath = Application.dataPath;
    }


    //public GameObject test1;

     float xoffset = 0;
    int offset = 0;

    

    //基礎的な変数
    const float             cons = 2;//半径の2倍(直径)

    public BoxMove          tes;

    public float            slash = 0.24f;//斜辺の長さ（ラインの正確具合）

           Vector3          tmp = new Vector3(0, 0, 0)
            ,               tp = new Vector3(0, 0, 0)
            ,               Vec;//tpは開始地点

    public float            VecFront, VecUp,VecSide;//次のオブジェクト生成　への　ベクトルの角度
    public float            time, SetTime = 3;//Debugように使ってた変数


    //ポリゴンからのオブジェクト生成時に必要な変数
    public Material         mat;
    public Mesh      mesh;
           Vector3          Vofset;//移動ベクトル
           Vector3          points = new Vector3();

           List<Vector3>    ObjectPoint = new List<Vector3>();//ポリゴンの座標リスト
           List<Vector3>    vertices = new List<Vector3>();//ポリゴンの座標リスト
           List<Vector2>    uvs = new List<Vector2>();//ポリゴン描写の
           List<int>        tris = new List<int>();//uv貼り付けリスト
           float            UvOffset = 0;
           int              verticesOffset = 0;
           int              verticesNum = 0;//合計の頂点数,
    public int              saveNum=10;//セーブポイントの数

    public int              pointNum = 0;//セーブポイントが作られた数

          [SerializeField] float            PositionOffset = 0.12f;//マテリアルの大きさの半分
                                                                   //float xofset = 0.12f;//マテリアルの大きさの半分

   // public static bool testStage = false;




    void Start()
    {
        DestroyCom();
        //Debug.Break();
        mesh = GameObject.Find("mesh").GetComponent<MeshFilter>().mesh;
        mesh = new();
        ObjectPoint.Clear();
        vertices.Clear();
        uvs.Clear();
        tris.Clear();
        tmp = new Vector3(0, 0, 0);
        tp = new Vector3(0, 0, 0);
        LineDrawer.firstTime = true;

        /*if (testStage)
        {
            vertices.Clear();
            uvs.Clear();
            tris.Clear();
            wp = LoadTest(defaultpath + "/Positions/pos0.json");
            //Debug.Log(wp.pos);
            LoadStage(wp.pos);
        }*/
        //makebox();
        wp.pos = new List<Vector3>();
        Pathes = new string[]
        {
            "/Positions/pos0.json",
            "/Positions/pos1.json",
            "/Positions/pos2.json",
            "/Positions/pos3.json",
            "/Positions/pos4.json",
            "/Positions/pos5.json",
            "/Positions/pos6.json",
            "/Positions/pos7.json",
            "/Positions/pos8.json",
            "/Positions/pos9.json"
        };
        //makebox();
    }
    void Update()
    {
        addCom();
        //string datapath = defaultpath + additionalpath;
        //Debug.Log("--------------------------------------path = " + datapath);
        // makeStage();
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    ld = true;
        //    vertices.Clear();
        //    uvs.Clear();
        //    tris.Clear();
        //    //Debug.Log("saved");
        //    SaveTest(wp, datapath);
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    if (File.Exists(datapath))
        //    {
        //       // Debug.Log("loading");
        //        vertices.Clear();
        //        uvs.Clear();
        //        tris.Clear();
        //        wp = LoadTest(datapath);
        //        //Debug.Log(wp.pos);
        //        LoadStage(wp.pos);
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    additionalpath = Pathes[0];
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //   // Debug.Log("dpath = " + Pathes[1]);
        //    additionalpath = Pathes[1];
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    //Debug.Log("dpath = " + Pathes[2]);
        //    additionalpath = Pathes[2];
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    //Debug.Log("dpath = " + Pathes[3]);
        //    additionalpath = Pathes[3];
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //   // Debug.Log("dpath = " + Pathes[4]);
        //    additionalpath = Pathes[4];
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    //Debug.Log("dpath = " + Pathes[5]);
        //    additionalpath = Pathes[5];
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha6))
        //{
        //    //Debug.Log("dpath = " + Pathes[6]);
        //    additionalpath = Pathes[6];
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    //Debug.Log("dpath = " + Pathes[7]);
        //    additionalpath = Pathes[7];
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //   // Debug.Log("dpath = " + Pathes[8]);
        //    additionalpath = Pathes[8];
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    //Debug.Log("dpath = " + Pathes[9]);
        //    additionalpath = Pathes[9];
        //}
}

    public void makeStage()
    {
        //嵍僋儕僢僋嵟弶偺
        if (LineDrawer.firstTime)
        {
            tp = tes.targetPosition();//
            wp.pos.Add(tp + zure);
            ncmbs = tp;

            //Debug.Log(tes.targetPosition());
            //Instantiate(test1,tp,new Quaternion(1,1,1,1));
            makeFirstLine(tp);
            LineDrawer.firstTime = false;
           
        }
        else//偦偺屻偺嵍僋儕僢僋
        {
            tmp = tes.targetPosition();
            if (tmp != tp)
            {
                wp.pos.Add(tmp + zure);
                ncmbs = tmp;
            }
            float slashTmpXY = DistanceDifference(tp.x - tmp.x, tp.y - tmp.y);//師偺僆僽僕僃僋僩惗惉傑偱偺嫍棧(XY)
            float slashTmpXZ = DistanceDifference(tp.x - tmp.x, tp.z - tmp.z);//師偺僆僽僕僃僋僩惗惉傑偱偺嫍棧(XZ)

            //Debug.Log(slashTmpXY);
            //VecBack = VecQy(tp, tmp);//墱峴儀僋僩儖儀僋僩儖偺妏搙

            if (Mathf.Abs(slashTmpXY) > slash || Mathf.Abs(slashTmpXZ) > slash)//堦掕斖埻撪偱偁傟偽
            {
                //Debug.Log("hi");
                Vec = tmp - tp;
                VecFront = VecQx(Vec);//惓柺儀僋僩儖
                VecUp = VecQy(Vec);//墱峴儀僋僩儖
                VecSide = VecQz(Vec);//墶儀僋僩儖
               // Debug.Log("VecQx" + VecFront);
                //Debug.Log("QyVec" + VecBack);
                // Debug.Log(tes.targetPosition());
                makeLine(tp);
                tp = tmp;//奐巒億僕僔儑儞傪峏怴
            }
        }

    }

    public float VecQx(Vector3 dt)
    {
        float rad = Mathf.Atan2(dt.y, dt.x);
        if (rad >= 3.14f)
            rad = 3.12f;
        return (float)rad;
    }//惓柺儀僋僩儖偺妏搙傪曉偡丅

    public float VecQy(Vector3 dt)
    {
        float rad = Mathf.Atan2(dt.z, dt.x);
        if (rad >= 3.14f)
            rad = 3.12f;
        return (float)rad;
    }//墱峴儀僋僩儖偺妏搙傪曉偡丅
    public float VecQz(Vector3 dt)
    {
        float rad = Mathf.Atan2(dt.z, dt.y);
        if (rad >= 3.14f)
            rad = 3.12f;
        //Debug.Log(rad);
        return (float)rad;
    }//墱峴儀僋僩儖偺妏搙傪曉偡丅

    public Vector3 VecD(Vector3 tp, Vector3 tmp)
    {
        Vector3 vec = tp + tmp;
        return vec;
    }//擇揰娫儀僋僩儖傪曉偡

    public float DistanceDifference(float A, float B)
    {
        float Distance = Mathf.Sqrt(Mathf.Pow(A, 2) + Mathf.Pow(B, 2));
        return Distance;
    }//AB娫偺幬曈偺抣乮挿偝乯傪曉偡

    float a = 1.2f, UVoffset = 0;
    public void makebox()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[] {
        new Vector3 (-a, 0, a),
        new Vector3 (a,  0, a),
        new Vector3 (-a , a, 0),
        new Vector3 (a ,  a, 0),

        new Vector3 (-a, 0, -a),
        new Vector3 (a, 0, -a),
        new Vector3 (-a , -a, 0),
        new Vector3 (a , -a, 0),
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

        };



        mesh.triangles = new int[] {
        0, 1, 2,
        1, 3, 2,


        2, 3, 4,
        3, 5, 4,

        4, 5, 6,
        5, 7, 6,

        6, 7, 0,
        7, 1, 0,
        };

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshRenderer>().material = mat;

    }
    //仾巊傢側偄偗偳嶲峫偵

    public void makeFirstLine(Vector3 tp)
    {
        Vofset = tp;//奐巒抧揰偺儀僋僩儖
        //Debug.Log(Vofset);
        // 奐巒揰傪曐懚
        points = Vofset;

        // 捀揰傪4偮惗惉
        //堦掕偺暆偺捀揰傪惂嶌
        this.vertices.Add(new Vector3(tp.x, tp.y, tp.z));
        this.vertices.Add(new Vector3(tp.x, tp.y, tp.z));
        this.vertices.Add(new Vector3(tp.x, tp.y, tp.z));
        this.vertices.Add(new Vector3(tp.x, tp.y, tp.z));
        
        this.ObjectPoint.Add(Vofset);
        verticesNum++;

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

        // uv嵗昗傪愝掕
        this.uvs.Add(new Vector2(0, 0));
        this.uvs.Add(new Vector2(0, 1));
        
        this.uvs.Add(new Vector2(1, 0));
        this.uvs.Add(new Vector2(1, 1)); 

        xoffset += 2;


        this.offset = 0;

        // 儊僢僔儏惗惉
        this.mesh = new Mesh();
    }
    public void makeLine(Vector3 Vofset3)
    {
        float FloorLine = 100;//彫悢揰俀埲壓愗傝幪偰偺偨傔
        //Debug.Log("Mathf.Cos(VecUp)" + Mathf.Cos(VecUp));
        float SinFront = Mathf.Sin(VecFront) * FloorLine;
        float CosFront = Mathf.Cos(VecFront) * FloorLine;
        float SinUp = Mathf.Sin(VecUp) * FloorLine;
        float CosUp = Mathf.Cos(VecUp) * FloorLine;
        float SinSide = Mathf.Sin(VecSide) * FloorLine;
        float CosSide= Mathf.Cos(VecSide) * FloorLine;
        //Debug.Log("VecUp CosUp===" + VecUp + CosUp);
        SinFront = Mathf.Floor(SinFront) / FloorLine;
        CosFront = Mathf.Floor(CosFront) / FloorLine;
        SinUp = Mathf.Floor(SinUp) / FloorLine;
        CosUp = Mathf.Floor(CosUp) / FloorLine;
        SinSide = Mathf.Floor(SinSide) / FloorLine;
        CosSide = Mathf.Floor(CosSide) / FloorLine;
        if (-0.05 < SinFront && SinFront < 0.05)
            SinFront = 0;
        if (-0.05 < CosFront && CosFront < 0.05)
            CosFront = 0;
        if (-0.05 < SinUp && SinUp < 0.05)
            SinUp = 0;
        if (-0.05 < CosUp && CosUp < 0.05)
            CosUp = 0;
        if (-0.05 < SinSide && SinSide < 0.05)
            SinSide = 0;
        if (-0.05 < CosSide && CosSide < 0.05)
            CosSide = 0;

        Vector3 Zplus90 , Yplus90, Zminus90 ,Yminus90;

        Zplus90 = Vofset3 + new Vector3(    -PositionOffset * (SinUp * CosFront),//
                                            -((PositionOffset * (SinSide * SinFront))),//
                                            (PositionOffset * ((Mathf.Abs(CosUp) + Mathf.Abs(CosSide)) - (Mathf.Abs(CosUp) * Mathf.Abs(CosSide)))));//

        Yplus90 = Vofset3 + new Vector3(    -(PositionOffset * (SinFront)),//
                                            (PositionOffset * (CosFront)),//
                                            -(PositionOffset * (SinUp * CosFront * SinFront)));//


        Zminus90 = Vofset3 + new Vector3(   PositionOffset * (SinUp * CosFront),//
                                            ((PositionOffset * (SinSide * SinFront))),//
                                            -(PositionOffset * ((Mathf.Abs(CosUp) + Mathf.Abs(CosSide)) - (Mathf.Abs(CosUp) * Mathf.Abs(CosSide)))));//

        Yminus90 = Vofset3 + new Vector3(   (PositionOffset * (SinFront)),//
                                            -(PositionOffset * (CosFront)),//
                                            (PositionOffset * (SinUp * CosFront * SinFront)));//

        /*UpForntSideLog(SinFront, CosFront, SinUp, CosUp, SinSide, CosSide);
        WidthLineLog(Zplus90, Yplus90, Zminus90, Yminus90, Vofset3);
        PositionLog(Zplus90, Yplus90, Zminus90, Yminus90, Vofset3);*/

        Vofset += new Vector3(PositionOffset * cons, PositionOffset * cons, 0);//堏摦儀僋僩儖



        //  Debug.Log("plus90 =" + plus90);
        //  Debug.Log("minus90 =" + minus90);


        // 捀揰傪捛壛
        this.vertices.Add(Zplus90);
        this.vertices.Add(Yplus90);
        this.vertices.Add(Zminus90);
        this.vertices.Add(Yminus90);

        this.ObjectPoint.Add(Vofset3);
        verticesNum++;

        // UV傪捛壛
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


        // 僀儞僨僢僋僗傪捛壛
        this.tris.Add(offset+0);
        this.tris.Add(offset + 4);
        this.tris.Add(offset + 1);
        this.tris.Add(offset + 4);
        this.tris.Add(offset + 5);
        this.tris.Add(offset + 1);

        this.tris.Add(offset + 1);
        this.tris.Add(offset + 5);
        this.tris.Add(offset + 2);
        this.tris.Add(offset + 5);
        this.tris.Add(offset + 6);
        this.tris.Add(offset + 2);

        this.tris.Add(offset + 2);
        this.tris.Add(offset + 6);
        this.tris.Add(offset + 3);
        this.tris.Add(offset + 6);
        this.tris.Add(offset + 7);
        this.tris.Add(offset + 3);

        this.tris.Add(offset + 3);
        this.tris.Add(offset + 7);
        this.tris.Add(offset + 0);
        this.tris.Add(offset + 7);
        this.tris.Add(offset + 4);
        this.tris.Add(offset + 0);

        offset += 4;

        /*
        mesh.triangles = new int[] {
        0, 1, 2,
        1, 3, 2,
    };*/


        mesh.vertices = this.vertices.ToArray();
        mesh.uv = this.uvs.ToArray();
        mesh.triangles = this.tris.ToArray();


        /*foreach (Vector3 ObjectPointName in ObjectPoint)
        {
            Debug.Log("ObjectPoint = " + ObjectPointName);
        }
        foreach (Vector3 verticesName in vertices)
        {
            Debug.Log("verticesName = " + verticesName);
        }
        foreach (Vector2 uvName in uvs)
        {
            Debug.Log("uvName = " + uvName);
        }
        foreach (int trisName in tris)
        {
            Debug.Log("trisvName = " + trisName);
        }*/



        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshRenderer>().material = mat;

    }

    /*堦掕昩悢偊傞娭悢丅
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
    public void LoadStage(List<Vector3> v3)//ステージのロード
    {
        int count = v3.Count;
        int savepoints = count / 10;//足した部分

        for (int i = 0; i < v3.Count; i++)
        {
            if (i == 0)
            {
                tp = v3[0];//vec3型、左クリックしながらWASD(shiftで奥行き)
                makeFirstLine(tp);
                //Debug.Log("tp(v3[0]) = " + tp);

            }
            else
            {
                tmp = v3[i];//vec3型

                float slashTmpXY = DistanceDifference(tp.x - tmp.x, tp.y - tmp.y);//次のオブジェクト生成までの距離(XY)
                float slashTmpXZ = DistanceDifference(tp.x - tmp.x, tp.z - tmp.z);//次のオブジェクト生成までの距離(XZ)

                //Debug.Log(slashTmpXY);
                //VecBack = VecQy(tp, tmp);//奥行ベクトルベクトルの角度

                if (Mathf.Abs(slashTmpXY) > slash || Mathf.Abs(slashTmpXZ) > slash)//一定範囲内であれば
                {
                    //Debug.Log("hi");
                    Vec = tmp - tp;
                    VecFront = VecQx(Vec);//正面ベクトル
                    VecUp = VecQy(Vec);//奥行ベクトル
                    VecSide = VecQz(Vec);//横ベクトル
                                         // Debug.Log("VecQx" + VecFront);
                                         //Debug.Log("QyVec" + VecBack);
                                         // Debug.Log(tes.targetPosition());
                    makeLine(tp);
                    tp = tmp;//開始ポジションを更新
                }
            }
            if (i != 0 && i % savepoints == 0 && i < count - savepoints)
            {
                CreatePoints(v3[i]);
            }
            //足した部分
        }
        Instantiate(START, v3[0], new Quaternion(0, 0, 0, 0));
        Instantiate(END, v3[v3.Count - 1], new Quaternion(0, 0, 0, 0));
        saveManager.SetActive(true);

    }

    public void SaveTest(Wrapper w, string s)
    {
        string jsdt = JsonUtility.ToJson(w);
        //Debug.Log(jsdt);
        Debug.Log("s = " + s);
        StreamWriter writer = new StreamWriter(s, false);//初めに指定したデータの保存先を開く
        writer.WriteLine(jsdt);//JSONデータを書き込み
        writer.Flush();//バッファをクリアする
        writer.Close();//ファイルをクローズする
    }

    public Wrapper LoadTest(string dp)
    {
        StreamReader reader = new StreamReader(dp);//
        string datastr = reader.ReadToEnd();//ファイルの中身をすべて読み込む
        reader.Close();//ファイルを閉じる
        //読み込んだJSONファイルをPlayerData型に変換して返す
        Wrapper jsrt = JsonUtility.FromJson<Wrapper>(datastr);
        return jsrt;
    }


    public void verticesNumPoint()
    {

        float PointNum = verticesNum / saveNum;
        for (int i = 0; i < saveNum; i++)
        {
            pointNum = i+1;//外部参照用にpointNumに渡す：Elementpoint:セーブポイント用
            CreatePoints(ObjectPoint[pointNum * i]);
        }
        
    }
    public void CreatePoints(Vector3 v3)
    {
        //Instantiate(savesprefab[pointNum], v3, Quaternion.identity);
        Instantiate(singlesaveprefab, v3, Quaternion.identity,parent.transform);
        //Debug.Log("オブジェクトを生成");
    }//足した部分


    void UpForntSideLog(float SinFront,
                      float CosFront,
                      float SinUp,
                      float CosUp,
                      float SinSide,
                      float CosSide)
    {
        if (SinUp >= 0)
            Debug.Log("-SinUp" + SinUp);
        else
            Debug.Log("<color=green>-SinUp" + SinUp + "</color>");
        if (SinFront >= 0)
            Debug.Log("-SinFront" + SinFront);
        else
            Debug.Log("<color=green>-SinFront" + SinFront + "</color>");

        if (SinSide >= 0)
            Debug.Log("SinSide" + SinSide);
        else
            Debug.Log("<color=green>-SinSide" + SinSide + "</color>");

        if (CosUp >= 0)
            Debug.Log("CosUp" + CosUp);
        else
            Debug.Log("<color=green>-CosUp" + CosUp + "</color>");
        if (CosFront >= 0)
            Debug.Log("CosFront" + CosFront);
        else
            Debug.Log("<color=green>-CosFront" + CosFront + "</color>");
        if (CosSide >= 0)
            Debug.Log("CosSide" + CosSide);
        else
            Debug.Log("<color=green>-CosSide" + CosSide + "</color>");
    }//SinCosの値をLog
    void PositionLog(Vector3 Zplus,
                     Vector3 Yplus,
                     Vector3 Zminus,
                     Vector3 Yminus,
                     Vector3 Vofset3)
    {
        Debug.Log("Zplus" + Zplus);
        Debug.Log("Yplus" + Yplus);
        Debug.Log("Zminus" + Zminus);
        Debug.Log("Yminus" + Yminus);

        Debug.Log("Vofset3" + Vofset3);
    }//それぞれLineの頂点のLog

    void WidthLineLog(Vector3 Zplus,
                        Vector3 Yplus,
                        Vector3 Zminus,
                        Vector3 Yminus,
                        Vector3 Vofset3)
    {
        if (DistanceDifference(Vofset3.x - Zplus.x, Vofset3.y - Zplus.y) >= 1.15 && DistanceDifference(Vofset3.x - Zplus.x, Vofset3.y - Zplus.y) <= 1.25 ||
            DistanceDifference(Vofset3.x - Zplus.x, Vofset3.z - Zplus.z) >= 1.15 && DistanceDifference(Vofset3.x - Zplus.x, Vofset3.z - Zplus.z) <= 1.25 ||
            DistanceDifference(Vofset3.y - Zplus.y, Vofset3.z - Zplus.z) >= 1.15 && DistanceDifference(Vofset3.y - Zplus.y, Vofset3.z - Zplus.z) <= 1.25)
            Debug.Log("<color=yellow>(" + DistanceDifference(Vofset3.x - Zplus.x, Vofset3.y - Zplus.y) + ")" +
            "(" + DistanceDifference(Vofset3.x - Zplus.x, Vofset3.z - Zplus.z) + ")" +
            "(" + DistanceDifference(Vofset3.y - Zplus.y, Vofset3.z - Zplus.z) + ")" + "ZplusXYZ</color>"
            );
        else if (DistanceDifference(Vofset3.x - Zplus.x, Vofset3.y - Zplus.y) >= 1 || DistanceDifference(Vofset3.x - Zplus.x, Vofset3.z - Zplus.z) >= 1 || DistanceDifference(Vofset3.y - Zplus.y, Vofset3.z - Zplus.z) >= 1)
            Debug.Log("(" + DistanceDifference(Vofset3.x - Zplus.x, Vofset3.y - Zplus.y) + ")" +
            "(" + DistanceDifference(Vofset3.x - Zplus.x, Vofset3.z - Zplus.z) + ")" +
            "(" + DistanceDifference(Vofset3.y - Zplus.y, Vofset3.z - Zplus.z) + ")" + "ZplusXYZ"
            );
        else
            Debug.Log("<color=red>(" + DistanceDifference(Vofset3.x - Zplus.x, Vofset3.y - Zplus.y) + ")" +
            "(" + DistanceDifference(Vofset3.x - Zplus.x, Vofset3.z - Zplus.z) + ")" +
            "(" + DistanceDifference(Vofset3.y - Zplus.y, Vofset3.z - Zplus.z) + ")" + "ZplusXYZ</color>"
            );


        if (DistanceDifference(Vofset3.x - Yplus.x, Vofset3.y - Yplus.y) >= 1.15 && DistanceDifference(Vofset3.x - Yplus.x, Vofset3.y - Yplus.y) <= 1.25 ||
            DistanceDifference(Vofset3.x - Yplus.x, Vofset3.z - Yplus.z) >= 1.15 && DistanceDifference(Vofset3.x - Yplus.x, Vofset3.z - Yplus.z) <= 1.25 ||
            DistanceDifference(Vofset3.y - Yplus.y, Vofset3.z - Yplus.z) >= 1.15 && DistanceDifference(Vofset3.y - Yplus.y, Vofset3.z - Yplus.z) <= 1.25)
            Debug.Log("(<color=yellow>" + DistanceDifference(Vofset3.x - Yplus.x, Vofset3.y - Yplus.y) + ")" +
            "(" + DistanceDifference(Vofset3.x - Yplus.x, Vofset3.z - Yplus.z) + ")" +
            "(" + DistanceDifference(Vofset3.y - Yplus.y, Vofset3.z - Yplus.z) + ")" + "YplusXYZ</color>"
            );
        else if (DistanceDifference(Vofset3.x - Yplus.x, Vofset3.y - Yplus.y) >= 1 || DistanceDifference(Vofset3.x - Yplus.x, Vofset3.z - Yplus.z) >= 1 || DistanceDifference(Vofset3.y - Yplus.y, Vofset3.z - Yplus.z) >= 1)
            Debug.Log("(" + DistanceDifference(Vofset3.x - Yplus.x, Vofset3.y - Yplus.y) + ")" +
            "(" + DistanceDifference(Vofset3.x - Yplus.x, Vofset3.z - Yplus.z) + ")" +
            "(" + DistanceDifference(Vofset3.y - Yplus.y, Vofset3.z - Yplus.z) + ")" + "YplusXYZ"
            );
        else
            Debug.Log("<color=red>(" + DistanceDifference(Vofset3.x - Yplus.x, Vofset3.y - Yplus.y) + ")" +
            "(" + DistanceDifference(Vofset3.x - Yplus.x, Vofset3.z - Yplus.z) + ")" +
            "(" + DistanceDifference(Vofset3.y - Yplus.y, Vofset3.z - Yplus.z) + ")" + "YplusXYZ</color>"
            );



        if (DistanceDifference(Vofset3.x - Zminus.x, Vofset3.y - Zminus.y) >= 1.15 && DistanceDifference(Vofset3.x - Zminus.x, Vofset3.y - Zminus.y) <= 1.25 ||
            DistanceDifference(Vofset3.x - Zminus.x, Vofset3.z - Zminus.z) >= 1.15 && DistanceDifference(Vofset3.x - Zminus.x, Vofset3.z - Zminus.z) <= 1.25 ||
            DistanceDifference(Vofset3.y - Zminus.y, Vofset3.z - Zminus.z) >= 1.15 && DistanceDifference(Vofset3.y - Zminus.y, Vofset3.z - Zminus.z) <= 1.25)
            Debug.Log("(<color=yellow>" + DistanceDifference(Vofset3.x - Zminus.x, Vofset3.y - Zminus.y) + ")" +
                    "(" + DistanceDifference(Vofset3.x - Zminus.x, Vofset3.z - Zminus.z) + ")" +
                    "(" + DistanceDifference(Vofset3.y - Zminus.y, Vofset3.z - Zminus.z) + ")" + "ZminusXYZ</color>"
                    );
        else if (DistanceDifference(Vofset3.x - Zminus.x, Vofset3.y - Zminus.y) >= 1 || DistanceDifference(Vofset3.x - Zminus.x, Vofset3.z - Zminus.z) >= 1 || DistanceDifference(Vofset3.y - Zminus.y, Vofset3.z - Zminus.z) >= 1)
            Debug.Log("(" + DistanceDifference(Vofset3.x - Zminus.x, Vofset3.y - Zminus.y) + ")" +
                    "(" + DistanceDifference(Vofset3.x - Zminus.x, Vofset3.z - Zminus.z) + ")" +
                    "(" + DistanceDifference(Vofset3.y - Zminus.y, Vofset3.z - Zminus.z) + ")" + "ZminusXYZ"
                    );
        else
            Debug.Log("<color=red>(" + DistanceDifference(Vofset3.x - Zminus.x, Vofset3.y - Zminus.y) + ")" +
                    "(" + DistanceDifference(Vofset3.x - Zminus.x, Vofset3.z - Zminus.z) + ")" +
                    "(" + DistanceDifference(Vofset3.y - Zminus.y, Vofset3.z - Zminus.z) + ")" + "ZminusXYZ</color>"
                    );


        if (DistanceDifference(Vofset3.x - Yminus.x, Vofset3.y - Yminus.y) >= 1.15 && DistanceDifference(Vofset3.x - Yminus.x, Vofset3.y - Yminus.y) <= 1.25 ||
            DistanceDifference(Vofset3.x - Yminus.x, Vofset3.z - Yminus.z) >= 1.15 && DistanceDifference(Vofset3.x - Yminus.x, Vofset3.z - Yminus.z) <= 1.25 ||
            DistanceDifference(Vofset3.y - Yminus.y, Vofset3.z - Yminus.z) >= 1.15 && DistanceDifference(Vofset3.y - Yminus.y, Vofset3.z - Yminus.z) <= 1.25)
            Debug.Log("(<color=yellow>" + DistanceDifference(Vofset3.x - Yminus.x, Vofset3.y - Yminus.y) + ")" +
            "(" + DistanceDifference(Vofset3.x - Yminus.x, Vofset3.z - Yminus.z) + ")" +
            "(" + DistanceDifference(Vofset3.y - Yminus.y, Vofset3.z - Yminus.z) + ")" + "YminusXYZ</color>"
            );
        else if (DistanceDifference(Vofset3.x - Yminus.x, Vofset3.y - Yminus.y) >= 1 || DistanceDifference(Vofset3.x - Yminus.x, Vofset3.z - Yminus.z) >= 1 || DistanceDifference(Vofset3.y - Yminus.y, Vofset3.z - Yminus.z) >= 1)
            Debug.Log("(" + DistanceDifference(Vofset3.x - Yminus.x, Vofset3.y - Yminus.y) + ")" +
            "(" + DistanceDifference(Vofset3.x - Yminus.x, Vofset3.z - Yminus.z) + ")" +
            "(" + DistanceDifference(Vofset3.y - Yminus.y, Vofset3.z - Yminus.z) + ")" + "YminusXYZ"
            );
        else
            Debug.Log("<color=red>(" + DistanceDifference(Vofset3.x - Yminus.x, Vofset3.y - Yminus.y) + ")" +
                    "(" + DistanceDifference(Vofset3.x - Yminus.x, Vofset3.z - Yminus.z) + ")" +
                    "(" + DistanceDifference(Vofset3.y - Yminus.y, Vofset3.z - Yminus.z) + ")" + "YminusXYZ</color>"
                    );
    }//真ん中の位置からそれぞれの点の距離のLog




    void DestroyCom()
    {
        var meshFileterComponent = targetGameObject.GetComponent<MeshFilter>();
        // 取得したコンポーネントを削除(対象のゲームオブジェクトから削除される)
        Destroy(meshFileterComponent);
    }
    void addCom()
    {
        targetGameObject.AddComponent<MeshFilter>();
    }













}