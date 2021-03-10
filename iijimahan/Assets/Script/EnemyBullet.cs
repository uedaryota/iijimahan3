using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 velocity;
    private int life;
    public int speed;
    private bool deadFlag = false;
    private GameObject parent;
    enum State
    {
        Flont, Back
    }
    private State state;
    // Start is called before the first frame update
    void Start()
    {
        life = 300;
        state = State.Flont;
        deadFlag = false;
        //shotInterval = 30;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
        if (other.gameObject.tag == "Friend")
        {
            deadFlag = true;
        }
    }
    
    void Move()
    {
        transform.position += speed * velocity * Time.deltaTime;
        life--;
        if (life <= 0)
        {
            deadFlag = true;
        }
    }
    void Attack()
    {

    }
    public void Initialize()
    {
        life = 300;
        velocity = Vector3.zero;
        state = State.Flont;
        deadFlag = false;
    }
    public void SetVelocity(Vector3 velocity)
    {
        this.velocity = velocity;
    }
    public void SetParent(GameObject parent)
    {
        this.parent = parent;
    }
    public GameObject GetParent()
    {
        return parent;
    }
}

