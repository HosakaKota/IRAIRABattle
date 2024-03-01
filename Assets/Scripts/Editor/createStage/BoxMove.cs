using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
    public float speed = 3f;
    bool isShift = false;
    //Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isShift);
       // isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
       // move();//廫帤key偵傛傞Box偺堏摦
    }


    public void move()
    {
        float dx = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float dy = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        if (isShift)//shift僋儕僢僋偱偁傟偽 XZ堏摦
        {
            transform.position = new Vector3(
            transform.position.x + dx, transform.position.y , transform.position.z + dy);
        }
        else// 乭XY堏摦
        {
            transform.position = new Vector3(
            transform.position.x + dx, transform.position.y + dy, transform.position.z + dy);
        }
    }
    public Vector3 targetPosition()
    {
       // Debug.Log(this.transform.position);
        return this.transform.position;
        
    }

    //public void addBox()
    //{

    //    // 傂偲偮偩偗Cube傪惗惉偟偰巊偄傑傢偡丅偨偩偟丄Mesh帺懱偼嵎偟懼偊傞
    //    var o = GameObject.CreatePrimitive(PrimitiveType.Cube);

    //    o.transform.parent = GetComponent<Transform>();
    //    // BoxCollider偼巊傢側偄偺偱徚偡
    //    GameObject.Destroy(o.GetComponent<BoxCollider>());

    //    // CombineMeshes()偡傞帪偵巊偆攝楍乮GameObject偺悢偩偗嶌傞乯
    //    CombineInstance[] combineInstanceAry = new CombineInstance[A.Length];

    //    for (int i = 0; i < A.Length; i++)
    //    {
    //        // 崌惉偡傞Mesh
    //        combineInstanceAry[i].mesh = A[i].GetComponent<MeshFilter>().sharedMesh;
    //        //応強傪曄姺偟偰搉偡
    //        combineInstanceAry[i].transform = A[i].transform.localToWorldMatrix;

    //        Destroy(A[i]);
    //    }//僨乕僞傪擖傟傞


    //    // 崌惉偟偨乮偡傞乯儊僢僔儏
    //    var combinedMesh = new Mesh();
    //    combinedMesh.name = "addCubes";
    //    combinedMesh.CombineMeshes(combineInstanceAry);
    //    // 忋彂偒偡傞
    //    o.GetComponent<MeshFilter>().mesh = combinedMesh;

    //}


    //private void FixedUpdate()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    {

    //        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);//僇儊儔偐傜Ray傪庢傞
    //        RaycastHit hit;
    //        Debug.DrawRay(ray.origin, ray.direction * 6, Color.blue, 50f);
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            Destroy(hit.collider.gameObject);
    //        }
    //    }
    //}   
}
