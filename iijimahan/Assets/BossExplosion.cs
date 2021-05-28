﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosion : MonoBehaviour
{
    [SerializeField] private GameObject Explosion;
    private BossHp hp;
    private bool EffectFlag;
    [SerializeField, Header("爆発位置")] private Vector3[] effectpos = new Vector3[4];
    [SerializeField, Header("爆発サイズ(小さい)")]private Vector3 effectScaleSmall = new Vector3(1.0f, 1.0f, 1.0f);
    [SerializeField, Header("爆発サイズ(大きい)")]private Vector3 effectScaleBig = new Vector3(2.5f, 2.5f, 2.5f);
    [SerializeField, Header("ボスの爆発中の速さ")]private Vector3 speed = new Vector3(-0.1f, -0.1f, 0.0f);
    [SerializeField, Header("ボスの完全爆発までの時間")] private int time = 3;
    [SerializeField, Header("爆発SE")] private AudioClip ExplosionSE;
    private GameObject option;
    private Option script;
    private AudioSource audiosource;


    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        hp = gameObject.GetComponent<BossHp>();
        EffectFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp.Hp <= 0 && EffectFlag == false)
        {
            this.tag = "Untagged";
            StartCoroutine("ExplosionEffect");
            EffectFlag = true;
        }
        if(EffectFlag == true)
        {
            transform.position += speed * Time.deltaTime;
        }
    }

    IEnumerator ExplosionEffect()
    {
        audiosource.volume = script.GetSEVolume();
        for (int i = 0; i < time; i++)
        {
            GameObject effect = Instantiate(Explosion);
            effect.transform.position = transform.position + effectpos[0];
            effect.transform.localScale = effectScaleSmall;
            audiosource.PlayOneShot(ExplosionSE);
            yield return new WaitForSeconds(0.5f);

            GameObject effect2 = Instantiate(Explosion);
            effect2.transform.position = transform.position + effectpos[1];
            effect2.transform.localScale = effectScaleSmall;
            audiosource.PlayOneShot(ExplosionSE);
            yield return new WaitForSeconds(0.5f);

            GameObject effect3 = Instantiate(Explosion);
            effect3.transform.position = transform.position + effectpos[2];
            effect3.transform.localScale = effectScaleSmall;
            audiosource.PlayOneShot(ExplosionSE);
            yield return new WaitForSeconds(0.5f);

            GameObject effect4 = Instantiate(Explosion);
            effect4.transform.position = transform.position + effectpos[3];
            effect4.transform.localScale = effectScaleBig;
            audiosource.PlayOneShot(ExplosionSE);
            yield return new WaitForSeconds(0.1f);
            Debug.Log(i + "回目");
        }
        Debug.Log("おわり");
        gameObject.SetActive(false);
    }
}
