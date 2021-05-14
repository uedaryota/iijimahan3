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
    BuffEffectScript effect;
    public float MaxrotateTime = 0.5f;
    float rotateTime = 0;
    float rotateX, rotateY;
    float currentrotateZ, rotateZ;
    float muteki = 3;
    float blinkTime;
    // Start is called before the first frame update
    void Start()
    {
        rotateX = 0;
        rotateY = 0;
        currentrotateZ = 180;
        rotateZ = 0;
        muteki = 0;
        hp = StartHp;
        blinkTime = 0;
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
        Blinking();
        CheckDead();
        CheckWave();
        ChangeRotate();
        WaveMove();
    }
    void Blinking()
    {
        muteki -= Time.deltaTime;
        MeshRenderer[] ren = GetComponentsInChildren<MeshRenderer>();
        int count = ren.Length;
      
        if (muteki > 0)
        {
            blinkTime -= Time.deltaTime;

            if (blinkTime < 0)
            {
                if (ren[0] != null && ren[0].enabled == false) 
                {
                    blinkTime = 0.15f;
                    for (int a = 0; a < count; a++)
                    {
                        ren[a].enabled = true;
                    }
                }
                else if(ren[0] != null && ren[0].enabled == true)
                {
                    blinkTime = 0.15f;
                    for (int a = 0; a < count; a++)
                    {
                        ren[a].enabled = false;
                    }
                }
            }
              
        }
        else
        {
            blinkTime -= Time.deltaTime;
            
            if (blinkTime < 0)
            {
                
                if (ren[0] != null && ren[0].enabled == false)
                {
                    for (int a = 0; a < count; a++)
                    {
                        ren[a].enabled = true;
                    }
                }    
            }
        }
    }
    void ChangeRotate()
    {
        //  this.transform.LookAt(target.transform, new Vector3(0, 0, 1));

        if (this.gameObject.tag == "Enemy")
        {
            if (rotateTime > MaxrotateTime)
            {
                rotateTime -= Time.deltaTime;
                muteki = 2;
                rotateX = 180 / MaxrotateTime * rotateTime;
            }
            //  this.transform.Rotate(0, 0, 180/ MaxrotateTime * rotateTime);
        }

        if (this.gameObject.tag == "Friend")
        {
            if (rotateTime < MaxrotateTime)
            {
                rotateTime += Time.deltaTime;
                muteki = 2;
                rotateX = -180 / MaxrotateTime * rotateTime;
            }
            // this.transform.Rotate(0, 0, 180 / MaxrotateTime * rotateTime);
        }



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
        if (waveMove && gameObject.tag == "Enemy")
        {
            GameObject burst = Instantiate(Resources.Load<GameObject>("Burst"));
            burst.transform.position = transform.position;
            burst.GetComponent<EffectScript>().HitSE();
            deadFlag = true;
        }
        if (waveMove)
        {
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position);
            int number = 0;
            int PointY = 0;
            int PointX = 0;
            //int line = 0;
            for (int a = 0; a < team.Length; a++) 
            {
                if (team[a] == this.gameObject)
                {
                    number = a;
                }
            }
            if ((team.Length - 1) % 5 >= 2) //2以上
            {
                PointY = Screen.height / ((team.Length - 1) / 5 * 2 + 2);
                if (number % 5 >= 2)
                {
                    PointY = PointY * ((number / 5 * 2) + 1);
                }
                else if (number % 5 <= 1)
                {
                    PointY = PointY * ((number / 5 * 2));
                }

            }
            else if ((team.Length - 1) % 5 <= 1) //1以下
            {
                PointY = Screen.height / ((team.Length - 1) / 5 * 2 + 1);
                if (number % 5 >= 2)
                {
                    PointY = PointY * ((number / 5 * 2) + 1);
                }
                else if (number % 5 <= 1)
                {
                    PointY = PointY * ((number / 5 * 2));
                }
            }
            if (number % 5 >= 2)//2以上
            {
                PointX = Screen.width / 3 / 4 * ((number % 5) - 1)+ Screen.width / 7;
            }
            else if (number % 5 <= 1) //1以下
            {
                PointX = Screen.width / 3 / 3 * ((number % 5) + 1) + Screen.width / 7;
            }
           
            bool f = false;
         
            if (Mathf.Abs(screenPos.x - PointX) > 10)
            {
                if (screenPos.x > PointX)
                {
                    transform.position += new Vector3(-Time.deltaTime * 7, 0, 0);
                }
                else if (screenPos.y < PointY)
                {
                    transform.position += new Vector3(Time.deltaTime * 7, 0, 0);
                }
                f = true;
            }
            if (Mathf.Abs(screenPos.y - PointY) > 10)
            {
                if (screenPos.y > PointY)
                {
                    transform.position += new Vector3(0, -Time.deltaTime * 7, 0);
                }
                else if (screenPos.y < PointY)
                {
                   transform.position += new Vector3(0, Time.deltaTime *7, 0);
                }
                f = true;
            }
            if (!f) 
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
        if (muteki > 0) 
        {
            Debug.Log("Nodamage");
        }
        else
        {
            hp -= damage;
        }

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
            effect = buffInstance.GetComponent<BuffEffectScript>();
        }
        if (BuffLevel == 1)
        {
            hp += StartHp * 1.3f;
            power += StartPower * 1.5f;
            effect.ChangeLevel1();
        }
        if (BuffLevel == 2)
        {

            hp += StartHp * 1.3f;
            power += StartPower * 1.5f;
            effect.ChangeLevel2();
        }
        if (BuffLevel == 3)
        {

            hp += StartHp * 1.3f;
            power += StartPower * 1.5f;
            effect.ChangeLevel3();
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
