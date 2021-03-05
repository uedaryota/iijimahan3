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
        //デバッグ用*******************************
        gauge++;
        if (gauge > 100) gauge = 100;//Debug.Log(gauge);
        //デバッグ用*******************************
        if( HP <= 0 )
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("敵をひっくり返す技");
            gauge -= 40;
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
}
