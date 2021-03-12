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
    float greenGauge = 0;
    float redGauge = 0;
    int poolDamage = 0;

    void Start()
    {

        
    }
    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("aiueo");
            greenGauge = 10;
            redGauge = 10f / 100f;
            Debug.Log("g" + redGauge);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("aiueo");
            GreenGauge.fillAmount += 0.1f;
        }
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
        //Debug.Log("numnunm");
    }
}
