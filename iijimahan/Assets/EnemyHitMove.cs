using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Enemy" || other.transform.tag == "Player") 
        {
            Vector3 vel = other.transform.position - transform.position;
            vel = vel.normalized / 50;
            transform.position -= vel;
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
