using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject bullet;
    private Vector3 velocity;
    public float speed =10f;
    public int HP;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Move();
        transform.position += velocity * Time.deltaTime;
        velocity = Vector3.zero;
        Vector3 screen_point = Input.mousePosition;
        Vector3 screen_playerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
        //Debug.Log(screen_point);
        if (Input.GetMouseButtonDown(0))
        {
            // 弾丸の複製
            GameObject bullets = Instantiate(bullet) as GameObject;
            // クリックした座標の取得（スクリーン座標からワールド座標に変換）
            // Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 向きの生成（Z成分の除去と正規化）
            //Vector3 vel = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;
            Vector3 a = screen_point - screen_playerPos;
            a.z = 0;
            bullets.GetComponent<BulletControl>().SetTransform(a, this.transform.position);
        }
        // z キーが押された時
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // 弾丸の複製
            GameObject bullets = Instantiate(bullet) as GameObject;
            // クリックした座標の取得（スクリーン座標からワールド座標に変換）
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 向きの生成（Z成分の除去と正規化）
            Vector3 vel = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;
            //Vector3 vel;
            //vel = new Vector3(0.2f, 0, 0);
            bullets.GetComponent<BulletControl>().SetTransform(vel, this.transform.position);
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
