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
        if(target!=null)
        {
            velocity = Vector3.Normalize(target.transform.position - transform.position);
            if (Vector3.Distance(target.transform.position, transform.position) <= targetDistance)
            {
                velocity = Vector3.zero;
            }
            transform.position += velocity * Time.deltaTime;
        }
     
    }
        
    void Attack()
    {
        if (this.gameObject.tag == "Friend")
        {
            shotInterval--;
            if (shotInterval <= 0)
            {
                GameObject bullet = Instantiate(Resources.Load<GameObject>("FriendBullet"));
                shotInterval = shotIntervalStart;
                bullet.GetComponent<FriendBullet>().SetVelocity(Vector3.Normalize(target.transform.position - transform.position));
                bullet.transform.position = transform.position;
                bullet.GetComponent<FriendBullet>().SetParent(this.gameObject);
            }
        }
        else if (this.gameObject.tag == "Enemy") 
        {
            shotInterval--;
            if (shotInterval <= 0)
            {
                GameObject bullet = Instantiate(Resources.Load<GameObject>("EnemyBullet"));
                shotInterval = shotIntervalStart;
                bullet.GetComponent<EnemyBullet>().SetVelocity(Vector3.Normalize(target.transform.position - transform.position));
                bullet.transform.position = transform.position;
                bullet.GetComponent<EnemyBullet>().SetParent(this.gameObject);
            }
        }
    }
    void CheakDead()
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
       CheckTarget();
       Move();
        ObjectRotate();
       Attack();
       CheakDead();
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

    private void CheckTarget()
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
            else if (target.tag != "Enemy") 
            {
                target = GameObject.FindGameObjectWithTag("Enemy");
                if (target == null)
                {
                    Destroy(this.gameObject);
                }
            }
        }

    }
    void BulletDamage(GameObject other)
    {
        hp--;
        //弾の親のオブジェクトがターゲット
        if (other.tag == "FriendBullet")
        {
            target = other.GetComponent<FriendBullet>().GetParent();
        }
        if (other.tag == "EnemyBullet")
        {
            target = other.GetComponent<EnemyBullet>().GetParent();
        }
       
    }
    void Damage(int damage)
    {
        this.hp -= damage;
    }
    public void GaugeEnergyDrop()
    {
        GameObject energys = Instantiate(energy) as GameObject;
        energys.GetComponent<GaugeEnergyControl>().SetPosition(this.transform.position);
    }
    void ObjectRotate()
    {
        if (target != null)
        {
            if (this.gameObject.tag == "Enemy")
            {
                this.transform.LookAt(target.transform, new Vector3(0, 0, 1));
            }

            if (this.gameObject.tag == "Friend")
            {
                this.transform.LookAt(target.transform, new Vector3(0, 0, -1));
            }

        }

    }
    void ToBack()
    {
        Quaternion aim = this.transform.rotation;
        aim.Set(aim.x, aim.y, aim.z + 180, aim.w);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, aim, 0.5f);
    }
}
