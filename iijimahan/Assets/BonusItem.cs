using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour
{
    [SerializeField, Header("速度")]private Vector3 velocity = new Vector3(-0.5f, -1.0f, 0.0f);
    [SerializeField, Header("ボーナスWave突入SE")] private AudioClip SE;
    private GameObject EnemyManager;
    private EnemyManager script;
    private GameObject Option;
    private Option script2;
    private AudioSource audiosource;
    private float DeleteTime = 20.0f;
    private float DelayTime = 3.0f;

    void Start()
    {
        EnemyManager = GameObject.Find("EnemyManager");
        script = EnemyManager.GetComponent<EnemyManager>();
        Option = GameObject.Find("Option");
        script2 = Option.GetComponent<Option>();
    }

    void Update()
    {
        DeleteTime -= Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        if (DeleteTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (SE != null)
            {
                audiosource.volume = script2.GetSEVolume();
                audiosource.PlayOneShot(SE);
            }
            script.SetBonusWave(true);
            isDead();
        }
    }

    private void isDead()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        DelayTime += Time.deltaTime;
        if (DelayTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
