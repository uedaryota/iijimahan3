using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float StartHp = 300;
    float hp;
    public float StartPower = 100;
    float power;
    float BuffLevel = 0;
    private bool deadFlag = false;
    GameObject buffInstance;
    public GameObject energy;
    // Start is called before the first frame update
    void Start()
    {
        hp = StartHp;
        power = StartPower;
        deadFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
    }
    void CheckDead()
    {
        if (hp <= 0)
        {
            deadFlag = true;
        }
        if (deadFlag)
        {
            GaugeEnergyDrop();
            Destroy(this.gameObject);
        }
    }
    public void Dead()
    {
        deadFlag = true;
    }
    public float GetHp()
    {
        return hp;
    }
    public float GetPower()
    {
        return power;
    }
    public void Damage(float damage)
    {
        hp -= damage;
    }
    public void LaserDamage()
    {
        if (this.gameObject.tag == "Friend")
        {
            Damage(100f);
        }
    }
    public void Buff()
    {
        BuffLevel += 1;
        if (buffInstance == null)
        {
            buffInstance = Instantiate(Resources.Load<GameObject>("BuffParticle"));
            buffInstance.GetComponent<BuffEffectScript>().SetParent(gameObject);
        }
        if (BuffLevel == 1)
        {
            hp += StartHp * 1.3f;
            power += StartPower * 1.5f;
        }
        if (BuffLevel == 2)
        {

            hp += StartHp * 1.3f;
            power += StartPower * 1.5f;
        }
        if (BuffLevel == 3)
        {

            hp += StartHp * 1.3f;
            power += StartPower * 1.5f;
        }
    }
    public float GetBuffLevel()
    {
        return BuffLevel;
    }
    public void GaugeEnergyDrop()
    {
        GameObject energys = Instantiate(energy) as GameObject;
        energys.GetComponent<GaugeEnergyControl>().SetPosition(this.transform.position);
    }
}
