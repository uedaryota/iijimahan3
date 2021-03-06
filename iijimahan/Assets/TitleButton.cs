﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    private GameObject option;
    private Option script;
    [SerializeField, Header("次のシーンを選択した時の音")] public AudioClip SelectSE;
    AudioSource audioSource;
    public GameObject fade;
    public GameObject OptionCanvas;
    public GameObject Canvas;
    private float time;
    private bool flag;

    void Start()
    {
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        audioSource = GetComponent<AudioSource>();
        time = 0;
        flag = false;
        // fade.GetComponent<FadeStart>().FadeInA();
        //Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (flag == true)
        {
            time += Time.unscaledDeltaTime;
        }
        if (time >= 0.5f)
        {
            OptionButton();
            time = 0.0f;
            flag = false;
        }
    }

    public void OnClick()
    {
        if (transform.name == "StartButton")
        {
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            Invoke("StartButton", 0.5f);
            //fade.GetComponent<FadeStart>().FadeOutNextScene("GameScene");
            //SceneManager.LoadScene("GameScene");
        }
        if(transform.name == "OptionButton")
        {
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            flag = true;
            //Canvas.SetActive(false);
            //OptionCanvas.SetActive(true);
        }
        if (transform.name == "GameEndButton")
        {
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            Invoke("Quit", 0.5f);
        }
    }
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }

    private void StartButton()
    {
        fade.GetComponent<FadeStart>().FadeOutNextScene("GameScene");
    }

    private void OptionButton()
    {
        Canvas.SetActive(false);
        OptionCanvas.SetActive(true);
    }
}
