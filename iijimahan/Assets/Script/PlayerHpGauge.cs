using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpGauge : MonoBehaviour
{
    [SerializeField]
    public Image GreenGauge;
    [SerializeField]
    public Image RedGauge;
    float greenDamage = 0;
    float redDamage = 0;
    int poolDamage = 0;

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Debug.Log("aiueo");
        //    greenDamage = 10;
        //    redDamage = 10f / 100f;
        //    Debug.Log("g" + redDamage);
        //}
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    Debug.Log("aiueo");
        //    GreenGauge.fillAmount += 0.1f;
        //}
        // GreenGauge.fillAmount -= greenDamage/100;
        if (greenDamage != 0)
        {
            float num = greenDamage / 100;
            GreenGauge.fillAmount -= num;
            greenDamage = 0;
        }
        if (RedGauge.fillAmount >= GreenGauge.fillAmount)
        {
            //Debug.Log(redDamage);
            RedGauge.fillAmount -= redDamage * Time.deltaTime;
            if (RedGauge.fillAmount == GreenGauge.fillAmount)
            {
                redDamage = 0;
            }
        }
        else
        {
            RedGauge.fillAmount = GreenGauge.fillAmount;
        }
    }

    public void Damage(float dame)
    {      
        greenDamage = dame;
        redDamage = dame / 100f;
    }
}
