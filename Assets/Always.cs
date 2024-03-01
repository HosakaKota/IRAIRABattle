using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Always : MonoBehaviour
{
    public PhotoSO tempDataFromMenu;
    public SaveSO saveSO;
    public static GameObject always;
    public List<PhotoSO> photoSOs = new();

    private void Start()
    {
        if (always==null)
        {
            always = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
