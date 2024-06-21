using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Vector2 vectorR;
    public float menuTime;
    public float SetTime = 0.1f;

    [Header("Buttons")]
    [SerializeField] float buttonSpeed;
    [SerializeField] Image makeButton;
    [SerializeField] Image playButton;

    [Header("Colors")]
    [SerializeField] Color initiateColor;
    [SerializeField] Color choiceColor;

    [Header("PlayList")]
    [SerializeField] GameObject listUI;

    List<Image> buttons;
    int number = 0;

    private void Start()
    {
        buttons = new() { makeButton, playButton};
    }



    private void Update()
    {
        vectorR = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        if (MenuWaitTime())
        {
            if (vectorR.y < -buttonSpeed)
            {
                DownChoose();

            }
            else if (vectorR.y > buttonSpeed)
            {
                UpChoose();

            }
        }

        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            switch (number)
            {
                case 0:
                    SceneManager.LoadScene("myScene");
                    Always always = FindObjectOfType<Always>();
                    always.tempDataFromMenu =  always.photoSOs[FindObjectOfType<SavePositions>().StageID];

                    break;
                case 1:
                    gameObject.SetActive(false);
                    listUI.SetActive(true);
                    break;
            }
        }
    }


    void DownChoose()
    {
        number++;
        if (number == 2)
        {
            number = 0;
        }
        Coloring();
    }
    void UpChoose()
    {
        number--;
        if (number == -1)
        {
            number = 1;
        }
        Coloring();
    }

    void Coloring()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == number)
            {
                buttons[i].color = choiceColor;
                buttons[i].rectTransform.localScale = new Vector3(3f, 3f, 3f);
            }
            else
            {
                buttons[i].color = initiateColor;
                buttons[i].rectTransform.localScale = new Vector3(2.570723f, 2.570723f, 2.570723f);
            }

        }
    }

    bool MenuWaitTime()
    {
        menuTime += Time.deltaTime;

        if (menuTime > SetTime)
        {
            menuTime = 0;

            return true;

        }
        else
        {
            return false;
        }

    }
}
