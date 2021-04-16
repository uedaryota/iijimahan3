using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public AudioClip BGM;
    AudioSource audioSource;

    public GameObject fade;
    private bool oneFadeFlag = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        if(!oneFadeFlag)
        {
            fade.GetComponent<FadeStart>().FadeInA();
            oneFadeFlag = true;
        }
    }
}
