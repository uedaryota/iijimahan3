using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField, Header("速度")]private float speed = 1;
    [SerializeField, Header("消えるまでの時間")]private float deletetime = 20;
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
        Debug.Log(PlayerPos);
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        if(timer >= deletetime)
        {
            Destroy(this.gameObject);
        }
    }

    
}
