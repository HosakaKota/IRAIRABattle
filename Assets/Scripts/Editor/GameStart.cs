using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStart : MonoBehaviour
{
    private float nowTime;
    public TextMeshPro timeText;
    public bool start;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stick")
        {
            start = true;
            GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);

        }
    }

    void Update()
    {
        if (start)
        {
            nowTime += Time.deltaTime;
            int hour = (int)nowTime / 3600;
            int minute = (int)(nowTime - hour * 3600) / 60;
            int second = (int)(nowTime - hour * 3600 - minute * 60);
            timeText.text = string.Format("{0:D2}:{1:D2}", minute, second);
        }
    }


}
