using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    private GameObject option;
    private Option script;
    public AudioClip BGM;
    AudioSource audioSource;

    public GameObject fade;
    private bool oneFadeFlag = false;

    void Start()
    {
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGM;
        audioSource.Play();
        
    }

    void Update()
    {
        audioSource.volume = script.GetBGMVolume();
        if(!oneFadeFlag)
        {
            fade.GetComponent<FadeStart>().FadeInA();
            oneFadeFlag = true;
        }
    }
}
