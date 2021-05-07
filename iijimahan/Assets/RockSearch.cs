using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSearch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rock")
        {
           
            this.transform.parent.GetComponent<EnemyHitMove>().SetRock(other.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
