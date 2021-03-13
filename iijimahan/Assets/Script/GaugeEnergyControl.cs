﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeEnergyControl : MonoBehaviour
{
    private float easingCount = 0;
    //private bool easingFlag = false;
    public int num = 80;
    public float maxSpeed = 30;
    private float speed = 0.0f;
    private Vector3 playerPos;
    private Vector3 velocity;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;

        playerPos = player.transform.position;
        speed = Easing.SineInOut(easingCount, num, speed, maxSpeed);
        velocity = Move();
        transform.position += velocity * speed * Time.deltaTime * 2;

        easingCount = easingCount + 90 *Time.deltaTime;
    }

    public Vector3 Move()
    {
        float x = this.transform.position.x - playerPos.x;
        float y = this.transform.position.y - playerPos.y;

        Vector3 v = new Vector3(-x, -y, 0);
        v.Normalize();

        return v;
    }
    public void SetPosition(Vector3 pos)
    {
        this.transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("playerに当たった");
            Destroy(this.gameObject);
        }       
    }

   
}
