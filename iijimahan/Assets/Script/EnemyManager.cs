using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public struct WaveEnemy
    {
        [SerializeField, Header("生成するエネミーオブジェクト")]
        public Object[] enemyboxes_Manual;
        [SerializeField, Header("生成数")]
        public int enemycountlimit_Manual;
        [SerializeField, Header("生成位置")]
        public Vector2[] pos_Manual;
        [SerializeField, Header("生成間隔")]
        public float[] interval_Manual;
    }

    [SerializeField, Header("wave毎の手入力エネミー")] WaveEnemy[] enemy_Manual;

    [SerializeField, Header("enemyboxオブジェクト")] Object[] enemyboxes;
    [SerializeField, Header("bossオブジェクト")] Object[] bossboxes;
    [SerializeField, Header("wave毎のenemyの生成上限")] int[] enemycountlimit;
    [SerializeField, Header("wave毎の生成間隔")] float[] interval;
    [SerializeField, Header("wave毎の沸き方(1:上下交互, 2:上2回下2回)")] int[] waverespawn;
    [SerializeField, Header("enemyの生成用の座標")] Vector2 pos = new Vector2(20, 20);
    [SerializeField, Header("bossの生成用の座標")] Vector2 bosspos = new Vector2(20, 0);

    //手動用
    private float timer_Manual;
    private int interval_count;

    //自動用

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
        interval_count = 0;
        //enemy_Manual = new WaveEnemy[wave - 1];
    }

    void Update()
    {
        if(old_wave != wave)
        {
            enemycount = 0;
            interval_count = 0;
            if(bosschack == false)
            {
                bosschack = true;
            }
            //enemy_Manual = new WaveEnemy[wave - 1];
        }
        old_wave = wave;

        switch (wave)
        {
            case 1:
                EnemyRespawn_Manual();
                return;

            case 2:
                BossRespawn();
                EnemyRespawn(waverespawn[wave - 1]);
                return;

            case 3:
                EnemyRespawn_Manual();
                return;

            case 4:
                BossRespawn();
                EnemyRespawn(waverespawn[wave - 1]);
                return;

            case 5:
                EnemyRespawn_Manual();
                return;

            case 6:
                BossRespawn();
                EnemyRespawn(waverespawn[wave - 1]);
                return;
                
            case 7:
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
        if (enemycountlimit[wave - 1] > enemycount || enemycountlimit[wave - 1]  > 100)
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

    void EnemyRespawn_Manual()
    {
        timer_Manual += Time.deltaTime;

        //現在のwaveで生成するエネミーの数に足りているか
        if (enemy_Manual[wave-1].enemycountlimit_Manual > enemycount)
        {
            //一定時間毎に
            if (timer_Manual >= enemy_Manual[wave - 1].interval_Manual[interval_count])
            {
                //座標の割り当て
                transform.position = enemy_Manual[wave - 1].pos_Manual[enemycount];
                //エネミーの生成
                Instantiate(enemy_Manual[wave - 1].enemyboxes_Manual[enemycount], transform.position, Quaternion.identity);
                //ループ処理用の変数達
                timer_Manual = 0;
                interval_count++;
                enemycount++;
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
