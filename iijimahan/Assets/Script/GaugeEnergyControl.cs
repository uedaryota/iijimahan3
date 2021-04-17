using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeEnergyControl : MonoBehaviour
{
    private float easingCount = 0;
    //private bool easingFlag = false;
    public int num = 80;
    private int num2 = 1200;
    public float maxSpeed = 30;
    private float maxSpeed2 = 15;
    private float speed = 0.0f;
    private float speed2 = 0.0f;
    private Vector3 playerPos;
    private Vector3 velocity;

    private GameObject player;
    private GameObject gauge;

    private GameObject target;

    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gauge = GameObject.FindGameObjectWithTag("GaugeTarget");
        target = GameObject.FindGameObjectWithTag("GaugeTarget2");
        playerPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;

        //playerPos = player.transform.position;
        //playerPos = gauge.transform.position;

        if (!hit)
        {
            speed = Easing.SineInOut(easingCount, num, speed, maxSpeed);
            float speed2 = Easing.QuartIn(easingCount, num2, speed, maxSpeed);

            speed = speed - speed2/50;


        }
        else
        {
            speed = Easing.SineInOut(easingCount, num2, speed, maxSpeed2);
            
            playerPos = gauge.transform.position;
        }


        velocity = Move();
        transform.position += velocity * speed * Time.deltaTime * 2;

        easingCount = easingCount + 90 *Time.deltaTime;
    }

    public Vector3 Move()
    {
        float x = this.transform.position.x - playerPos.x;
        float y = this.transform.position.y - playerPos.y;

        Vector3 v = new Vector3(-x, -y, 0);
        v.Normalize();

        return v;
    }
    public void SetPosition(Vector3 pos)
    {
        this.transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Player")
        //{
        //    Destroy(this.gameObject);
        //}

        if (other.gameObject.tag == "GaugeTarget")
        {
            player.GetComponent<PlayerControl>().GaugeUp();
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "GaugeTarget2" && hit == false)
        {
            playerPos = gauge.transform.position;
            easingCount = 0;
            speed = 0;
            hit = true;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "GaugeTarget")
        {
            player.GetComponent<PlayerControl>().GaugeUp();
            Destroy(this.gameObject);
        }

        if (col.gameObject.tag == "GaugeTarget2" && hit == false)
        {
            //playerPos = gauge.transform.position;
            //easingCount = 0;
            //speed = 0;
            //hit = true;

            player.GetComponent<PlayerControl>().GaugeUp();
            Destroy(this.gameObject);
        }
    }


}
