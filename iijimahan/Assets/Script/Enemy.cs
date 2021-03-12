﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    private GameObject target;
    public int moveTime = 30;
    public int chargeTime = 30;
    private int currentMove;
    private int currentCharge;
    private Vector3 velocity;
    private bool deadFlag = false;
    public GameObject energy;
    public int MaxrotateTime = 60;
    int rotateTime = 0;
    Transform lastTransform;
    // Start is called before the first frame update
    void Start()
    {
        deadFlag = false;
        currentCharge = 0;
        currentMove = 0;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckTarget();
        ObjectRotate();
        ChangeRotate();
        if (target!=null)
        {
            CheckDead();
        }
     
        Move();
    }
    void Move()
    {
        currentCharge++;
        if (currentCharge > chargeTime)
        {

            transform.position += velocity * Time.deltaTime;
            currentMove++;
            if (currentMove > moveTime)
            {
                currentMove = 0;
                currentCharge = 0;
            }
        }
        else
        {
            if (target != null)
            {
                velocity = target.transform.position - transform.position;
                velocity = Vector3.Normalize(velocity) * 5;
            }
        }
    }
    void CheckTarget()
    {
        if (this.gameObject.tag == "Enemy")
        {
            if (target == null)
            {
                // if (target.tag != "Player")
                {
                    target = GameObject.FindGameObjectWithTag("Player");
                }
            }
            else if (target.tag != "Player" && target.tag != "Friend")
            {
                target = GameObject.FindGameObjectWithTag("Player");
            }
        }
        if (this.gameObject.tag == "Friend")
        {
            if (target == null)
            {
                //   if (target.tag != "Enemy")
                {
                    target = GameObject.FindGameObjectWithTag("Player");
                }
            }
            else if (target.tag != "Enemy" && target.tag != "Boss")
            {
                target = GameObject.FindGameObjectWithTag("Boss");
                if (target == null)
                {
                    //Destroy(this.gameObject);
                    target = GameObject.FindGameObjectWithTag("Enemy");
                    if (target == null)
                    {
                        //Destroy(this.gameObject);
                    }
                }


            }
        }
        }
    void CheckDead()
    {
        if (hp <= 0)
        {
            deadFlag = true;
        }
        if (deadFlag)
        {
            GaugeEnergyDrop();
            Destroy(this.gameObject);
        }
    }
    public void GaugeEnergyDrop()
    {
        GameObject energys = Instantiate(energy) as GameObject;
        energys.GetComponent<GaugeEnergyControl>().SetPosition(this.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (this.gameObject.tag == "Enemy")
        {
            if (other.gameObject.tag == "Player")
            {
                deadFlag = true;
            }

            if (other.gameObject.tag == "PlayerBullet")
            {
                this.gameObject.tag = "Friend";
                CheckTarget();
            }
        }
        if (this.gameObject.tag == "Friend")
        {
            if (other.gameObject.tag == "Enemy")
            {
                deadFlag = true;
            }

            if (other.gameObject.tag == "EnemyBullet")
            {
                hp--;
            }
        }
    }
    void Damage(int damage)
    {
        hp -= damage;
    }
    public void testmove()
    {

    }
    void ChangeRotate()
    {
        //  this.transform.LookAt(target.transform, new Vector3(0, 0, 1));

        if (this.gameObject.tag == "Enemy")
        {
            if (rotateTime > MaxrotateTime)
            {
                rotateTime--;
            }
            this.transform.Rotate(0, 0, 180 / MaxrotateTime * rotateTime);
        }

        if (this.gameObject.tag == "Friend")
        {
            if (rotateTime < MaxrotateTime)
            {

                rotateTime++;
            }
            this.transform.Rotate(0, 0, 180 / MaxrotateTime * rotateTime);
        }



    }
    void ObjectRotate()
    {
        if (target != null)
        {
            this.transform.LookAt(target.transform, new Vector3(0, 0, 1));
            lastTransform = this.target.transform;
        }
        else
        {
            this.transform.LookAt(lastTransform, new Vector3(0, 0, 1));
        }

    }

}