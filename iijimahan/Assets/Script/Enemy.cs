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
    enum State
    {
        Flont,Back
    }
    private State state;
    // Start is called before the first frame update
    void Start()
    {
        deadFlag = false;
        currentCharge = 0;
        currentMove = 0;
        state = State.Flont;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
        CheckTarget();
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
        target = GameObject.FindGameObjectWithTag("Player"); 
    }
    void CheckDead()
    {
        if(deadFlag)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            deadFlag = true;
        }
    }
    public void testmove()
    {

    }
}
