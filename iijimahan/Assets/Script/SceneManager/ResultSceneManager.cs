using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    private GameObject option;
    private Option optionscript;
    public GameObject text_obj;
    private PlayerControl script;
    private int gauge;
    private float time;
    private int wave;
    public AudioClip BGM;
    AudioSource audioSource;
    public GameObject fade;
    private bool oneFadeFlag = false;

    float audioTimer = 0;
    bool bgmFlag = false;

    float vol = 0;


    void Start()
    {
        option = GameObject.Find("Option");
        optionscript = option.GetComponent<Option>();
        optionscript.SetClearFlag(true);
        gauge = PlayerControl.GetGaugeCount();
        time = GameSceneManager.GetClearTime();
        wave = EnemyManager.GetWaveGameOver();
        Text text = text_obj.GetComponent<Text>();
        text.text = "ゲージ使用回数 : " + gauge + "   タイム : " + time + "   最終Wave : " + wave;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGM;
        audioSource.Play();
    }
    void Update()
    {
        audioTimer += Time.deltaTime;

        if(audioTimer<0.5f)
        {
            audioSource.volume = 0;
        }
        else
        {
            vol += Time.deltaTime * 0.1f;
            if(vol< optionscript.GetBGMVolume())
            {
                audioSource.volume = vol;
            }
            else
            {
                audioSource.volume = optionscript.GetBGMVolume();
            }
        }




        
        if (!oneFadeFlag)
        {
            fade.GetComponent<FadeStart>().FadeInA();
            oneFadeFlag = true;
        }
    }
}
