using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPower : MonoBehaviour
{
    public float power=1;
    
    // Start is called before the first frame update
    void Start()
    {
        power = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHp>().GetPower();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetPower()
    {
        return power;
    }
}
