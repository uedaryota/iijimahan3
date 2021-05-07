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
        if (other.transform.tag == "Enemy" || other.transform.tag == "Player"|| other.transform.tag == "Friend")  
        {
            Vector3 vel = other.transform.position - transform.position;
            vel = vel.normalized/ 30 ;
            transform.position -= vel ;
        }
        if ( other.transform.tag == "Rock")
        {
            Vector3 vel = other.transform.position - transform.position;
            vel = vel.normalized / 3;
            transform.position -= vel;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
