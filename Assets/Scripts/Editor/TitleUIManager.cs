using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    public float titleTime;
    public float SetTime = 0.1f;
    [SerializeField] GameObject startUI;
    bool onStartUI = true;
    [SerializeField] GameObject menuUI;
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A) && onStartUI)
        {
            startUI.SetActive(false);
            menuUI.SetActive(true);
            onStartUI = false;
        }

        if (OVRInput.GetDown(OVRInput.RawButton.B) && !onStartUI)
        {
            startUI.SetActive(true);
            menuUI.SetActive(false);
            onStartUI = true;
        }
    }

}
