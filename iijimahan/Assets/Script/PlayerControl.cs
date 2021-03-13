using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField, Header("プレイヤーの初期HP")]
    public int HP = 100;
    [SerializeField, Header("プレイヤーの移動速度")]
    public float speed = 10f;
    [SerializeField, Header("プレイヤーのが撃つ弾")]
    public GameObject bullet;
    [SerializeField, Header("プレイヤーのゲージ技の弾")]
    public GameObject gaugebullet;
    [SerializeField, Header("プレイヤーのモデル")]
    public GameObject model;
    [SerializeField, Header("点滅周期")]
    public float tenmetuInterval = 1.0f;


    private PlayerHpGauge playerHpGauge;

    private PlayerEnergyGauge playerEnergyGauge;

    private GameObject GaugeUI;

    private Vector3 velocity;  
    private int gauge = 0;

    private int timer = 0;//計測用

    private bool mutekiFlag = false;
    private bool tenmetuFlag = false;

    private Vector3 padvelocity;
    private Vector3 padRvelocity;
    private Vector3 padRvelocity2;
    private Vector3 poolvelocity = new Vector3(1, 0, 0);

    private bool keyboardFlag = true;

    private static int gaugeCount = 0;


    //private Color cr;
    //private float cl;

    //デバッグ用***コメントアウトする

    //デバッグ用

    public enum PlayerState
    {
        Alive,
        Dead,
    }

    private PlayerState playerState;
    public PlayerState GetPlayerState { get => playerState; set => playerState = value; }



    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        playerState = PlayerState.Alive;
        playerHpGauge = GameObject.FindObjectOfType<PlayerHpGauge>();
        //playerEnergyGauge = GaugeUI.GetComponent<PlayerEnergyGauge>();
        playerEnergyGauge = GameObject.FindObjectOfType<PlayerEnergyGauge>();
        //cr = model.GetComponent<Renderer>().material.color;
        //cl = 255 - cr.r;
        transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 180);
    }
    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;

        //timer++;
        //デバッグ用*******************************

        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);
        //Debug.Log(HP);
        //gauge = 100;

        //デバッグ用*******************************
        if (gauge >= 100) gauge = 100;
        if ( HP <= 0 )
        {
            playerState = PlayerState.Dead;
        }

        CheckControlDevice();//操作デバイスチェック       

        if(keyboardFlag)//キーボード操作
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
                GameObject bullets = Instantiate(bullet) as GameObject;
                Vector3 vel = screen_point - screen_playerPos;
                vel.z = 0;
                bullets.GetComponent<BulletControl>().SetTransform(vel, this.transform.position);
                Debug.Log("撃つ");

            }
            if (Input.GetKeyDown(KeyCode.E) && gauge >= 40)
            {
                Debug.Log("敵をひっくり返す技");
                gauge = gauge - 40;

                GameObject gmobj = Instantiate(gaugebullet) as GameObject;
                gmobj.GetComponent<PlayerGaugeBulletControl>().SetPosition(this.transform.position);
                gmobj.GetComponent<PlayerGaugeBulletControl>().SetGaugeFlag(true);
                playerEnergyGauge.SetGauge(40f);
                gaugeCount++;
            }
            if (Input.GetKeyDown(KeyCode.Q) && gauge >= 40)
            {
                Debug.Log("味方を強化する技");
                //gauge = gauge - 40;
            }
        }
        else//パッド操作
        {

            PadMove();//移動、回転、画面外に行かない処理

            if (Input.GetKeyDown("joystick button 5"))
            {
                // 弾丸の複製
                GameObject bullets = Instantiate(bullet) as GameObject;     
                bullets.GetComponent<BulletControl>().SetTransform(poolvelocity, this.transform.position);
            }
            if(Input.GetKeyDown("joystick button 1") && gauge >= 40)
            {
                Debug.Log("敵をひっくり返す技");
                gauge = gauge - 40;
                GameObject gmobj = Instantiate(gaugebullet) as GameObject;
                gmobj.GetComponent<PlayerGaugeBulletControl>().SetPosition(this.transform.position);
                gmobj.GetComponent<PlayerGaugeBulletControl>().SetGaugeFlag(true);
                playerEnergyGauge.SetGauge(40f);
                gaugeCount++;
            }

            padRvelocity2 = padRvelocity;

        }

       

    }
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
    public void KeyBoardMove()
    {
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
        if (Input.GetKey(KeyCode.A)&& screen_playerPos2.x > 50)
        {
            velocity += new Vector3(-speed, 0, 0);
        }
        velocity.Normalize();
        velocity *= speed;
    }

    public void PadMove()
    {
        Vector3 screen_playerPos2 = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);

        padvelocity.x = Input.GetAxis("L_Horizontal");
        padvelocity.y = Input.GetAxis("L_Vertical") * -1;
        padRvelocity.x = Input.GetAxis("R_Horizontal");
        padRvelocity.y = Input.GetAxis("R_Vertical") * -1;

        if (padRvelocity != padRvelocity2)
        {
            if (padRvelocity.x == 0 && padRvelocity.y == 0)
            {
            }
            else
            {
                poolvelocity = padRvelocity;
                var h = Input.GetAxis("R_Horizontal");
                var v = Input.GetAxis("R_Vertical");
                float radian = Mathf.Atan2(v, -h) * Mathf.Rad2Deg;
                if (radian < 0)
                {
                    radian += 360;
                }
                transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, radian);
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
        if (screen_playerPos2.x < 0 + 50 && Input.GetAxis("L_Horizontal") < 0)
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

  
    private void OnTriggerEnter(Collider other)
    {
        if (playerState != PlayerState.Alive ) return;

        if (other.gameObject.tag == "GaugeEnergy")
        {
            gauge += 20;
            //GaugeUI.GetComponent<PlayerEnergyGauge>().UpGauge(20f);
            playerEnergyGauge.UpGauge(20f);

        }

        if (mutekiFlag) return;//無敵なら以下処理しない

        float damage = 5f;
        if (other.gameObject.tag == "Enemy")
        {
            HP -= (int)damage;
            playerHpGauge.Damage(damage);
            MutekiFlagActive();
            //Debug.Log("エネミーと当たった");
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            HP -= (int)damage;
            playerHpGauge.Damage(damage);
            MutekiFlagActive();
            //Debug.Log("エネミーの弾と当たった");
        }
        if (other.gameObject.tag == "Boss")
        {
            HP -= (int)damage;
            playerHpGauge.Damage(damage);
            MutekiFlagActive();
            //Debug.Log("エネミーの弾と当たった");
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

    IEnumerator WaitSpriteAlpha()
    {
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
        }

        yield return new WaitForSeconds(0.15f);
        tenmetuFlag = false;
        mutekiFlag = false;
    }
}
