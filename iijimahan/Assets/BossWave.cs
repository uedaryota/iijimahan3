using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossWave : MonoBehaviour
{
    [SerializeField, Header("フェードする速さ")] public float fadespeed = 1.0f;
    [SerializeField, Header("フェードする回数(偶数回で入力する)")] public int fadecount = 4;

    private GameObject sprite;
    private GameObject EnemyManager;
    private UnityEngine.Color color;
    private int fadenum;
    private int count;
    private int Wave;
    private int old_wave;

    void Start()
    {
        fadenum = 0;
        count = 100;
        Vector4 str = new Vector4(1, 1, 1, 0);
        sprite = this.gameObject;
        sprite.GetComponent<Image>().color = str;
        EnemyManager = GameObject.Find("EnemyManager");
    }

    // Update is called once per frame
    void Update()
    {
        Wave = EnemyManager.GetComponent<EnemyManager>().wave;
        if (old_wave != Wave && Wave % 2 == 0)
        {
            count = 0;
        }
        if (fadecount > count)
        {
            fadeFunc();
        }
        old_wave = Wave;
    }

    void fadeFunc()
    {
        switch (fadenum)
        {
            case 0:
                if (sprite.GetComponent<Image>().color.a < 1)
                {
                    color = sprite.GetComponent<Image>().color;
                    color.a = color.a + (Time.deltaTime * fadespeed);
                    sprite.GetComponent<Image>().color = color;
                }
                else
                {
                    fadenum = 1;
                    count++;
                }
                return;

            case 1:
                if (sprite.GetComponent<Image>().color.a > 0)
                {
                    color = sprite.GetComponent<Image>().color;
                    color.a = color.a - (Time.deltaTime * fadespeed);
                    sprite.GetComponent<Image>().color = color;
                }
                else
                {
                    fadenum = 0;
                    count++;
                }
                return;

            default:
                Debug.Log("fadeにてエラー発生");
                return;
        }
    }
}
