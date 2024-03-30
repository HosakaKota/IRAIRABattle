using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] GameObject Timer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stick")
        {
            Timer.GetComponent<GameStart>().start = false;
        }
    }
}
