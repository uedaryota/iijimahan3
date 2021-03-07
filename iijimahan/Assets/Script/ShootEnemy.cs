using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public int hp;
    private GameObject target;
    private Vector3 velocity;
    private bool deadFlag = false;
    enum State
    {
        Flont, Back
    }
    private State state;
    public float targetDistance;
    private float shotInterval;
    public int shotIntervalStart;
    // Start is called before the first frame update
    void Start()
    {
        deadFlag = false;
        state = State.Flont;
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
    }
    // Update is called once per frame
    void Update()
    {
        if(state==State.Flont)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        Move();
        Attack();
    }

}
