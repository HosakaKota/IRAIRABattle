using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
   
    
    public int publicHitTimes = 0;
    [SerializeField] TextMeshPro Text;
    [SerializeField] TextMeshPro TimeText;
    private void Update()
    {
        
        Text.text = publicHitTimes.ToString();
       
    }
}
