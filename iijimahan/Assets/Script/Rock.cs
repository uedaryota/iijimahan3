using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField, Header("速度")]private float speed = 1;
    [SerializeField, Header("消えるまでの時間")]private float deletetime = 20;
    public int HP = 20;
    private Vector3 velocity;
    private Object player;
    private Vector3 PlayerPos;
    private float timer;

    void Start()
    {
        timer = 0;
        PlayerPos = GameObject.Find("Player").transform.position;
        velocity = PlayerPos - transform.position;
        velocity *= 0.001f * speed;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        if(timer >= deletetime || HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            HP--;
        }
    }
}
