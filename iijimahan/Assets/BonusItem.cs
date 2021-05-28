using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour
{
    [SerializeField, Header("速度")]private Vector3 velocity = new Vector3(-0.5f, -1.0f, 0.0f);
    [SerializeField, Header("ボーナスWave突入SE")] private AudioClip SE;
    [SerializeField]private GameObject obj;
    private GameObject EnemyManager;
    private EnemyManager script;
    private GameObject Option;
    private Option script2;
    private AudioSource audiosource;
    private float DeleteTime = 20.0f;
    private float DelayTime = 1.0f;
    private bool DeadFlag;
    private int wave;
    private int old_wave;

    void Start()
    {
        EnemyManager = GameObject.Find("EnemyManager");
        script = EnemyManager.GetComponent<EnemyManager>();
        Option = GameObject.Find("Option");
        script2 = Option.GetComponent<Option>();
        audiosource = gameObject.GetComponent<AudioSource>();
        DeadFlag = false;
        wave = script.GetWave();
        old_wave = script.GetWave();
    }

    void Update()
    {
        DeleteTime -= Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        old_wave = wave;
        wave = script.GetWave();
        if(wave != old_wave)
        {
            DeadFlag = true;
        }
        if (DeleteTime <= 0)
        {
            Destroy(gameObject);
        }
        if(DeadFlag == true)
        {
            DelayTime += Time.deltaTime;
            if (DelayTime <= 0)
            {
                Destroy(gameObject);
            }
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            Destroy(obj);

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
            DeadFlag = true;
        }
    }
}
