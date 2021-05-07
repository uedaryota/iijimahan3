using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public float Hp = 1500;
    bool deadFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        deadFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheakDead();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FriendBullet")
        {
            EnemyState state = other.GetComponent<FriendBullet>().GetParent().GetComponent<EnemyState>();
            Hp -= state.GetPower();
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            EnemyState state = other.GetComponent<EnemyBullet>().GetParent().GetComponent<EnemyState>();
            Hp -= state.GetPower();
        }

    }
    void CheakDead()
    {
        if(Hp<=0)
        {
            deadFlag = true;
        }
        if(deadFlag)
        {
            Destroy(this.gameObject);
        }
    }
}
