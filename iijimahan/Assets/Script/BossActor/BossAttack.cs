﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Acter
{
    Start,Attack,
    End,
}
public class BossAttack : MonoBehaviour
{
    int Cnt = 0;
    Acter act;
    [SerializeField, Header("ボス攻撃SE")] public AudioClip BulletSE;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        act = Acter.Start;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;
        switch (act)
        {
            case Acter.Start:
                if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossFirstAction>().action==BossFirstAction.MoveAction.End)
                {
                    act = Acter.Attack;
                }
                break;
            case Acter.Attack:
                if (Cnt * Time.deltaTime >1 )
                {
                    for (int x = 0; x < 15; x++)
                    {
                        audioSource.PlayOneShot(BulletSE);
                        GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet2(transform.position, 0);
                    }
                    Cnt = 0;
                    if(GetComponent<BossHp>().status==BossHp.Status.Damege)
                    {
                        for (int x = 0; x < 3; x++)
                        {
                            audioSource.PlayOneShot(BulletSE);
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet4(transform.position, 1);
                        }
                        audioSource.PlayOneShot(BulletSE);
                        GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 1);
                       
                    }
                }
                Cnt++;
                break;
        }
    }

}
