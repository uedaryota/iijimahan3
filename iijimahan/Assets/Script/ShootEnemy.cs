using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public int hp;
    private GameObject target;
    private Vector3 velocity;
    private bool deadFlag = false;
    public GameObject energy;
    public float targetDistance;
    private float shotInterval;
    public int shotIntervalStart;
    // Start is called before the first frame update
    void Start()
    {
        deadFlag = false;
        CheckTarget();
        shotInterval = shotIntervalStart;
    }
    private void Move()
    {
        velocity = Vector3.Normalize(target.transform.position - transform.position);
        if (Vector3.Distance(target.transform.position, transform.position) <= targetDistance)
        {
            velocity = Vector3.zero;
        }
        transform.position += velocity * Time.deltaTime;
    }
        
    void Attack()
    {
        shotInterval--;
        if (shotInterval <= 0)
        {
            GameObject bullet = Instantiate(Resources.Load<GameObject>("EnemyBullet"));
            shotInterval = shotIntervalStart;
            bullet.GetComponent<EnemyBullet>().SetVelocity(Vector3.Normalize(target.transform.position - transform.position));
            bullet.transform.position = transform.position;
          
        }
    }
    void DeadCheak()
    {
        if (hp <= 0)
        {
            deadFlag = true;
        }
        if(deadFlag)
        {
            GaugeEnergyDrop();
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
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
                hp--;
            }
            return;
        }
    
    }

    private void CheckTarget()
    {
        if (this.gameObject.tag == "Enemy")
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        if (this.gameObject.tag == "Friend")
        {
            target = GameObject.FindGameObjectWithTag("Enemy");
        }

    }

    public void GaugeEnergyDrop()
    {
        GameObject energys = Instantiate(energy) as GameObject;
        energys.GetComponent<GaugeEnergyControl>().SetPosition(this.transform.position);
    }
}
