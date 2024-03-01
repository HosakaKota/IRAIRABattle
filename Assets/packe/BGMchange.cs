using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMchange : MonoBehaviour
{
    private AudioSource audio;
    private AudioClip Sound;
    private string songName;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
   
            if (SceneManager.GetActiveScene().name == "Title")
            {
                songName = "Title";
                Sound = (AudioClip)Resources.Load("Sound/" + songName);
                audio.PlayOneShot(Sound);
            }
        

            if (SceneManager.GetActiveScene().name == "Play")
            {
                songName = "Play";
                Sound = (AudioClip)Resources.Load("Sound/" + songName);
                audio.PlayOneShot(Sound);
            }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "GameEnd")
        {
            if (SceneManager.GetActiveScene().name == "GameEnd")
        {
            audio.Stop();
            songName = "GameEnd";
            Sound = (AudioClip)Resources.Load("Sound/" + songName);
            audio.PlayOneShot(Sound);
        }}
    }
}
