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

    private PlayerHpGauge playerHpGauge;

    private Vector3 velocity;  
    private int gauge = 0;

    private int timer = 0;//計測用

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
    }
    // Update is called once per frame
    void Update()
    {
        timer++;
        //デバッグ用*******************************

        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);
        //Debug.Log(HP);
        //デバッグ用*******************************
        if (gauge >= 100) gauge = 100;
        if ( HP <= 0 )
        {
            playerState = PlayerState.Dead;
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

        }
        if(Input.GetKeyDown(KeyCode.E) && gauge>=40)
        {
            Debug.Log("敵をひっくり返す技");
            gauge = gauge - 40;
            GameObject bullets = Instantiate(bullet) as GameObject;
            bullets.GetComponent<BulletControl>().SetPosition(this.transform.position);
            bullets.GetComponent<BulletControl>().SetGaugeFlag(true);
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
        if (playerState != PlayerState.Alive) return;

        float damage = 5f;
        if (other.gameObject.tag == "Enemy")
        {
            HP -= (int)damage;
            playerHpGauge.Damage(damage);
            //Debug.Log("エネミーと当たった");
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            HP -= (int)damage;
            playerHpGauge.Damage(damage);
            //Debug.Log("エネミーの弾と当たった");
        }
        if (other.gameObject.tag == "Boss")
        {
            HP -= (int)damage;
            playerHpGauge.Damage(damage);
            //Debug.Log("エネミーの弾と当たった");
        }
        if (other.gameObject.tag == "GaugeEnergy")
        {
            gauge += 20;
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
}
