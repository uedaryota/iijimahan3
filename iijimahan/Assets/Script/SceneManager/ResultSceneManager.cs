using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    public GameObject text_obj;
    private PlayerControl script;
    private int gauge;
    private float time;
    public AudioClip BGM;
    AudioSource audioSource;

    void Start()
    {
        gauge = PlayerControl.GetGaugeCount();
        time = GameSceneManager.GetClearTime();
        Text text = text_obj.GetComponent<Text>();
        text.text = "ゲージ使用回数 : " + gauge + "      タイム : " + time;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
    void Update()
    {
    }
}
