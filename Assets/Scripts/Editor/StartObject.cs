using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartObject : MonoBehaviour
{
    PlayTimeManager timeManager;
    bool first = true;
    

    private void Start()
    {
        timeManager = GameObject.FindObjectOfType<PlayTimeManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stick")
        {
            if (first)
            {
                if (SceneManager.GetActiveScene().name == "play")
                {
                    FindObjectOfType<PlayTimeManager>().CreateCollider();
                }
                timeManager.OnBeginPlay();
                timeManager.startedGame = true;
                transform.SetParent(GameObject.Find("parent").transform);
                transform.SetAsLastSibling();
                first = false;
            }
            GetComponent<CheckPrefabs>().passed = true;
            GetComponent<MeshRenderer>().enabled = false;
            //Destroy(gameObject);
        }
    }
}
