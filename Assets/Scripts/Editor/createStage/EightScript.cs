using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EightScript : MonoBehaviour
{
    const float pai = 0.71f;

    //��b�I�ȕϐ�
    const float cons = 2;//���a��2�{(���a)

    public BoxMove tes;
    public float slash = 1.0f;//�Εӂ̒����i���C���̐��m��j
    Vector3 tmp = new Vector3(0, 0, 0)
        , tp = new Vector3(0, 0, 0);//tp�͊J�n�n�_
    public Vector3 VecFront, VecBack;//���̃I�u�W�F�N�g�����@�ւ́@�x�N�g���̊p�x
    public float time, SetTime = 3;//Debug�悤�Ɏg���Ă��ϐ�


    //�|���S������̃I�u�W�F�N�g�������ɕK�v�ȕϐ�
    public Material mat;
    Mesh mesh;
    Vector3 Vofset;//�ړ��x�N�g��
    Vector3 points = new Vector3();
    List<Vector3> vertices = new List<Vector3>();//�|���S���̍��W���X�g

    List<Vector2> uvs = new List<Vector2>();//�|���S���`�ʂ�
    float xoffset = 0;

    List<int> tris = new List<int>();//uv�\��t�����X�g
    int offset = 0;

    float ofset = 0.0f,
           xofset = 1.2f;//���a�̑傫��



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
        //���N���b�N�ŏ���
        if (Input.GetMouseButtonDown(0))
        {
            tp = tes.targetPosition();//
            makeFirstLine(tp);
        }
        else if (Input.GetMouseButton(0))//���̌�̍��N���b�N
        {
            tmp = tes.targetPosition();
            float slashTmp = DistanceDifference(tp.x - tmp.x, tp.y - tmp.y);//���̃I�u�W�F�N�g�����܂ł̋���


            //VecBack = VecQy(tp, tmp);//���s�x�N�g���x�N�g���̊p�x

            Debug.Log("VecQx" + VecFront);
            if (Mathf.Abs(slashTmp) > slash)//���͈͓��ł����
            {

                VecFront = VecQx(tp, tmp);//

                // Debug.Log(tes.targetPosition());
                makeLine(tp);
                tp = tmp;//�J�n�|�W�V�������X�V
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
    }//���ʃx�N�g���̊p�x��Ԃ��B
    public int VecQy(Vector3 tp, Vector3 tmp)
    {
        Vector3 dt = tp - tmp;
        float rad = Mathf.Atan2(dt.z, dt.x);
        float vecY = rad * Mathf.Rad2Deg;
        return (int)vecY;
    }//���s�x�N�g���̊p�x��Ԃ��B

    public Vector3 VecD(Vector3 tp, Vector3 tmp)
    {
        Vector3 vec = tp + tmp;
        return vec;
    }//��_�ԃx�N�g����Ԃ�

    public float DistanceDifference(float X, float Y)
    {
        float Distance = Mathf.Sqrt(Mathf.Pow(X, 2) + Mathf.Pow(Y, 2));
        return Distance;
    }//�Εӂ̒l��Ԃ�

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
    //���g��Ȃ����ǎQ�l��

    public void makeFirstLine(Vector3 tp)
    {
        Vofset = tp;//�J�n�n�_�̃x�N�g��
        //Debug.Log(Vofset);
        // �J�n�_��ۑ�
        points = Vofset;

        // ���_��4����
        //���̕��̒��_�𐧍�
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

        // uv���W��ݒ�
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

        // ���b�V������
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
        //���̃x�N�g���{X��

        Vofset += new Vector3(xofset * cons, xofset * cons, 0);//�ړ��x�N�g��



        //  Debug.Log("plus90 =" + plus90);
        //  Debug.Log("minus90 =" + minus90);


        // ���_��ǉ�
        this.vertices.Add(Zplus90);
        this.vertices.Add(ZY);
        this.vertices.Add(Yplus90);
        this.vertices.Add(ZmY);
        this.vertices.Add(Zminus90); 
        this.vertices.Add(mZmY);
        this.vertices.Add(Yminus90); 
        this.vertices.Add(mZY);

        // UV��ǉ�
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


        // �C���f�b�N�X��ǉ�
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

    /*���b������֐��B
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