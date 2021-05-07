
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHitMove : MonoBehaviour
{
    private List<GameObject> rock = new List<GameObject>();
    Vector3 velocity = Vector3.zero;
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
        float speed = 6;
        float targetDistance = 7;
        if (rock.Count != 0)
        {
            for (int a = 0; a < rock.Count; a++)
            {
                if (Vector3.Distance(rock[a].transform.position, transform.position) <= targetDistance)
                {
                    //  Debug.Log("aaa");
                    velocity += Vector3.Normalize(rock[a].transform.position - transform.position);
                    velocity = Vector3.Normalize(velocity) * speed;
                    transform.position -= velocity * Time.deltaTime;
                }
            }


        }
    }
    public void SetRock(GameObject _rock)
    {
        if(rock.Count == 0)
        {
            rock.Add(_rock);
        }
        else
        {
            bool origin = true;
            for (int a = 0; a < rock.Count; a++)
            {
                if (rock[a] == _rock)
                {
                    origin = false;
                }
            }
            if (origin)
            {
                rock.Add(_rock);
            }
        }
    }
    void CheckRockList()
    {
        for (int a = 0; a < rock.Count; a++)
        {
            if (rock[a] == null)
            {
                rock.Remove(rock[a]);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckRockList();
        AvoidRock();
    }
}
