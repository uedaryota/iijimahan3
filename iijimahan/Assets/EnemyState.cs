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
    EnemyManager manager;
    float wave;
    float lastwave;
    bool waveMove;
    GameObject[] team;
    // Start is called before the first frame update
    void Start()
    {
        hp = StartHp;
        power = StartPower;
        deadFlag = false;
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();
        wave = manager.GetWave();
        lastwave = wave;
        waveMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
        CheckWave();
        WaveMove();
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
        if (manager != null && manager.GetGameClear()) 
        {
            Destroy(this.gameObject);
        }
    }
    void CheckWave()
    {
        wave = manager.GetWave();
        if (wave != lastwave)
        {
            waveMove = true;
            team = GameObject.FindGameObjectsWithTag("Friend");
        }
        lastwave = wave;
    }
    void WaveMove()
    {
        if (waveMove)
        {
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
            float number = 0;
            float PointY = 0;
            for (int a = 0; a < team.Length; a++) 
            {
                if (team[a] == this.gameObject)
                {
                    number = a;
                }
            }
            PointY = Screen.height / number;
            if (screenPos.x > Screen.width / 7) 
            {
                transform.position += new Vector3(-Time.deltaTime * 10, 0, 0);
            }
            if (Mathf.Abs(screenPos.y - PointY) > 10)
            {
                if (screenPos.y > PointY)
                {
             //       transform.position += new Vector3(0, -Time.deltaTime * 10, 0);
                }
                else if (screenPos.y < PointY)
                {
              //      transform.position += new Vector3(0, Time.deltaTime * 10, 0);
                }
            }
            else
            {
                waveMove = false;
            }
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
        if(hp > StartHp + StartHp * 0.3f * BuffLevel)
        {
            hp = StartHp + StartHp * 0.3f * BuffLevel;
        }
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
