using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuadScript : MonoBehaviour
{

    const float cons = 2;//���a��2�{(���a)

    public BoxMove tes;
    public float slash = 1.0f;//�Εӂ̒����i���C���̐��m��j
    Vector3 tmp = new Vector3(0, 0, 0)
        , tp = new Vector3(0, 0, 0);//tp�͊J�n�n�_

    public Material mat;
    Mesh mesh;
    Vector3 Vofset;//�ړ��x�N�g��
    Vector3 points = new Vector3();
    List<Vector3> vertices = new List<Vector3>();//�|���S���̍��W���X�g

    List<Vector2> uvs = new List<Vector2>();//�|���S���`�ʂ�
    float xoffset = 0;

    List<int> tris = new List<int>();//uv�\��t�����X�g
    int offset = 0;

    float xofset = 1.2f;//���a�̑傫��


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
        //���N���b�N�ŏ���
        if (Input.GetMouseButtonDown(0))
        {
            tp = tes.targetPosition();//
            makeFirstLine(tp);
        }
        else if (Input.GetMouseButton(0))//���̌�̍��N���b�N
        {
            tmp = tes.targetPosition();
            float slashTmp = DistanceDifference(tp.x - tmp.x, tp.y - tmp.y);//

            if (Mathf.Abs(slashTmp) > slash)//���͈͓��ł����
            {
                Debug.Log(tes.targetPosition());
                makeLine(tp);
                tp = tmp;//�J�n�|�W�V�������X�V
            }
        }

    }

    public float DistanceDifference(float X, float Y)
    {
        float Distance = Mathf.Sqrt(Mathf.Pow(X, 2) + Mathf.Pow(Y, 2));
        return Distance;
    }//�Εӂ̒l��Ԃ�

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
        Vofset = tp;//�J�n�n�_�̃x�N�g��
        Debug.Log(Vofset);
        // �J�n�_��ۑ�
        points = Vofset;

        // ���_���Q����
        this.vertices.Add(Vofset + new Vector3(0, -xofset, 0));//���̕��̒��_�𐧍�
        this.vertices.Add(Vofset + new Vector3(0, xofset, 0));


        // uv���W��ݒ�
        this.uvs.Add(new Vector2(0, 0));
        this.uvs.Add(new Vector2(0, 1));
        xoffset += 1;


        this.offset = 0;

        // ���b�V������
        this.mesh = new Mesh();
    }
    public void makeLine(Vector3 Vofset2)
    {

        Vector2 plus90 = new Vector2(Vofset2.x, Vofset2.y) + new Vector2(xofset * cons, xofset);
        Vector2 minus90 = new Vector2(Vofset2.x, Vofset2.y) + new Vector2(xofset * cons, -xofset);


        Vofset += new Vector3(xofset * cons, xofset * cons, 0);//�ړ��x�N�g��



        //  Debug.Log("plus90 =" + plus90);
        //  Debug.Log("minus90 =" + minus90);


        // ���_��ǉ�
        this.vertices.Add(minus90);
        this.vertices.Add(plus90);

        // UV��ǉ�
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


        // �C���f�b�N�X��ǉ�
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