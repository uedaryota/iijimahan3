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
            if (target == null || target.tag == "Player" || target.tag == "Friend")
            {
                target = GameObject.FindGameObjectWithTag("Boss");
                if (target == null || target.tag == "Player")
                {
                    //Destroy(this.gameObject);
                    GameObject[] objects;
                    objects = GameObject.FindGameObjectsWithTag("Enemy");
                    GameObject near = null;
                    for (int a = 0; a < objects.Length; a++)
                    {
                        if (near == null)
                        {
                            near = objects[a];
                        }
                        else
                        {
                            float len1, len2;
                            len1 = Vector3.Dot(this.transform.position - near.transform.position, this.transform.position - near.transform.position);
                            len2 = Vector3.Dot(this.transform.position - objects[a].transform.position, this.transform.position - objects[a].transform.position);
                            if (len1 > len2)
                            {
                                near = objects[a];
                            }
                        }
                    }
                    target = near;
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

            }
            if (other.gameObject.tag == "FriendBullet")
            {
                BulletDamage(other.gameObject);
            }
            if (other.gameObject.tag == "Friend")
            {
                if (other.GetComponent<ShootEnemy>() != null)
                {
                    Damage(other.GetComponent<ShootEnemy>().hp);
                }
                else if (other.GetComponent<Enemy>() != null)
                {
                    Damage(other.GetComponent<Enemy>().hp);
                }
            }

            return;
        }
        if (this.gameObject.tag == "Friend")
        {
            if (other.gameObject.tag == "Enemy")
            {
                deadFlag = true;
            }

            if (other.gameObject.tag == "EnemyBullet")
            {
                BulletDamage(other.gameObject);
            }
            return;
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
    void BulletDamage(GameObject other)
    {
        hp--;
        //弾の親のオブジェクトがターゲット
        if (other.tag == "FriendBullet")
        {
            if (target != null)
            {
                if (other.GetComponent<FriendBullet>() != null)
                {
                    if (other.GetComponent<FriendBullet>().GetParent() != null)
                    {
                        target = other.GetComponent<FriendBullet>().GetParent();
                    }
                }
            }
        }
        if (other.tag == "EnemyBullet")
        {
            if (target != null)
            {
                if (other.GetComponent<EnemyBullet>() != null)
                {

                    if (other.GetComponent<EnemyBullet>().GetParent() != null)
                    {
                        target = other.GetComponent<EnemyBullet>().GetParent();
                    }
                }
            }
        }

    }
}