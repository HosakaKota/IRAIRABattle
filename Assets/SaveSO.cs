using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SaveData",menuName ="new SaveData")]
public class SaveSO : ScriptableObject
{
    int lastsave;

    public int GetLastSaveInt()
    {
        return lastsave;
    }

    public void SetLastSaveInt()
    {
        lastsave++;
        if (lastsave>=9)
        {
            lastsave = 0;
        }
    }
}
