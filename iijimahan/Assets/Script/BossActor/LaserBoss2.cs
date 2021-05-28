using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBoss2 : MonoBehaviour
{
    int Cnt = 440;
    float Angle;
    Acter act;
    int Cnt2 = 0;
  
    [SerializeField, Header("ボス攻撃SE")] public AudioClip BulletSE;
    [SerializeField, Header("ボス攻撃SE")] public AudioClip LaserSE;
    [SerializeField, Header("ボス攻撃SE")] public AudioClip EnSE;
    bool SECharge = false;
    private AudioSource audioSource;
    private GameObject option;
    private Option optionscript;
    private BossHp hp;
    // Start is called before the first frame update
    void Start()
    {
        hp = gameObject.GetComponent<BossHp>();
        act = Acter.Start;
        audioSource = GetComponent<AudioSource>();
        option = GameObject.Find("Option");
        optionscript = option.GetComponent<Option>();
        Cnt2 = 0;
        SECharge = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;
        if (hp.Hp <= 0) return;
        switch (act)
        {
            case Acter.Start:
                if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossFirstAction>().action == BossFirstAction.MoveAction.End)
                {
                    act = Acter.Attack;
                }
                break;
            case Acter.Attack:
                if (Cnt > 400)
                {

                    if (GetComponent<BossHp>().GetHp() > GetComponent<BossHp>().GetMaxHp() / 5)
                    {
                        for (int x = 0; x < 4; x++)
                        {
                            audioSource.PlayOneShot(BulletSE);
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet3(transform.position, 1);
                        }
                        for (int x = 0; x < 1; x++)
                        {
                            audioSource.PlayOneShot(BulletSE);
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet4(transform.position, 1);
                        }
                    }
                    audioSource.PlayOneShot(BulletSE);
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 1);
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet6(transform.position, 1);
                    Cnt = 0;
                }
                if(Cnt>100&& GetComponent<BossHp>().GetHp() < GetComponent<BossHp>().GetMaxHp() / 5)
                {

                    if (GetComponent<BossHp>().GetHp() < GetComponent<BossHp>().GetMaxHp() / 5)
                    {
                        Cnt2++;
                        if (Cnt2 > 16)
                        {
                            for (int x = 0; x < 10; x++)
                            {
                                audioSource.PlayOneShot(BulletSE);
                                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet3(transform.position, 1);
                            }
                            Cnt2 = 0;
                        }

                    }
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet6(transform.position, 1);
                    Cnt = 0;
                }
                
                Cnt++;

                
            
                break;
        }
        
    }
    public void ChargeFinish()
    {
        if (SECharge == true)
        {
            audioSource.Stop();
            SECharge = false;
        }
    }
    public void ChargeStart()
    {
        if (SECharge == false)
        {
            audioSource.volume = optionscript.GetSEVolume();
            audioSource.PlayOneShot(LaserSE);
            SECharge = true;
        }
    }
    public void EnStart()
    {
        if (SECharge == false)
        {
            audioSource.volume = optionscript.GetSEVolume();
            audioSource.PlayOneShot(EnSE);
            SECharge = true;
        }
    }


    public AudioClip ChageMosic()
    {
        return LaserSE;
    }
}
