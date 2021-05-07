using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitMove : MonoBehaviour
{
    GameObject rock;
    Vector3 velocity = 0;
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
        if (other.transform.tag == "Rock")
        {
            Vector3 vel = other.transform.position - transform.position;
            vel = vel.normalized / 3;
            transform.position -= vel;
        }
    }
    void AvoidRock()
    {
        float speed = 3;
        float targetDistance = 7;
        if (rock != null)
        {
            if (Vector3.Distance(rock.transform.position, transform.position) <= targetDistance)
            {
              //  Debug.Log("aaa");
                velocity += Vector3.Normalize(rock.transform.position - transform.position);
                velocity = Vector3.Normalize(velocity) * speed;
                transform.position -= velocity * Time.deltaTime;
            }

        }
    }
    public void SetRock(GameObject _rock)
    {
        rock = _rock;
    }
    // Update is called once per frame
    void Update()
    {
        AvoidRock();
    }
}
