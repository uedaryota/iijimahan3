using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerControl : MonoBehaviour
{
    [SerializeField, Header("プレイヤーの初期HP")]
    public float HP = 100;
    [SerializeField, Header("プレイヤーの移動速度")]
    public float speed = 10f;
    [SerializeField, Header("プレイヤーのが撃つ弾")]
    public GameObject bullet;
    [SerializeField, Header("プレイヤーのゲージ技の弾")]
    public GameObject gaugebullet;
    [SerializeField, Header("強化のゲージ技の弾")]
    public GameObject kyoukabullet;
    [SerializeField, Header("プレイヤーのモデル")]
    public GameObject model;
    //[SerializeField, Header("点滅周期")]
    //public float tenmetuInterval = 1.0f;
    [SerializeField, Header("エネルギーたまる量")]
    public int upenergy = 10;
    [SerializeField, Header("プレイヤーが受けるダメージ")]
    public float damage = 5f;
    [SerializeField, Header("レーザーのダメージ上限")]
    public float damagelimit = 10f;

    [SerializeField, Header("テストSE")]
    public AudioClip testAudio;
    [SerializeField, Header("プレイヤー弾撃つSE")]
    public AudioClip playerBulletSE;
    [SerializeField, Header("被弾SE")]
    public AudioClip dameageSE;
    [SerializeField, Header("プレイヤー弾ゲージショット反転SE")]
    public AudioClip playerGaugeShootHantenSE;
    [SerializeField, Header("プレイヤー弾ゲージショット強化SE")]
    public AudioClip playerGaugeShootKyoukaSE;
    [SerializeField, Header("エネルギー回収SE")]
    public AudioClip playerEnergyUpSE;

    //public GameObject bulletbox;

    public Rigidbody rd;

    private AudioSource audioSource;

    private PlayerHpGauge playerHpGauge;

    private PlayerEnergyGauge playerEnergyGauge;

    private GameObject GaugeUI;

    private Vector3 velocity;  
    private int gauge = 0;

    //private int timer = 0;//計測用

    private bool mutekiFlag = false;
    private bool tenmetuFlag = false;

    private Vector3 padvelocity;
    private Vector3 padRvelocity;
    private Vector3 padRvelocity2;
    private Vector3 poolvelocity = new Vector3(1, 0, 0);

    private bool keyboardFlag = false;

    private static int gaugeCount = 0;

    private GameObject rock;

    private Vector3 nockbackVel = Vector3.zero;

    private bool nockBackFlag = false;
    private float nockBackCount = 0;
    Vector3 rockPos;

    private int nomove = 240;
    private bool startFlag = false;

    public bool insekiHitFlag = false;
    private float insekicount = 0;
    private bool nockvelFlag = false;
    private int LaserCnt = 0;
    private float angle = 0;

    public GameObject playerBurst;
    private bool playerDeadEffectFlag = false;
    private bool deadMoveFlag = false;
    private float deadMoveSpeed = 0;
    private float easingCount = 0;
    //private bool easingFlag = false;
    private int num = 350;
    
    private float maxSpeed = 0.3f;
    private float clearMaxSpeed = 0.7f;
    private float bulletangle = 0;
    private bool clearFlag = false;
    private bool clearSceneFlag = false;

    public GameObject gaugeEffect;

    private float clearCount = 0;

    public MeshRenderer mRenderer;
    bool startOneFlag = false;

    private float bulletcounter = 0;

    private bool bossBarrierFlag = false;
    private float bossBarrierCounter = 0;
    
    private GameObject option;
    private Option optionscript;


    public GameObject hanabiEffect;
    private float movetime = 0.0f;
    private bool moveFlag = false;

    //デバッグ用***コメントアウトする

    //デバッグ用

    public enum PlayerState
    {
        Alive,
        DeadMove,
        ClearMove,
        Dead,
    }

    private PlayerState playerState;
    public PlayerState GetPlayerState { get => playerState; set => playerState = value; }



    // Start is called before the first frame update
    void Start()
    {
        option = GameObject.Find("Option");
        optionscript = option.GetComponent<Option>();
        Application.targetFrameRate = 60;
        playerState = PlayerState.Alive;
        playerHpGauge = GameObject.FindObjectOfType<PlayerHpGauge>();
        //playerEnergyGauge = GaugeUI.GetComponent<PlayerEnergyGauge>();
        playerEnergyGauge = GameObject.FindObjectOfType<PlayerEnergyGauge>();
        //cr = model.GetComponent<Renderer>().material.color;
        //cl = 255 - cr.r;
        transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 180);
        gaugeCount = 0;

        audioSource = GetComponent<AudioSource>();
        //Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;

        //ボスのバリア表示関係
        if(bossBarrierFlag)
        {
            bossBarrierCounter += 60 * Time.deltaTime;

            if(bossBarrierCounter>30)
            {
                bossBarrierCounter = 0;
                bossBarrierFlag = false;
            }
        }

        //timer++;

        //デバッグ用*******************************

        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);
        //Debug.Log(HP);
        //gauge = 100;

        //回復
        //if (Input.GetKey(KeyCode.H))
        //{
        //    float heal = 1f;
        //    HP += heal;
        //    playerHpGauge.Heal(heal / 100);
        //    if (HP >= 100) HP = 100;
        //}

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    HP -= 10;
        //    playerHpGauge.Damage(10);
        //}

        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    playerState = PlayerState.ClearMove;
        //}

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    //Instantiate(hanabiEffect);

        //    GameObject hanabis = Instantiate(hanabiEffect) as GameObject;

        //    hanabis.GetComponent<EffectLifeHanabi>().SetPosition(transform.position);



        //}

        //デバッグ用*******************************

        if (gauge >= 100) gauge = 100;
        if ( HP <= 0 )
        {
            playerState = PlayerState.DeadMove;
        }
        if (clearFlag) playerState = PlayerState.ClearMove;

        //クリア演出
        if(playerState == PlayerState.ClearMove)
        {
            //transform.position += new Vector3(0.075f, 0.0f, 0.0f);

            clearCount += Time.deltaTime * 1;
            transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 180);

            if (clearCount < 1.7f) return;

            deadMoveSpeed = Easing.SineInOut(easingCount, num, deadMoveSpeed, clearMaxSpeed);
            float back = 0.1f;
            //num = 200;
            //clearMaxSpeed = 0.8f;
            //deadMoveSpeed = Easing.BackInOut(easingCount, num, deadMoveSpeed, clearMaxSpeed,0.7f);
            transform.position += new Vector3(deadMoveSpeed - back,0, 0);
            easingCount = easingCount + 90 * Time.deltaTime;

          

            Vector3 screen_playerPos2 = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
            if (screen_playerPos2.x > Screen.width)
            {
                clearSceneFlag = true;
            }
            
            return;
        }

        //死んだ時の演出
        if(playerState == PlayerState.DeadMove)
        {
            if(!playerDeadEffectFlag)
            {
                StartCoroutine("DeadEffectStart");//死亡時の爆発エフェクト開始
                playerDeadEffectFlag = true;
            }

            if (!deadMoveFlag) return;//爆発エフェクト終わるまで待機

            //回転しながら落ちるならコメントアウト
            playerState = PlayerState.Dead;

            //以下****回転しながら落ちる処理***

            //float speed = 0.25f;
            //deadMoveSpeed = Easing.SineInOut(easingCount, num, deadMoveSpeed, maxSpeed);
            //transform.position += new Vector3(0, -deadMoveSpeed, 0);

            //angle += 630 * Time.deltaTime;
            //transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, angle);
            //easingCount = easingCount + 90 * Time.deltaTime;

            //Vector3 screen_playerPos2 = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
            //if(screen_playerPos2.y<0)
            //{
            //    playerState = PlayerState.Dead;
            //}

            return;
        }

        //プレイヤーの最初の行動

        //float startSpeed = 0.075f;
        float startSpeed = 4.875f;

        if(!startFlag)
        {
            transform.position += new Vector3( startSpeed,0,0)* Time.deltaTime;

            if(transform.position.x >= -6)
            {
                startFlag = true;
                playerEnergyGauge.SetStartUIflag(startFlag);
                playerHpGauge.SetStartUIflag(startFlag);
            }
        }

        if (!startFlag) return;

        //******以下通常　Update処理　　****************************************************

        CheckControlDevice();//操作デバイスチェック    
        
        //隕石に当たった時の処理
        //ノックバック処理
        if (nockBackFlag)
        {
            NockBack();
            //Gamenn();
        }
        else if(!nockBackFlag)
        {
            nockvelFlag = false;
        }

        if(insekiHitFlag)
        {
            insekicount += 1.5f*Time.deltaTime;
        }
        if(insekicount>=1.5f)
        {
            insekiHitFlag = false;
            insekicount = 0;
        }

        //キーボード操作処理
        if (keyboardFlag)
        {
            KeyBoardMove();
            transform.position += velocity * Time.deltaTime;
            velocity = Vector3.zero;
            //マウスの時
            //マウスの方向に弾を飛ばす
            Vector3 screen_point = Input.mousePosition;
            Vector3 screen_playerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
            //マウスの方向にむく
            var screenPos = Camera.main.WorldToScreenPoint(transform.position);
            var direction = Input.mousePosition - screenPos;
            var angle = GetAim(Vector3.zero, direction);
            transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, angle - 180);


            //マウスの右クリックで弾発射
            if (Input.GetMouseButtonDown(0))
            {
                bulletcounter = 0;
                GameObject bullets = Instantiate(bullet) as GameObject;
                Vector3 vel = screen_point - screen_playerPos;
                vel.z = 0;
                bullets.GetComponent<BulletControl>().SetTransform(vel, this.transform.position);
                bullets.GetComponent<BulletControl>().SetRotation(
                    new Vector3(transform.rotation.x,transform.rotation.y,angle-180));
                //bullets.transform.parent = bulletbox.transform;
                //音
                PlayerBulletSE();

            }
            //マウス長押しで弾発射
            if (Input.GetMouseButton(0))
            {
                bulletcounter += Time.deltaTime * 1;
                if (bulletcounter > 0.22f)
                {
                    GameObject bullets = Instantiate(bullet) as GameObject;
                    Vector3 vel = screen_point - screen_playerPos;
                    vel.z = 0;
                    bullets.GetComponent<BulletControl>().SetTransform(vel, this.transform.position);
                    bullets.GetComponent<BulletControl>().SetRotation(
                        new Vector3(transform.rotation.x, transform.rotation.y, angle - 180));
                    //bullets.transform.parent = bulletbox.transform;
                    //音
                    bulletcounter = 0;
                    PlayerBulletSE();
                }
               
            }
            //ゲージ技
            if (Input.GetKeyDown(KeyCode.E) && gauge >= 40)
            {
                GaugeBullet();
            }
            if (Input.GetKeyDown(KeyCode.Q) && gauge >= 40)
            {
                GaugeKyoukaBullet();
            }
        }
        else//パッド操作
        {

            PadMove();//移動、回転、画面外に行かない処理

            //RBを押したら弾を撃つ
            if (Input.GetKeyDown("joystick button 5"))
            {
                bulletcounter = 0;
                // 弾丸の複製
                GameObject bullets = Instantiate(bullet) as GameObject;     
                bullets.GetComponent<BulletControl>().SetTransform(poolvelocity, this.transform.position);
                bullets.GetComponent<BulletControl>().SetRotation(
                  new Vector3(transform.rotation.x, transform.rotation.y, bulletangle ));
               
                PlayerBulletSE();
            }
            //長押しで弾を撃つ
            if (Input.GetKey("joystick button 5"))
            {
                bulletcounter += Time.deltaTime * 1;

                if(bulletcounter > 0.22f)
                {
                    // 弾丸の複製
                    GameObject bullets = Instantiate(bullet) as GameObject;
                    bullets.GetComponent<BulletControl>().SetTransform(poolvelocity, this.transform.position);
                    bullets.GetComponent<BulletControl>().SetRotation(
                      new Vector3(transform.rotation.x, transform.rotation.y, bulletangle));
                    bulletcounter = 0;
                    PlayerBulletSE();
                }            
            }
            //ゲージ技
            if (Input.GetKeyDown("joystick button 1") && gauge >= 40)
            {
                GaugeBullet();
            }
            if (Input.GetKeyDown("joystick button 0") && gauge >= 40)
            {
               
                GaugeKyoukaBullet();
            }

            padRvelocity2 = padRvelocity;//velocityを保存

        }

        if(!startOneFlag)
        {
            transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 180);
            startOneFlag = true;
        }

    }

    public void PlayerBulletSE()
    {
        audioSource.volume = optionscript.GetSEVolume();
        audioSource.PlayOneShot(playerBulletSE);
    }

    public void GaugeBullet()
    {
        Debug.Log("敵をひっくり返す技");
        gauge = gauge - 40;
        GameObject gmobj = Instantiate(gaugebullet) as GameObject;
        gmobj.GetComponent<PlayerGaugeBulletControl>().SetPosition(this.transform.position);
        gmobj.GetComponent<PlayerGaugeBulletControl>().SetGaugeFlag(true);
        playerEnergyGauge.SetGauge(40f);
        gaugeCount++;
        //音
        audioSource.volume = optionscript.GetSEVolume();
        audioSource.PlayOneShot(playerGaugeShootHantenSE);
    }
    public void GaugeKyoukaBullet()
    {
        Debug.Log("味方を強化する技");
        gauge = gauge - 40;

        GameObject gmobj = Instantiate(kyoukabullet) as GameObject;
        gmobj.GetComponent<PlayerGaugeBulletControl>().SetPosition(this.transform.position);
        gmobj.GetComponent<PlayerGaugeBulletControl>().SetGaugeFlag(true);
        playerEnergyGauge.SetGauge(40f);
        gaugeCount++;
        //音
        audioSource.volume = optionscript.GetSEVolume();
        audioSource.PlayOneShot(playerGaugeShootKyoukaSE);
    }

    //隕石ノックバック処理
    public void NockBack()
    {
        if (nockBackCount > 0.2f)
        {
            nockBackFlag = false;
            nockBackCount = 0;
        }
        
        Vector3 screen_playerPos2 = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
        //nockbackVel.y = nockbackVel.y * -1;

        bool upFlag = false;
        bool upFlag2 = false;

        bool downFlag = false;

        if(!nockvelFlag)
        {
            if (screen_playerPos2.y > Screen.height - 50)
            {
                //nockbackVel.y = 0;
                upFlag2 = true;
            }
            else if (screen_playerPos2.y < 50)
            {
                //nockbackVel.y = 0;
                upFlag = true;
                
            }
            if (screen_playerPos2.x > Screen.width - 50)
            {
                nockbackVel.x = 0;
                downFlag = true;
            }
            else if (screen_playerPos2.x < nomove)
            {
                nockbackVel.x = 0;
                downFlag = true;
            }
        }

        if(upFlag && downFlag)
        {
            if(nockbackVel.y< 0 && upFlag)
            {
                if (nockbackVel.y < 0.2)
                {
                    nockbackVel = new Vector3(nockbackVel.x, nockbackVel.y * 2.2f, nockbackVel.z);
                }
                nockbackVel.y = nockbackVel.y * -1;
                
            }
            
            
            transform.position += nockbackVel * Time.deltaTime * 30f;
        }
        else if (upFlag2 && downFlag)
        {
            if (nockbackVel.y < 0.2)
            {
                nockbackVel = new Vector3(nockbackVel.x, nockbackVel.y * 2.2f, nockbackVel.z);
            }
            if (nockbackVel.y > 0 && upFlag2)
            {
                nockbackVel.y = nockbackVel.y * -1;
            }


            transform.position += nockbackVel * Time.deltaTime * 30f;
        }
        else
        {
            if (screen_playerPos2.y > Screen.height - 50)
            {
                nockbackVel.y = 0;
            }
            if (screen_playerPos2.y < 50)
            {
                nockbackVel.y = 0;
            }
            if (screen_playerPos2.x > Screen.width - 50)
            {
                nockbackVel.x = 0;
            }
            if (screen_playerPos2.x < nomove)
            {
                nockbackVel.x = 0;
            }
        }

        transform.position += nockbackVel * Time.deltaTime * 30f;    

        nockBackCount += 1 * Time.deltaTime;
        nockvelFlag = true;
    }

    //コントローラーチェック
    public void CheckControlDevice()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
          || Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            keyboardFlag = true;
        }
        if (Input.GetAxis("L_Horizontal") == 0 && Input.GetAxis("L_Vertical") == 0)
        {

        }
        else
        {
            keyboardFlag = false;//パッド操作
        }
    }

    //キーボード操作(移動)
    public void KeyBoardMove()
    {
        if (nockBackFlag) return;

        Vector3 screen_playerPos2 = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);

        if (Input.GetKey(KeyCode.W) && screen_playerPos2.y < Screen.height -50)
        {
            velocity += new Vector3(0, speed, 0);
        }
        if (Input.GetKey(KeyCode.S) && screen_playerPos2.y >50)
        {
            velocity += new Vector3(0, -speed, 0);
        }
        if (Input.GetKey(KeyCode.D) && screen_playerPos2.x < Screen.width - 50)
        {
            velocity += new Vector3(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A)&& screen_playerPos2.x > nomove)
        {
            velocity += new Vector3(-speed, 0, 0);
        }
        velocity.Normalize();
        velocity *= speed;
    }

    //パッド操作(移動＆画面外処理)
    public void PadMove()
    {
        if (nockBackFlag) return;
        Vector3 screen_playerPos2 = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);

        padvelocity.x = Input.GetAxis("L_Horizontal");
        padvelocity.y = Input.GetAxis("L_Vertical") * -1;
        padRvelocity.x = Input.GetAxis("R_Horizontal");
        padRvelocity.y = Input.GetAxis("R_Vertical") * -1;

        //前の向き方変更があったら通る
        if (padRvelocity != padRvelocity2)
        {
            if (padRvelocity.x == 0 && padRvelocity.y == 0)
            {
                //bulletangle = 180;
                //poolvelocity = new Vector2(1, 0);
                //transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 180);
            }
            else
            {
                //プレイヤーの向きを決める
                poolvelocity = padRvelocity;
                float h = Input.GetAxis("R_Horizontal");
                float v = Input.GetAxis("R_Vertical");

                //Pad動き参考
                //// スティックが倒れていれば、倒れている方向を向く
                if (h != 0 || v != 0)
                {
                    //    var direction = new Vector3(h, 0, v);
                    //    transform.localRotation = Quaternion.LookRotation(direction);

                    float radian = Mathf.Atan2(v, -h) * Mathf.Rad2Deg;
                    if (radian < 0)
                    {
                        radian += 360;
                    }
                    bulletangle = radian;
                    transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, radian);

                    //moveFlag = true;
                }



                if (moveFlag)
                {
                    movetime += Time.deltaTime * 60;

                    if(movetime > 30)
                    {
                        moveFlag = false;
                    }
                }



                //Debug.Log(new Vector2(h,v));

                
            }
        }
        

        //画面外処理
        if (screen_playerPos2.y > Screen.height - 50 && Input.GetAxis("L_Vertical") * -1 > 0)
        {
            padvelocity.y = 0;
        }
        if (screen_playerPos2.y < 0 + 50 && Input.GetAxis("L_Vertical") * -1 < 0)
        {
            padvelocity.y = 0;
        }
        if (screen_playerPos2.x < 0 + nomove && Input.GetAxis("L_Horizontal") < 0)
        {
            padvelocity.x = 0;
        }
        if (screen_playerPos2.x > Screen.width - 50 && Input.GetAxis("L_Horizontal") > 0)
        {
            padvelocity.x = 0;
        }

        float padspeed = speed / 60;
        transform.position += padvelocity * padspeed;
    }

    //隕石ダメージ
    public void RockDamage(float dame)
    {
        if (mutekiFlag) return;

        HP -= (int)dame;
        playerHpGauge.Damage(dame);
        MutekiFlagActive();
    }

    //HP回復
    public void PlayerHeal()
    {
        float heal = 0.05f;
        HP += heal;
        playerHpGauge.Heal(heal / 100);
        if (HP >= 100) HP = 100;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "FriendHeal")
        {
            PlayerHeal();
        }
    }

    public void GaugeUp()
    {
        gauge += upenergy;
        playerEnergyGauge.UpGauge((float)upenergy);
        //音
        audioSource.volume = optionscript.GetSEVolume();
        audioSource.PlayOneShot(playerEnergyUpSE);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerState != PlayerState.Alive ) return;//生きてなかったら以下処理しない

        //if (other.gameObject.tag == "GaugeEnergy")
        //{
        //    gauge += upenergy;
        //    playerEnergyGauge.UpGauge((float)upenergy);
        //    GameObject effect = Instantiate(gaugeEffect);
        //    effect.transform.position = transform.position;
        //    effect.transform.localScale = new Vector3(2, 2, 2);
        //    effect.GetComponent<PlayerBurstScript>().SetModeFlag(true);
        //    //音
        //    audioSource.PlayOneShot(playerEnergyUpSE);
        //}

        if (other.gameObject.tag == "Rock")
        {
            if (insekiHitFlag) return;
            insekiHitFlag = true;
            RockDamage(damage);
            audioSource.volume = optionscript.GetSEVolume();
            //音
            audioSource.PlayOneShot(dameageSE);
            //当たったRockを探す
            SerchObject();

            nockbackVel = (transform.position - other.transform.position).normalized;

            nockBackFlag = true;      
        }

        if (other.gameObject.tag == "Enemy")
        {
            GameObject burst2 = Instantiate(playerBurst);
            burst2.transform.position = other.transform.position;
            burst2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            DamageSE();
            if (!mutekiFlag)
            {
                HP -= (int)damage;
                playerHpGauge.Damage(damage);
                MutekiFlagActive();
                
            }
           
        }

        if (other.gameObject.tag == "EnemyBullet")
        {

            if (playerState == PlayerState.DeadMove) return;
            GameObject burst = Instantiate(Resources.Load<GameObject>("Explosion"));
            burst.transform.position = transform.position;
            burst.GetComponent<EffectScript>().HitSE();

            if(!mutekiFlag)
            {
                HP -= (int)damage;
                playerHpGauge.Damage(damage);
                MutekiFlagActive();
                //音
                DamageSE();
            }
           
        }

        if (mutekiFlag) return;//無敵なら以下処理しない
        
       
       
        if (other.gameObject.tag == "Boss")
        {
            HP -= (int)damage;
            playerHpGauge.Damage(damage);
            MutekiFlagActive();
            //音
            DamageSE();
        }
        
        
    }

    public float GetAim(Vector2 from, Vector2 to)
    {
        float dx = to.x - from.x;
        float dy = to.y - from.y;
        float rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }

    public bool GetisDead()
    {
        if(playerState == PlayerState.Dead)
        {
            return true;
        }
        return false;
    }

    public int GetGauge()
    {
        return gauge;
    }

    public void MutekiFlagActive()
    {
        mutekiFlag = true;
        StartCoroutine("WaitSpriteAlpha");//点滅処理開始
    }

    public static int GetGaugeCount()
    {
        return gaugeCount;
    }

    public bool GetClearSceneFlag()
    {
        return clearSceneFlag;
    }

    IEnumerator DeadEffectStart()
    {

        Vector3 effectScale = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 effectScale2 = new Vector3(1.2f,1.2f,1.2f);

        GameObject burst33 = Instantiate(playerBurst);
        burst33.transform.position = transform.position;//+ new Vector3(0.5f, 0.7f, -0.6f);
        burst33.transform.localScale = new Vector3(3f, 3f, 3f);
        DamageSE();

        yield return new WaitForSeconds(0.7f);

        GameObject burst2 = Instantiate(playerBurst);
        burst2.transform.position = transform.position + new Vector3(0.5f,0.7f,-0.6f);
        burst2.transform.localScale = new Vector3(1f, 1f, 1f);
        DamageSE();
        yield return new WaitForSeconds(0.6f);

        GameObject burst3 = Instantiate(playerBurst);
        burst3.transform.position = transform.position + new Vector3(-0.7f, -0.7f, -0.6f);
        burst3.transform.localScale = new Vector3(1f, 1f, 1f);
        DamageSE();

        yield return new WaitForSeconds(0.6f);

        GameObject burst = Instantiate(playerBurst);
        burst.transform.position = transform.position + new Vector3(0.3f, -0.3f, -0.6f);
        burst.transform.localScale = new Vector3(1f, 1f, 1f);
      

        DamageSE();

        yield return new WaitForSeconds(0.6f);

        model.SetActive(false);

        GameObject burst4 = Instantiate(playerBurst);
        burst4.transform.position = transform.position;
        burst4.transform.localScale = new Vector3(2f, 2f, 2f);

        DamageSE();

        

        yield return new WaitForSeconds(0.9f);
        model.SetActive(false);
        deadMoveFlag = true;
    }

    public void DamageSE()
    {
        audioSource.volume = optionscript.GetSEVolume();
        audioSource.PlayOneShot(dameageSE);
    }

    IEnumerator WaitSpriteAlpha()
    {
        //点滅処理
        if (tenmetuFlag)
        {
            yield break;
        }
        tenmetuFlag = true;

        for (int i = 0; i < 8; i++)
        {
            model.SetActive(false);
            yield return new WaitForSeconds(0.15f);

            model.SetActive(true);
            yield return new WaitForSeconds(0.15f);
            if(playerState == PlayerState.DeadMove)
            {
                yield break;
            }
        }

        yield return new WaitForSeconds(0.15f);
        tenmetuFlag = false;
        mutekiFlag = false;
    }

    public void  SerchObject()
    {
        float distance = 0;
        float ansDist = 0;

        GameObject[] obj = GameObject.FindGameObjectsWithTag("Rock");

        for(int i = 0; i<obj.Length; i++)
        {
            distance = Vector3.Distance(obj[i].transform.position, this.transform.position);
            if (ansDist > distance || distance == 0)
            {
                ansDist = distance;
                rockPos = obj[i].transform.position;
            }
        }
       
    }

    public void HanabiEffectSet()
    {
        GameObject hanabis = Instantiate(hanabiEffect) as GameObject;

        hanabis.GetComponent<EffectLifeHanabi>().SetPosition(transform.position);
    }
    public void LaserDamage()
    {
        if (mutekiFlag) return;

        HanabiEffectSet();

        float damage2 = 1;
        HP -= (int)damage2;
        audioSource.volume = optionscript.GetSEVolume() ;
        playerHpGauge.Damage(damage2);
        LaserCnt++;
        if (LaserCnt == damagelimit)
        {
            LaserCnt = 0;
            MutekiFlagActive();
           
        }

        //音
        audioSource.volume = optionscript.GetSEVolume() * 0.2f;
        audioSource.PlayOneShot(dameageSE);
        audioSource.volume = optionscript.GetSEVolume();

    }
    public void AttackDamage()
    {
        if (mutekiFlag) return;
        float damage2 = 20;
        HP -= (int)damage2;
        playerHpGauge.Damage(damage2);
        //音
        audioSource.volume = optionscript.GetSEVolume();
        audioSource.PlayOneShot(dameageSE);
        nockBackFlag = true;
        //Debug.Log("うせやろ");
    }

    public void SetClearFlag(bool fl)
    {
        clearFlag = fl;
    }

    public void SetBossBarrierFlag(bool fl)
    {
        bossBarrierFlag = fl;
    }

    public bool GetBossBarrierFlag()
    {
        return bossBarrierFlag;
    }

    public void Test()
    {
        if(true)
        {
            //Debug.Log("味方を強化する技");
            //gauge = gauge - 40;

            //GameObject gmobj = Instantiate(kyoukabullet) as GameObject;
            //gmobj.GetComponent<PlayerGaugeBulletControl>().SetPosition(this.transform.position);
            //gmobj.GetComponent<PlayerGaugeBulletControl>().SetGaugeFlag(true);
            //playerEnergyGauge.SetGauge(40f);
            //gaugeCount++;
            ////音
            //audioSource.volume = optionscript.GetSEVolume();
            //audioSource.PlayOneShot(playerGaugeShootKyoukaSE);
        }

        if(true)
        {
            //Debug.Log("敵をひっくり返す技");
            //gauge = gauge - 40;

            //GameObject gmobj = Instantiate(gaugebullet) as GameObject;
            //gmobj.GetComponent<PlayerGaugeBulletControl>().SetPosition(this.transform.position);
            //gmobj.GetComponent<PlayerGaugeBulletControl>().SetGaugeFlag(true);
            //playerEnergyGauge.SetGauge(40f);
            //gaugeCount++;
            ////音
            //audioSource.volume = optionscript.GetSEVolume();
            //audioSource.PlayOneShot(playerGaugeShootHantenSE);
        }
    }
}
