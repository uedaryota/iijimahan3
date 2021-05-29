using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    EnemyState state;
    private GameObject target;

    private Vector3 velocity;
    public float speed = 5;
   
    
    float MaxrotateTime = 0.5f;
    float rotateTime = 0;
    Vector3 lastPosition;
    //float rotateTime = 0;
    float rotateX, rotateY;
    float currentrotateZ, rotateZ;

    // Start is called before the first frame update
    void Start()
    {
        rotateX = 0;
        rotateY = 0;
        currentrotateZ = 180;
        rotateZ = 0;
        state = GetComponent<EnemyState>();

        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckTarget();

        ObjectRotate();
        ChangeRotate();
        Move();
    }
    void Move()
    {
        //少しずつ加速度に追加していく
        if (target != null)
        {
            Vector3 vel = Vector3.Normalize(target.transform.position - transform.position) / 5;
            velocity += vel;
            velocity = Vector3.Normalize(velocity) * speed;
            transform.position += velocity * Time.deltaTime;
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
            if (target == null || target.tag == "Player" || target.tag == "Friend" || target.tag == "BossDead")
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
                                Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, near.transform.position);
                                if (screenPos.x > 0 && screenPos.x < Screen.width
                                    && screenPos.y > 0 && screenPos.y < Screen.height)
                                {
                                    near = objects[a];
                                }
                            }
                        }
                    }
                    target = near;
                }
            }

        }
        if (target != null)
        {
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, target.transform.position);
            if (screenPos.x < 0 || screenPos.x > Screen.width)
            {
                target = null;
            }
            if (screenPos.y < 0 || screenPos.y > Screen.height)
            {
                target = null;
            }

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
            if (screenPos.y < Screen.height && screenPos.y > 0)
            {

                if (this.gameObject.tag == "Enemy")
                {
                    if (other.gameObject.tag == "GaugeBullet")
                    {
                        this.gameObject.tag = "Friend";
                        GameObject effect = Instantiate(Resources.Load<GameObject>("Mebius"));
                        effect.transform.position = transform.position;
                    }
                    if (other.gameObject.tag == "Player")
                    {
                        state.Dead();
                    }

                    if (other.gameObject.tag == "PlayerBullet")
                    {
                        //  Buff();
                        this.gameObject.tag = "Friend";
                        GameObject effect = Instantiate(Resources.Load<GameObject>("Mebius"));
                        Destroy(other.gameObject);
                        effect.transform.position = transform.position;
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
                        state.Dead();
                    }

                    if (other.gameObject.tag == "Boss")
                    {
                        state.Dead();
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
                    if (other.gameObject.tag == "ReverseBullet")
                    {
                        //  Buff();
                        this.gameObject.tag = "Enemy";
                        GameObject effect = Instantiate(Resources.Load<GameObject>("Mebius"));
                        Destroy(other.gameObject);
                        effect.transform.position = transform.position;
                    }
                    return;
                }

            }
        }

    }
    void Damage(float damage)
    {
        state.Damage(damage);
    }
    public void testmove()
    {

    }
    void ChangeRotate()
    {
        //  this.transform.LookAt(target.transform, new Vector3(0, 0, 1));
        //if(Input.GetKeyDown(KeyCode.G))
        //{
        //    this.gameObject.tag = "Enemy";
        //}
        if (this.gameObject.tag == "Enemy")
        {
            if (rotateTime > 0) 
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

            if (Mathf.Abs(rotateZ - currentrotateZ) > 3)
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
          
            lastPosition= this.target.transform.position;
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
    public void DeadEnd()
    {
        state.Dead();
    }
}