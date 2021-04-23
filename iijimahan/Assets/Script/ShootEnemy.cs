
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    EnemyState state;
    private GameObject target;
    private Vector3 velocity;
    private bool deadFlag = false;
    public GameObject energy;
    public float targetDistance;
    private float shotInterval;
    public float shotIntervalStart = 1.5f;
    public float MaxrotateTime = 0.5f;
    float rotateTime = 0;
    float rotateX, rotateY;
    float currentrotateZ, rotateZ;
    // public GameObject buffEffect;
    GameObject buffInstance;

    Vector3 lastPosition;
    // Start is called before the first frame update
    void Start()
    {
        rotateX = 0;
        rotateY = 0;
        currentrotateZ = 180;
        rotateZ = 0;
        deadFlag = false;

        state = GetComponent<EnemyState>();
        rotateTime = 0;
        CheckTarget();
        shotInterval = shotIntervalStart;
    }
    private void Move()
    {
        if (target != null)
        {

            float speed = 5;
            if (Vector3.Distance(target.transform.position, transform.position) <= targetDistance &&
                Vector3.Distance(target.transform.position, transform.position) >= targetDistance / 2)
            {
                velocity -= Vector3.Normalize(velocity) * speed / 15;
            }
            else if (Vector3.Distance(target.transform.position, transform.position) <= targetDistance / 2)
            {
                velocity -= Vector3.Normalize(target.transform.position - transform.position);
                velocity = Vector3.Normalize(velocity) * speed;
            }
            else
            {
                velocity += Vector3.Normalize(target.transform.position - transform.position);
                velocity = Vector3.Normalize(velocity) * speed;
            }

           

            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
            if (screenPos.x > Screen.width|| screenPos.x < 0)
            {
                velocity = Vector3.Normalize(new Vector3(Screen.width / 2, Screen.height / 2, 0) - screenPos) * speed*3;
            }
         
            if (screenPos.y > Screen.height|| screenPos.y < 0)
            {
                velocity = Vector3.Normalize(new Vector3(Screen.width / 2, Screen.height / 2, 0) - screenPos) * speed*3;
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
                    if(Vector3.Distance(target.transform.position, transform.position) <= targetDistance)
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
                    
                }
                else if (this.gameObject.tag == "Enemy")
                {
                    if (Vector3.Distance(target.transform.position, transform.position) <= targetDistance)
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
       
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);

        if (other.gameObject.tag == "KyoukaBullet")
        {
            if (this.gameObject.tag == "Friend")
            {
                state.Buff();
            }
        }
        if (screenPos.x < Screen.width && screenPos.x > 0)
        {
            if (screenPos.y < Screen.height & screenPos.y > 0)
            { 
                if (this.gameObject.tag == "Enemy")
                {
                    if (other.gameObject.tag == "GaugeBullet")
                    {
                        //  Buff();
                        this.gameObject.tag = "Friend";
                        GameObject effect = Instantiate(Resources.Load<GameObject>("Mebius"));
                        effect.transform.position = transform.position;
                    }
                    if (other.gameObject.tag == "Player")
                    {
                        deadFlag = true;
                    }

                    if (other.gameObject.tag == "PlayerBullet")
                    {
                       // Buff();
                        GameObject effect = Instantiate(Resources.Load<GameObject>("Mebius"));
                        effect.transform.position = transform.position;
                        Destroy(other.gameObject);
                        this.gameObject.tag = "Friend";

                    }
                    if (other.gameObject.tag == "FriendBullet")
                    {
                        BulletDamage(other.gameObject);
                    }
                    if (other.gameObject.tag == "Friend")
                    {
                         if (other.GetComponent<EnemyState>() != null)
                        {
                            Damage(other.GetComponent<EnemyState>().GetPower());
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
                        if (other.GetComponent<BossPower>() != null)
                        {
                            state.Damage(other.GetComponent<BossPower>().GetPower());
                        }
                        else
                        {
                            BulletDamage(other.gameObject);
                        }
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
                        state.Damage(target.GetComponent<EnemyState>().GetPower());
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
                        state.Damage(target.GetComponent<EnemyState>().GetPower());
                    }
                }
            }
        }

    }


    void Damage(float damage)
    {
        state.Damage(damage);
    }
  
    void ChangeRotate()
    {
        //  this.transform.LookAt(target.transform, new Vector3(0, 0, 1));

        if (this.gameObject.tag == "Enemy")
        {
            if (rotateTime > MaxrotateTime)
            {
                rotateTime -= Time.deltaTime;
                rotateX = 180 / MaxrotateTime * rotateTime;
            }
            //  this.transform.Rotate(0, 0, 180/ MaxrotateTime * rotateTime);
        }

        if (this.gameObject.tag == "Friend")
        {
            if (rotateTime < MaxrotateTime)
            {
                rotateTime += Time.deltaTime;
                rotateX = -180 / MaxrotateTime * rotateTime;
            }
            // this.transform.Rotate(0, 0, 180 / MaxrotateTime * rotateTime);
        }



    }
    void ObjectRotate()
    {

        if (target != null)
        {
            Quaternion a = Quaternion.identity;
            Vector3 dir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x);
            rotateZ = angle / (3.1415f / 180);
            //if (rotateZ < 0)
            //{
            //    rotateZ = rotateZ + 360;
            //}
            if (rotateZ < 0)
            {
                rotateZ += 360;
            }
            if (currentrotateZ > 180 && Mathf.Abs(rotateZ - currentrotateZ) > 180)
            {
                currentrotateZ -= 360;
            }
            if (currentrotateZ < 180 && Mathf.Abs(rotateZ - currentrotateZ) > 180)
            {
                currentrotateZ += 360;
            }
            if (Mathf.Abs(rotateZ - currentrotateZ) > 1)
            {
                if (rotateZ - currentrotateZ < 0)
                {
                    currentrotateZ -= Time.deltaTime * 150;
                }
                else
                {
                    currentrotateZ += Time.deltaTime * 150;
                }
            }

            a.eulerAngles = new Vector3(0, 0, currentrotateZ);
            transform.rotation = a;

            transform.Rotate(new Vector3(rotateX, rotateY, 0));
            // transform.Rotate(0, 0, angle);
            //   this.transform.LookAt(target.transform, new Vector3(0, 0, 1));
            lastPosition = this.target.transform.position;
            
        }
        else
        {
            Quaternion a = Quaternion.identity;
            Vector3 dir = lastPosition - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x);
            rotateZ = angle / (3.1415f / 180);
            //if (rotateZ < 0)
            //{
            //    rotateZ = rotateZ + 360;
            //}
            if (rotateZ < 0)
            {
                rotateZ += 360;
            }
            if (currentrotateZ > 180 && Mathf.Abs(rotateZ - currentrotateZ) > 180)
            {
                currentrotateZ -= 360;
            }
            if (currentrotateZ < 180 && Mathf.Abs(rotateZ - currentrotateZ) > 180)
            {
                currentrotateZ += 360;
            }
            //  if (rotateZ < 0)
            {
                if (rotateZ - currentrotateZ < 0)
                {
                    currentrotateZ -= Time.deltaTime * 90;
                }
                else
                {
                    currentrotateZ += Time.deltaTime * 90;
                }
            }

            a.eulerAngles = new Vector3(0, 0, currentrotateZ);
            transform.rotation = a;

            transform.Rotate(new Vector3(rotateX, rotateY, 0));
            // transform.Rotate(0, 0, angle);
            //   this.transform.LookAt(target.transform, new Vector3(0, 0, 1));
            
        }

    }
   
}
