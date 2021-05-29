using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    #region Enum一覧

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
        Wave11の敵,
        Wave12の敵,
        Wave13の敵,
        Wave14の敵,
        Wave15の敵,
        Wave16の敵,
        Wave17の敵,
        Wave18の敵,
        Wave19の敵,
        Wave20の敵,
    }

    private enum wave_enemy2
    {
        Wave2の敵,
        Wave4の敵,
        Wave6の敵,
        Wave8の敵,
        Wave10の敵,
        Wave12の敵,
        Wave14の敵,
        Wave16の敵,
        Wave18の敵,
        Wave20の敵,
    }

    private enum wave_boss
    {
        Wave2のボス,
        Wave4のボス,
        Wave6のボス,
        Wave8のボス,
        Wave10のボス,
        Wave12のボス,
        Wave14のボス,
        Wave16のボス,
        Wave18のボス,
        Wave20のボス,
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
        敵21体目,
        敵22体目,
        敵23体目,
        敵24体目,
        敵25体目,
        敵26体目,
        敵27体目,
        敵28体目,
        敵29体目,
        敵30体目,
        敵31体目,
        敵32体目,
        敵33体目,
        敵34体目,
        敵35体目,
        敵36体目,
        敵37体目,
        敵38体目,
        敵39体目,
        敵40体目,
        敵41体目,
        敵42体目,
        敵43体目,
        敵44体目,
        敵45体目,
        敵46体目,
        敵47体目,
        敵48体目,
        敵49体目,
        敵50体目,
        敵51体目,
        敵52体目,
        敵53体目,
        敵54体目,
        敵55体目,
        敵56体目,
        敵57体目,
        敵58体目,
        敵59体目,
        敵60体目,
        敵61体目,
        敵62体目,
        敵63体目,
        敵64体目,
        敵65体目,
        敵66体目,
        敵67体目,
        敵68体目,
        敵69体目,
        敵70体目,
        敵71体目,
        敵72体目,
        敵73体目,
        敵74体目,
        敵75体目,
        敵76体目,
        敵77体目,
        敵78体目,
        敵79体目,
        敵80体目,
        敵81体目,
        敵82体目,
        敵83体目,
        敵84体目,
        敵85体目,
        敵86体目,
        敵87体目,
        敵88体目,
        敵89体目,
        敵90体目,
        敵91体目,
        敵92体目,
        敵93体目,
        敵94体目,
        敵95体目,
        敵96体目,
        敵97体目,
        敵98体目,
        敵99体目,
        敵101体目,
        敵102体目,
        敵103体目,
        敵104体目,
        敵105体目,
        敵106体目,
        敵107体目,
        敵108体目,
        敵109体目,
        敵110体目,
        敵111体目,
        敵112体目,
        敵113体目,
        敵114体目,
        敵115体目,
        敵116体目,
        敵117体目,
        敵118体目,
        敵119体目,
        敵120体目,
        敵121体目,
        敵122体目,
        敵123体目,
        敵124体目,
        敵125体目,
        敵126体目,
        敵127体目,
        敵128体目,
        敵129体目,
        敵130体目,
        敵131体目,
        敵132体目,
        敵133体目,
        敵134体目,
        敵135体目,
        敵136体目,
        敵137体目,
        敵138体目,
        敵139体目,
        敵140体目,
        敵141体目,
        敵142体目,
        敵143体目,
        敵144体目,
        敵145体目,
        敵146体目,
        敵147体目,
        敵148体目,
        敵149体目,
        敵150体目,
        敵151体目,
        敵152体目,
        敵153体目,
        敵154体目,
        敵155体目,
        敵156体目,
        敵157体目,
        敵158体目,
        敵159体目,
        敵160体目,
        敵161体目,
        敵162体目,
        敵163体目,
        敵164体目,
        敵165体目,
        敵166体目,
        敵167体目,
        敵168体目,
        敵169体目,
        敵170体目,
        敵171体目,
        敵172体目,
        敵173体目,
        敵174体目,
        敵175体目,
        敵176体目,
        敵177体目,
        敵178体目,
        敵179体目,
        敵180体目,
        敵181体目,
        敵182体目,
        敵183体目,
        敵184体目,
        敵185体目,
        敵186体目,
        敵187体目,
        敵188体目,
        敵189体目,
        敵190体目,
        敵191体目,
        敵192体目,
        敵193体目,
        敵194体目,
        敵195体目,
        敵196体目,
        敵197体目,
        敵198体目,
        敵199体目,
    }

    private enum wave_interval
    {
        StartWave1,
        Wave1Wave2,
        Wave2Wave3,
        Wave3Wave4,
        Wave4Wave5,
        Wave5Wave6,
        Wave6Wave7,
        Wave7Wave8,
        Wave8Wave9,
        Wave9Wave10,
        Wave10Wave11,
        Wave11Wave12,
        Wave12Wave13,
        Wave13Wave14,
        Wave14Wave15,
        Wave15Wave16,
        Wave16Wave17,
        Wave17Wave18,
        Wave18Wave19,
        Wave19Wave20,
    }

    #endregion

    #region 変数一覧

    public int wave = 1;
    [SerializeField, Header("ボスWave突入SE")] private AudioClip caution;
    
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

    [SerializeField, Header("最大Wave")]
    private int maxwave = 6;

    [SerializeField, Header("wave毎の手入力エネミー")]
    [EnumLabel(typeof(wave_enemy))]
    public WaveEnemy[] enemy_Manual;

    [SerializeField, Header("Wave毎に手動エネミーをリピートで無限に生成するか")]
    [EnumLabel(typeof(wave_enemy))]
    public bool[] Repeat;

    [SerializeField, Header("WaveとWaveの間のインターバル")]
    [EnumLabel(typeof(wave_interval))]
    private float[] wave_wave_interval;

    [SerializeField, Header("enemyboxオブジェクト")]
    [EnumLabel(typeof(enemy_box))]
    Object[] enemyboxes;
    [SerializeField, Header("bossオブジェクト")]
    [EnumLabel(typeof(wave_boss))]
    Object[] bossboxes;
    [SerializeField, Header("wave毎のenemyの生成上限")]
    [EnumLabel(typeof(wave_enemy2))]
    int[] enemycountlimit;
    [SerializeField, Header("wave毎の生成間隔")]
    [EnumLabel(typeof(wave_enemy2))]
    float[] interval;
    [SerializeField, Header("wave毎の沸き方(1:上下交互, 2:上2回下2回)")]
    [EnumLabel(typeof(wave_enemy2))]
    int[] waverespawn;
    [SerializeField, Header("enemyの生成用の座標")] Vector2 pos = new Vector2(20, 20);
    [SerializeField, Header("bossの生成用の座標")] Vector2 bosspos = new Vector2(20, 0);

    [SerializeField, Header("ボーナスウェーブのエネミー")]
    public WaveEnemy bonus_enemy;

    //手動用
    private float timer_Manual;
    private int interval_count;

    //自動用
    private bool gameclear;
    private Object[] tagcheckenemy;
    private float timer;
    private float timer2;
    private float x_rnd;
    private float y_rnd;
    private int old_wave;
    private float EnemyChackInterval = 0.5f;
    private float old_pos_chack;
    private float old_old_pos_chack;

    private int enemyrnd;
    private int enemycount;//エネミー生成用のカウント
    private bool bosschack;

    private float wave_timer;

    private static int waveGameOver;

    AudioSource audioSource;

    private GameObject option;
    private Option optionscript;

    public static bool bonuswave;
    private int wait_enemycount;
    private int bonuswave_switch;
    private bool BossDeadFlag;


    #endregion

    void Start()
    {
        option = GameObject.Find("Option");
        optionscript = option.GetComponent<Option>();
        bosschack = true;
        gameclear = false;
        old_pos_chack = pos.y;
        old_old_pos_chack *= -pos.y;
        interval_count = 0;
        wave_timer = 0;
        audioSource = GetComponent<AudioSource>();
        bonuswave = false;
        bonuswave_switch = 0;
        BossDeadFlag = false;
    }

    void Update()
    {
        #region デバッグコマンド

        /*
        if (Input.GetKey(KeyCode.Keypad1))
        {
            wave = 1;
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            wave = 2;
        }
        if (Input.GetKey(KeyCode.Keypad3))
        {
            wave = 3;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            wave = 4;
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            wave = 5;
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            wave = 6;
        }

        */
        #endregion

        if(BossDeadFlag == true)
        {
            ObjectCheck("BossDead");
            playerGetBossTag = "BossDead";
        }

        if (bonuswave == true)
        {
            timer_Manual += Time.deltaTime;
            switch (bonuswave_switch)
            {
                case 0:
                    wait_enemycount = enemycount;
                    enemycount = 0;
                    interval_count = 0;
                    wave_timer = 0;
                    bonuswave_switch++;
                    return;

                case 1:
                    //現在のwaveで生成するエネミーの数に足りているか
                    if (bonus_enemy.enemycountlimit_Manual > enemycount)
                    {
                        //一定時間毎に
                        if (timer_Manual >= bonus_enemy.interval_Manual[interval_count])
                        {
                            //座標の割り当て
                            transform.position = bonus_enemy.pos_Manual[enemycount];
                            //エネミーの生成
                            Instantiate(bonus_enemy.enemyboxes_Manual[enemycount], transform.position, Quaternion.identity);
                            //ループ処理用の変数達
                            timer_Manual = 0;
                            interval_count++;
                            enemycount++;
                        }
                    }
                    else
                    {
                        BonusEnemyCheck("Enemy");
                    }
                    return;
            }
        }
        waveGameOver = wave;
        if(old_wave != wave)
        {
            enemycount = 0;
            interval_count = 0;
            wave_timer = 0;
            BossDeadFlag = false;

            if(wave % 2 == 0)
            {
                audioSource.volume = optionscript.GetSEVolume();
                audioSource.PlayOneShot(caution);
            }

            if(bosschack == false)
            {
                bosschack = true;
            }
        }
        old_wave = wave;

        if (bonuswave == false)
        {
            //新しいwaveならWaveTimerでカウント
            if (wave_wave_interval[wave - 1] >= wave_timer)
            {
                wave_timer += Time.deltaTime;
            }
            //インターバルを迎えたら通常の生成に移行
            else
            {
                if (wave % 2 == 1)
                {
                    playerGetBossTag = "None";
                    EnemyRespawn_Manual();
                }
                else if (wave % 2 == 0)
                {                    
                    BossRespawn();
                    EnemyRespawn_Manual();
                    EnemyRespawn(waverespawn[wave / 2 - 1]);
                }
            }
        }
    }

    void EnemyRespawn(int respawn)
    {

        if (BossDeadFlag == true) return;
        timer += Time.deltaTime;
        //現在のwaveで生成するエネミーの数に足りているか
        if (enemycountlimit[wave / 2 - 1] > enemycount || enemycountlimit[wave / 2 - 1]  > 100)
        {
            //一定時間毎に
            if (timer >= interval[wave / 2 - 1])
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
            ObjectCheck("Enemy");
        }
    }

    void EnemyRespawn_Manual()
    {
        if (BossDeadFlag == true) return;
        if (enemy_Manual[wave - 1].enemycountlimit_Manual <= 0) return;
        timer_Manual += Time.deltaTime;
        if (Repeat[wave - 1] == true)
        {
            //現在のwaveで生成するエネミーの数に足りているか
            if (enemy_Manual[wave - 1].enemycountlimit_Manual > enemycount)
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
                enemycount = 0;
                interval_count = 0;
            }
        }
        else
        {
            //現在のwaveで生成するエネミーの数に足りているか
            if (enemy_Manual[wave - 1].enemycountlimit_Manual > enemycount)
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
                ObjectCheck("Enemy");
            }
        }
    }

    void BossRespawn()
    {
        //ボスが
        if (bosschack == true)
        {
            //ボスの座標指定
            transform.position = new Vector3(bosspos.x, bosspos.y, 0);
            //ボスの生成
            Instantiate(bossboxes[wave / 2 - 1], transform.position, Quaternion.identity);
            bosschack = false;
        }
        else
        {
            ObjectCheck("Boss");
        }
    }

    void ObjectCheck(string tagname)
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
                if (tagname == "Boss")
                {
                    BossDeadFlag = true;
                }
                if(tagname == "Enemy")
                {
                    if (wave == maxwave)
                    {
                        gameclear = true;
                    }
                    else
                    {
                        //waveが進む
                        wave++;
                    }
                }
                if(tagname == "BossDead")
                {
                    
                    if (wave == maxwave)
                    {
                        gameclear = true;
                    }
                    else
                    {
                        BossDeadFlag = false;
                        //waveが進む
                        wave++;
                    }
                }
            }
        }
    }

    void BonusEnemyCheck(string tagname)
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
                //ボーナスウェーブ終了
                bonuswave_switch = 0;
                bonuswave = false;
                enemycount = wait_enemycount;

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

    public static int GetWaveGameOver()
    {
        return waveGameOver;
    }

    public bool GetBonusWave()
    {
        return bonuswave;
    }

    public bool SetBonusWave(bool flag)
    {
        return bonuswave = flag;
    }

    public bool GetBossDead()
    {
        return BossDeadFlag;
    }

    private string playerGetBossTag;

    public string GetBossTag()
    {
        return playerGetBossTag;
    }
}
