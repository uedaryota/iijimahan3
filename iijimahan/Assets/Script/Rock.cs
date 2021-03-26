using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField, Header("速度")]private float speed = 1;
    private Vector3 velocity;
    private Object player;
    private Vector3 PlayerPos;

    void Start()
    {
        PlayerPos = GameObject.Find("Player").transform.position;
        velocity = PlayerPos - transform.position;
        velocity *= 0.001f * speed;
        Debug.Log(PlayerPos);
    }
    
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }

    
}
