﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField, Header("enemyboxオブジェクト手入力用")] Object[] enemyboxes_Manual;
    [SerializeField, Header("enemynoの生成位置手入力用")] Vector2[] pos_Manual;
    [SerializeField, Header("生成間隔手入力用")] float[] interval_Manual;

    [SerializeField, Header("enemyboxオブジェクト")] Object[] enemyboxes;
    [SerializeField, Header("bossオブジェクト")] Object[] bossboxes;
    [SerializeField, Header("wave毎のenemyの生成上限")] int[] enemycountlimit;
    [SerializeField, Header("wave毎の生成間隔")] float[] interval;
    [SerializeField, Header("wave毎の沸き方(1:上下交互, 2:上2回下2回)")] int[] waverespawn;
    [SerializeField, Header("enemyの生成用の座標")] Vector2 pos = new Vector2(20, 20);
    [SerializeField, Header("bossの生成用の座標")] Vector2 bosspos = new Vector2(20, 0);



    public int wave = 1;

    private bool gameclear;
    private Object[] tagcheckenemy;
    private float timer;
    private float timer2;
    private float x_rnd;
    private float y_rnd;
    private int old_wave;
    private float EnemyChackInterval = 1;
    private float old_pos_chack;
    private float old_old_pos_chack;

    private int enemyrnd;
    private int enemycount;//エネミー生成用のカウント
    private bool bosschack;
    
    void Start()
    {
        bosschack = true;
        gameclear = false;
        old_pos_chack = pos.y;
        old_old_pos_chack *= -pos.y;
    }

    void Update()
    {
        if(old_wave != wave)
        {
            enemycount = 0;
            if(bosschack == false)
            {
                bosschack = true;
            }
        }
        old_wave = wave;

        switch (wave)
        {
            case 1:
                EnemyRespawn(waverespawn[wave - 1]);
                return;

            case 2:
                BossRespawn();
                EnemyRespawn(waverespawn[wave - 1]);
                return;

            case 3:
                gameclear = true;
                return;

            default:
                Debug.Log("存在しないWaveに到達しました");
                return;
        }
    }

    void EnemyRespawn(int respawn)
    {
        timer += Time.deltaTime;
        
        //現在のwaveで生成するエネミーの数に足りているか
        if (enemycountlimit[wave - 1] > enemycount )
        {
            //一定時間毎に
            if (timer >= interval[wave - 1])
            {
                timer = 0;
                enemycount++;
                old_old_pos_chack = old_pos_chack;
                old_pos_chack = pos.y;
                enemyrnd = Random.Range(0, enemyboxes.Length);
                switch (respawn)
                {
                    case 1:
                        if(old_pos_chack == pos.y)
                        {
                            pos.y *= -1;
                        }
                        //座標の割り当て
                        transform.position = new Vector3(pos.x, pos.y, 0);
                        //エネミーの生成
                        Instantiate(enemyboxes[enemyrnd], transform.position, Quaternion.identity);
                        return;

                    case 2:
                        if(old_old_pos_chack == pos.y)
                        {
                            pos.y *= -1;
                        }
                        //座標の割り当て
                        transform.position = new Vector3(pos.x, pos.y, 0);
                        //エネミーの生成
                        Instantiate(enemyboxes[enemyrnd], transform.position, Quaternion.identity);
                        return;

                    default:
                        //座標の割り当て
                        transform.position = new Vector3(pos.x, pos.y, 0);
                        //エネミーの生成
                        Instantiate(enemyboxes[enemyrnd], transform.position, Quaternion.identity);
                        return;
                }
            }
        }
        else
        {
            EnemyCheck("Enemy");
        }
    }

    void EnemyRespawn2()
    {

    }

    void BossRespawn()
    {
        if (bosschack == true)
        {
            //ボスの座標指定
            transform.position = new Vector3(bosspos.x, bosspos.y, 0);
            //ボスの生成
            Instantiate(bossboxes[wave / 2 - 1], transform.position, Quaternion.identity);
            Debug.Log("ボス召喚");
            bosschack = false;
        }
        else
        {
            EnemyCheck("Boss");
        }
    }

    void EnemyCheck(string tagname)
    {
        timer2 += Time.deltaTime;
        //一定時間毎に
        if (timer2 > EnemyChackInterval)
        {
            timer2 = 0;
            //エネミーが何体居るかの確認
            tagcheckenemy = GameObject.FindGameObjectsWithTag(tagname);
            //エネミーが存在しなくなったとき
            if (tagcheckenemy.Length == 0)
            {
                //waveが進む
                wave++;
            }
        }
    }
    
    public bool GetGameClear()
    {
        return gameclear;
    }
    
    public int GetWave()
    {
        return wave;
    }
}
