using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Attack2 : MonoBehaviour
{
    int Cnt = 0;
    int Cnt2 = 0;
    Acter act;
    [SerializeField, Header("ボス攻撃SE")] public AudioClip BulletSE;
    [SerializeField, Header("ボス攻撃SE")] public AudioClip LaserSE;
    [SerializeField, Header("ボス攻撃SE")] public AudioClip EnSE;
    private AudioSource audioSource;
    private GameObject option;
    private Option optionscript;
    private BossHp hp;
    bool SECharge = false;
    // Start is called before the first frame update
    void Start()
    {
        SECharge = false;
        hp = gameObject.GetComponent<BossHp>();
        act = Acter.Start;
        option = GameObject.Find("Option");
        audioSource = GetComponent<AudioSource>();
        optionscript = option.GetComponent<Option>();
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
                if (Cnt * Time.deltaTime > 1)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        audioSource.PlayOneShot(BulletSE);
                        GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet2(transform.position, 0);
                    }
                    Cnt = 0;
                    Cnt2++;
                    
                }
                if (Cnt2>5)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        audioSource.PlayOneShot(BulletSE);
                        GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet4(transform.position, 1);
                        GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 1);
                    }
                    audioSource.PlayOneShot(BulletSE);
                    // GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet5(transform.position, 3);
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet3(transform.position, 3);
                    GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
                    Cnt2 = 0;
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
}
