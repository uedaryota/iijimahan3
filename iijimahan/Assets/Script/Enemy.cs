using System.Collections;
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
        CheckDead();
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
            velocity = target.transform.position - transform.position;
            velocity = Vector3.Normalize(velocity) * 5;
        }
    }
    void CheckTarget()
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
    void CheckDead()
    {
        if(deadFlag)
        {
            GaugeEnergyDrop(); 
            Destroy(this.gameObject);
        }
    }
    public void GaugeEnergyDrop()
    {
        GameObject energys = Instantiate(energy) as GameObject;
        energys.GetComponent <GaugeEnergyControl>().SetPosition(this.transform.position);
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
    public void testmove()
    {

    }
}
