using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossWave : MonoBehaviour
{
    [SerializeField, Header("Warningのフェードする速さ")] public float Warningfadespeed = 1.0f;
    [SerializeField, Header("Warningフェードする回数(偶数回で入力する)")] public int fadecount = 4;

    [SerializeField, Header("手配書のフェードする速さ")] public float fadespeed = 2.0f;
    [SerializeField, Header("手配書の表示時間")] public float time = 3.0f;

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
        if (gameObject.name == "Warning" || gameObject.name == "Warning2")
        {
            if (old_wave != Wave && Wave % 2 == 0)
            {
                count = 0;
            }
            if (fadecount > count)
            {
                fadeFuncWarning();
            }
            old_wave = Wave;
        }
        else
        {
            switch (Wave)
            {
                case 2:
                    if (gameObject.name == "Boss1")
                    {
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
                    return;

                case 4:
                    if (gameObject.name == "Boss2")
                    {
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
                    return;

                case 6:
                    if (gameObject.name == "Boss3")
                    {
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
                    return;
            }
        }
    }

    void fadeFuncWarning()
    {
        switch (fadenum)
        {
            case 0:
                if (sprite.GetComponent<Image>().color.a < 1)
                {
                    color = sprite.GetComponent<Image>().color;
                    color.a = color.a + (Time.deltaTime * Warningfadespeed);
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
                    color.a = color.a - (Time.deltaTime * Warningfadespeed);
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
                }
                return;

            case 1:
                time -= Time.deltaTime;
                if(time <= 0)
                {
                    fadenum = 2;
                }

                return;

            case 2:
                if (sprite.GetComponent<Image>().color.a > -10)
                {
                    color = sprite.GetComponent<Image>().color;
                    color.a = color.a - (Time.deltaTime * fadespeed);
                    sprite.GetComponent<Image>().color = color;
                }
                return;

            default:
                Debug.Log("fadeにてエラー発生");
                return;
        }
    }
}
