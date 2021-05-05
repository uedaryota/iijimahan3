using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpGauge : MonoBehaviour
{
    [SerializeField]
    public Image GreenGauge;
    [SerializeField]
    public Image RedGauge;
    float greenDamage = 0;
    float redDamage = 0;
    float maxHp = 155;
    float currentHp;
    //Sliderを入れる
    public Slider slider;

    void Start()
    {
        slider.value = 1;
        maxHp = gameObject.GetComponent<BossHp>().MaxHp;
        currentHp = maxHp;
    }
    public void Damage(float x)
    {
        Debug.Log("damage:" + x);
        float damage = x;
        currentHp = currentHp - damage;
        slider.value = (float)currentHp / (float)maxHp;
        Debug.Log("slider.value : " + slider.value);
    }
}