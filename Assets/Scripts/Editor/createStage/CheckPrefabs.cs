using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPrefabs : MonoBehaviour
{
    
    public bool passed = false;
    public bool onCheck;
    Transform parentOfThis;

    // Start is called before the first frame update
    void Awake()
    {
        passed = false;
        parentOfThis = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Stick"&&transform.GetSiblingIndex()==0&&FindObjectOfType<PlayTimeManager>().startedGame==true)
        {
            if (onCheck)
            {
                onCheck = false;
                FindObjectOfType<PlayTimeManager>().UnPortected();
            }
            passed = true;
            GetComponent<MeshRenderer>().enabled = false;
            transform.SetAsLastSibling();
        }
    }
}
