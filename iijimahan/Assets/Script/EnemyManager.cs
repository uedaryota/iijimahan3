using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField, Header("enemyboxオブジェクト")] Object[] enemyboxes;
    [SerializeField, Header("bossオブジェクト")] Object[] bossboxes;
    [SerializeField, Header("wave毎のenemyの生成上限")] int[] enemycountlimit;
    [SerializeField, Header("wave毎の生成間隔")] float[] interval;
    [SerializeField, Header("enemyのランダム生成用のy座標(高)")] float y_high = 0;
    [SerializeField, Header("enemyのランダム生成用のy座標(低)")] float y_low = 0;
    [SerializeField, Header("enemyのランダム生成用のx座標(高)")] float x_high = 0;
    [SerializeField, Header("enemyのランダム生成用のx座標(低)")] float x_low = 0;
    [SerializeField, Header("bossの生成用のx座標")] float x_bosspos = 0;
    [SerializeField, Header("bossの生成用のy座標")] float y_bosspos = 0;

    public int wave = 1;

    private Object[] tagcheckenemy;
    private float timer;
    private float x_rnd;
    private float y_rnd;
    private int old_wave;
    private float EnemyChackInterval = 1;

    private int enemyrnd;
    private int enemycount;//エネミー生成用のカウント
    private bool bosschack;
    
    void Start()
    {
        bosschack = true;
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
                enemyrnd = Random.Range(0, 1);
                EnemyRespawn();
                return;

            case 2:
                enemyrnd = Random.Range(0, 1);
                BossRespawn();
                EnemyRespawn();
                return;

            default:
                Debug.Log("存在しないWaveに到達しました");
                return;
        }
    }

    void EnemyRespawn()
    {
        timer += Time.deltaTime;
        
        //現在のwaveで生成するエネミーの数に足りているか
        if (enemycountlimit[wave - 1] > enemycount)
        {
            //一定時間毎に
            if (timer > interval[wave - 1])
            {
                timer = 0;

                enemycount++;

                //x座標のランダム生成
                x_rnd = Random.Range(x_low, x_high);

                //y座標のランダム生成
                y_rnd = Random.Range(y_low, y_high);

                //ランダム座標を割り当て
                transform.position = new Vector3(x_rnd, y_rnd, 0);

                //エネミーの生成
                Instantiate(enemyboxes[enemyrnd], transform.position, Quaternion.identity);
            }
        }
        else
        {
            EnemyCheck("Enemy");
        }
    }

    void BossRespawn()
    {
        if (bosschack == true)
        {
            //ボスの座標指定
            transform.position = new Vector3(x_bosspos, y_bosspos, 0);
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
        //一定時間毎に
        if (timer > EnemyChackInterval)
        {
            timer = 0;
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
}
