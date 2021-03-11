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

    private PlayerHpGauge playerHpGauge;

    private PlayerEnergyGauge playerEnergyGauge;

    private GameObject GaugeUI;

    private Vector3 velocity;  
    private int gauge = 0;

    private int timer = 0;//計測用

    private bool mutekiFlag = false;
    private float mutekiCounter = 0;
    private bool tenmetuFlag = false;

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
        playerState = PlayerState.Alive;
        playerHpGauge = GameObject.FindObjectOfType<PlayerHpGauge>();
        //playerEnergyGauge = GaugeUI.GetComponent<PlayerEnergyGauge>();
        playerEnergyGauge = GameObject.FindObjectOfType<PlayerEnergyGauge>();
        //cr = model.GetComponent<Renderer>().material.color;
        //cl = 255 - cr.r;
    }
    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;

        timer++;
        //デバッグ用*******************************

        //Debug.Log(mutekiCounter);
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
        if (mutekiFlag)
        {
            mutekiCounter += 60f * Time.deltaTime;
        }
        //if ((int)mutekiCounter % 40 == 0 && mutekiFlag)
        //{

        //    if (!tenmetuFlag)
        //    {
        //        tenmetuFlag = !tenmetuFlag;
        //        model.SetActive(false);Debug.Log("false;");
        //    }
        //    else
        //    {
        //        tenmetuFlag = !tenmetuFlag;
        //        model.SetActive(true); Debug.Log("true;");
        //    }

        //}
        if (mutekiCounter > 120)
        {
            mutekiFlag = false;
            //model.SetActive(true);
        }

        Move();
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
        transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, angle - 180 );


        //マウスの右クリックで弾発射
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullets = Instantiate(bullet) as GameObject;
            Vector3 vel = screen_point - screen_playerPos;
            vel.z = 0;
            bullets.GetComponent<BulletControl>().SetTransform(vel, this.transform.position);
            Debug.Log("撃つ");
            //playerEnergyGauge.UpGauge(20f);
            //gauge += 20;

        }
        if(Input.GetKeyDown(KeyCode.E) && gauge>=40)
        {
            Debug.Log("敵をひっくり返す技");
            gauge = gauge - 40;
            //GameObject bullets = Instantiate(bullet) as GameObject;
            //bullets.GetComponent<BulletControl>().SetPosition(this.transform.position);
            //bullets.GetComponent<BulletControl>().SetGaugeFlag(true);
            //GaugeUI.GetComponent<PlayerEnergyGauge>().SetGauge(40f);

            GameObject gmobj = Instantiate(gaugebullet) as GameObject;
            gmobj.GetComponent<PlayerGaugeBulletControl>().SetPosition(this.transform.position);
            gmobj.GetComponent<PlayerGaugeBulletControl>().SetGaugeFlag(true);
            playerEnergyGauge.SetGauge(40f);
        }
        if (Input.GetKeyDown(KeyCode.Q) && gauge >= 40)
        {
            Debug.Log("味方を強化する技");
            gauge = gauge - 40;
            
        }

    }
    public void Move()
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

  
    private void OnTriggerEnter(Collider other)
    {
        if (playerState != PlayerState.Alive ) return;

        if (other.gameObject.tag == "GaugeEnergy")
        {
            gauge += 20;
            //GaugeUI.GetComponent<PlayerEnergyGauge>().UpGauge(20f);
            playerEnergyGauge.UpGauge(20f);

        }

        if (mutekiFlag) return;

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
        mutekiCounter = 0;
    }
}
