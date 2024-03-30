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
}
