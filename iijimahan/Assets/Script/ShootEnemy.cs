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
    public float shotIntervalStart = 1.5f;
    public float MaxrotateTime = 0.5f;
    float rotateTime = 0;
    Transform lastTransform;
    // Start is called before the first frame update
    void Start()
    {
        deadFlag = false;
        rotateTime = 0;
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
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);

        if (screenPos.x < Screen.width && screenPos.x > 0)
        {
            if (screenPos.y < Screen.height & screenPos.y > 0)
            {
                if (this.gameObject.tag == "Friend")
                {
                    shotInterval -= Time.deltaTime;
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
                    shotInterval -= Time.deltaTime;
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
        ObjectRotate();
        ChangeRotate();
        if (target != null)
        {
            Move();

            Attack();
        }
        CheakDead();
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);

        if (screenPos.x < Screen.width && screenPos.x > 0)
        {
            if (screenPos.y < Screen.height & screenPos.y > 0)
            { 
                if (this.gameObject.tag == "Enemy")
                {
                    if (other.gameObject.tag == "Player")
                    {
                        deadFlag = true;
                    }

                    if (other.gameObject.tag == "PlayerBullet")
                    {
                        GameObject effect = Instantiate(Resources.Load<GameObject>("Mebius"));
                        effect.transform.position = transform.position;
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

            //else if (target.tag != "Enemy")
            //{

            //}
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
    void Damage(int damage)
    {
        this.hp -= damage;
    }
    public void GaugeEnergyDrop()
    {
        GameObject energys = Instantiate(energy) as GameObject;
        energys.GetComponent<GaugeEnergyControl>().SetPosition(this.transform.position);
    }
    void ChangeRotate()
    {
        //  this.transform.LookAt(target.transform, new Vector3(0, 0, 1));

        if (this.gameObject.tag == "Enemy")
        {
            if (rotateTime > MaxrotateTime)
            {
                rotateTime -= Time.deltaTime;
            }
            this.transform.Rotate(0, 0, 180/ MaxrotateTime * rotateTime);
        }

        if (this.gameObject.tag == "Friend")
        {
            if (rotateTime < MaxrotateTime)
            {
                rotateTime += Time.deltaTime;
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
