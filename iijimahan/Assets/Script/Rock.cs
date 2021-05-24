using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField, Header("速度")]private float speed = 1;
    [SerializeField, Header("消えるまでの時間")]private float deletetime = 20;
    public int HP = 20;
    private Vector3 velocity;
    private Object player;
    private Vector3 PlayerPos;
    private float timer;

    public GameObject energy;
    public GameObject effect;

    void Start()
    {
        timer = 0;
        PlayerPos = GameObject.Find("Player").transform.position;
        velocity = PlayerPos - transform.position;
        velocity *= 0.001f * speed;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        if(timer >= deletetime || HP <= 0)
        {
            Destroy(this.gameObject);

            if(HP <= 0)
            {
                if (gameObject.transform.localScale == new Vector3(2, 2, 2))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        GameObject energys = Instantiate(energy) as GameObject;
                        energys.GetComponent<GaugeEnergyControl>().SetPosition(this.transform.position + new Vector3(i * 0.5f, i * 0.5f, 0));
                    }

                    GameObject burst = Instantiate(effect);
                    burst.transform.position = transform.position;
                    burst.transform.localScale = new Vector3(1f, 1f, 1f);
                }

                if (gameObject.transform.localScale == new Vector3(5, 5, 5))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        GameObject energys = Instantiate(energy) as GameObject;
                        energys.GetComponent<GaugeEnergyControl>().SetPosition(this.transform.position + new Vector3(i * 0.5f, i * 0.5f, 0));
                    }

                    GameObject burst = Instantiate(effect);
                    burst.transform.position = transform.position;
                    burst.transform.localScale = new Vector3(2f, 2f,2f);
                }
            }


        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag == "PlayerBullet")
        //{
        //    HP--;
        //}
        if (other.gameObject.tag == "PlayerBulletEffect")
        {
            HP--;
        }
    }
    public void AttackBoss()
    {
        Destroy(this.gameObject);
    }
}
