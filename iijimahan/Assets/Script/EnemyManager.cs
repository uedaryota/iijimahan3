using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private enum wave_enemy
    {
        Wave1の敵,
        Wave2の敵,
        Wave3の敵,
        Wave4の敵,
        Wave5の敵,
        Wave6の敵,
        Wave7の敵,
        Wave8の敵,
        Wave9の敵,
        Wave10の敵,
    }

    private enum enemy_box
    {
        敵1体目,
        敵2体目,
        敵3体目,
        敵4体目,
        敵5体目,
        敵6体目,
        敵7体目,
        敵8体目,
        敵9体目,
        敵10体目,
        敵11体目,
        敵12体目,
        敵13体目,
        敵14体目,
        敵15体目,
        敵16体目,
        敵17体目,
        敵18体目,
        敵19体目,
        敵20体目,
        敵31体目,
        敵32体目,
        敵33体目,
        敵34体目,
        敵35体目,
        敵36体目,
        敵37体目,
        敵38体目,
        敵39体目,
        敵41体目,
        敵42体目,
        敵43体目,
        敵44体目,
        敵45体目,
        敵46体目,
        敵47体目,
        敵48体目,
        敵49体目,
        敵51体目,
        敵52体目,
        敵53体目,
        敵54体目,
        敵55体目,
        敵56体目,
        敵57体目,
        敵58体目,
        敵59体目,
        敵61体目,
        敵62体目,
        敵63体目,
        敵64体目,
        敵65体目,
        敵66体目,
        敵67体目,
        敵68体目,
        敵69体目,
        敵71体目,
        敵72体目,
        敵73体目,
        敵74体目,
        敵75体目,
        敵76体目,
        敵77体目,
        敵78体目,
        敵79体目,
        敵81体目,
        敵82体目,
        敵83体目,
        敵84体目,
        敵85体目,
        敵86体目,
        敵87体目,
        敵88体目,
        敵89体目,
        敵91体目,
        敵92体目,
        敵93体目,
        敵94体目,
        敵95体目,
        敵96体目,
        敵97体目,
        敵98体目,
        敵99体目,
    }
    
    [System.Serializable]
    public struct WaveEnemy
    {
        [SerializeField, Header("生成するエネミーオブジェクト")]
        [EnumLabel(typeof(enemy_box))]
        public Object[] enemyboxes_Manual;
        [SerializeField, Header("生成数")]
        public int enemycountlimit_Manual;
        [SerializeField, Header("生成位置")]
        [EnumLabel(typeof(enemy_box))]
        public Vector2[] pos_Manual;
        [SerializeField, Header("生成間隔")]
        [EnumLabel(typeof(enemy_box))]
        public float[] interval_Manual;
    }

    [SerializeField, Header("wave毎の手入力エネミー")]
    [EnumLabel(typeof(wave_enemy))]
    public WaveEnemy[] enemy_Manual;
        

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
