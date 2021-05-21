using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour
{
    private GameObject EnemyManager;
    private EnemyManager script;
    private float DeleteTime = 10.0f;
    [SerializeField, Header("速度")]private Vector3 velocity = new Vector3(-0.5f, -1.0f, 0.0f);

    void Start()
    {
        EnemyManager = GameObject.Find("EnemyManager");
        script = EnemyManager.GetComponent<EnemyManager>();
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
            script.SetBonusWave(true);
            Destroy(gameObject);
        }
    }
}
