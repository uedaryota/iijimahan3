using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject bullet;
    private Vector3 velocity;
    public float speed = 10f;
    public int HP = 100;
    public int gauge = 0;
    private int timer = 0;//計測用
    //デバッグ用***コメントアウトする
    //public GameObject energy;
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
    }
    // Update is called once per frame
    void Update()
    {
        timer++;
        //デバッグ用*******************************

        //Debug.Log(Screen.width);
        //Debug.Log(Screen.height);

        //if (Input.GetKeyDown(KeyCode.G))
        //{         
        //    GameObject bullets = Instantiate(energy) as GameObject;
        //}

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

        Vector3 screen_point = Input.mousePosition;
        Vector3 screen_playerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
        
        //マウスの右クリックで弾発射
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bullets = Instantiate(bullet) as GameObject;
            Vector3 vel = screen_point - screen_playerPos;
            vel.z = 0;
            bullets.GetComponent<BulletControl>().SetTransform(vel, this.transform.position);
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
        if (Input.GetKey(KeyCode.W))
        {
            velocity += new Vector3(0, speed, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity += new Vector3(0, -speed, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity += new Vector3(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            velocity += new Vector3(-speed, 0, 0);
        }
        velocity.Normalize();
        velocity *= speed;
    }

  
    private void OnTriggerEnter(Collider other)
    {
        if (playerState != PlayerState.Alive) return;

        if (other.gameObject.tag == "Enemy")
        {
            HP -= 10;
            //Debug.Log("エネミーと当たった");
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            HP -= 10;
            //Debug.Log("エネミーの弾と当たった");
        }
        if (other.gameObject.tag == "GaugeEnergy")
        {
            gauge += 20;
        }  
    }
}
