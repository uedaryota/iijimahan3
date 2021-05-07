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
        
        maxHp = gameObject.GetComponent<BossHp>().MaxHp;
        slider.value = maxHp;
        currentHp = maxHp;
    }
    public void Damage(float x)
    {
        float damage = x;
        currentHp = currentHp - damage;
        slider.value = (float)currentHp / (float)maxHp;
    }
}