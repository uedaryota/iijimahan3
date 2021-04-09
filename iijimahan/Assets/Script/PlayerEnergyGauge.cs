using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergyGauge : MonoBehaviour
{
    [SerializeField]
    public Image GreenGauge;
    [SerializeField]
    public Image RedGauge;
    [SerializeField]
    public Image Waku;
    float greenGauge = 0;
    float redGauge = 0;
    float alpha = 0;
    bool UIStartFlag = false;

    void Start()
    {
        GreenGauge.color = new Color(GreenGauge.color.r, GreenGauge.color.g, GreenGauge.color.b, alpha);
        RedGauge.color = new Color(RedGauge.color.r, RedGauge.color.g, RedGauge.color.b, alpha);
        Waku.color = new Color(Waku.color.r, Waku.color.g, Waku.color.b, alpha);
    }
    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;

        //ゲームが始まったら出てくる
        if(UIStartFlag)
        {
            StartUIAlpha();
        }

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Debug.Log("aiueo");
        //    greenGauge = 10;
        //    redGauge = 10f / 100f;
        //    Debug.Log("g" + redGauge);
        //}
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    Debug.Log("aiueo");
        //    GreenGauge.fillAmount += 0.1f;
        //}
        // GreenGauge.fillAmount -= greenDamage/100;
        if (greenGauge != 0)
        {
            float num = greenGauge / 100;
            GreenGauge.fillAmount -= num;
            greenGauge = 0;
        }
        if (RedGauge.fillAmount >= GreenGauge.fillAmount)
        {
            //Debug.Log(redDamage);
            RedGauge.fillAmount -= redGauge * Time.deltaTime;
            if (RedGauge.fillAmount == GreenGauge.fillAmount)
            {
                redGauge = 0;
            }
        }
        else
        {
            RedGauge.fillAmount = GreenGauge.fillAmount;
        }
    }

    public void SetGauge(float dame)
    {
        greenGauge -= dame/100f;
        redGauge = dame / 100f;
        GreenGauge.fillAmount -= dame / 100f;
    }

    public void UpGauge(float num)
    {
        greenGauge += num/100f;
        GreenGauge.fillAmount += num / 100f;
    }

    public void StartUIAlpha()
    {
        GreenGauge.color = new Color(GreenGauge.color.r, GreenGauge.color.g, GreenGauge.color.b, alpha);
        RedGauge.color = new Color(RedGauge.color.r, RedGauge.color.g, RedGauge.color.b, alpha);
        Waku.color = new Color(Waku.color.r, Waku.color.g, Waku.color.b, alpha);

        alpha += 2.5f * Time.deltaTime;

        if (alpha >= 1.0f)
        {
            UIStartFlag = true;
        }
    }

    public void SetStartUIflag(bool fl)
    {
       UIStartFlag = fl;
    }

}
