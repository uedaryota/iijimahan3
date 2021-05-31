using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyoukaTossinBoss : MonoBehaviour
{
    [SerializeField, Header("ボス攻撃SE")] public AudioClip BulletSE;
    [SerializeField, Header("反転の弾")]
    public GameObject gaugebullet;
    Patern patern;
    LaserUpDown updown;
    private AudioSource audioSource;
    int Cnt = 0;
    int Cnt2 = 0;
    int Cnt3 = 0;
    private BossHp hp;
    // Start is called before the first frame update

    [SerializeField, Header("ボス攻撃SE")] public AudioClip LaserSE;
    [SerializeField, Header("ボス攻撃SE")] public AudioClip EnSE;
    private GameObject option;
    private Option optionscript;
    bool SECharge = false;



    void Start()
    {
        hp = gameObject.GetComponent<BossHp>();
        audioSource = GetComponent<AudioSource>();
        patern = Patern.First;
        updown = LaserUpDown.Up;
        option = GameObject.Find("Option");
        optionscript = option.GetComponent<Option>();
        SECharge = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.timeScale <= 0) return;
        if (hp.Hp <= 0)
        {
            ChargeFinish();
        }
        if (hp.Hp <= 0) return;
        if (GameObject.FindGameObjectWithTag("Boss") == null) Destroy(gameObject);

        if (GetComponent<BossMove>().nowact() == BossMove.MoveAction.Normal)
        {
            GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
        }
        if (GetComponent<BossMove>().nowact() == BossMove.MoveAction.End)
        {
            GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
        }
        if (GetComponent<BossMove>().nowact() == BossMove.MoveAction.Action1)
        {
            Cnt++;
            switch (patern)
            {
                case Patern.First:
                    {
                        if (Cnt * Time.deltaTime > 1)
                        {
                            audioSource.PlayOneShot(BulletSE);
                            //GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet4(transform.position, 3);
                            // GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 2);
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet5(transform.position, 3);
                            for (int i = 0; i < 3; i++)
                            {
                                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet2(transform.position, 2);
                            }
                            Cnt = 0;
                            Cnt2++;
                            GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
                        }
                        if (Cnt2 > 5)
                        {
                            GameObject gmobj = Instantiate(gaugebullet) as GameObject;
                            gmobj.GetComponent<ReverseBullet>().SetPosition(this.transform.position);
                            gmobj.GetComponent<ReverseBullet>().SetGaugeFlag(true);
                            Cnt2 = 0;
                            GetComponent<BossMove>().action = BossMove.MoveAction.AttackMove;
                        }

                        if (GetComponent<BossHp>().GetHp() < GetComponent<BossHp>().GetMaxHp() * 0.8 && patern == Patern.First)
                        {
                            patern = Patern.Second;
                        }
                        break;
                    }
                case Patern.Second://レーザー主体
                    {
                        if (Cnt * Time.deltaTime > 2)
                        {
                            audioSource.PlayOneShot(BulletSE);
                            //
                            // GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 2);
                            for (int i = 0; i < 5; i++)
                            {
                                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet5(transform.position, 3);
                                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet4(transform.position, 3);
                            }
                            Cnt = 0;
                            Cnt2++;
                            Cnt3++;
                            GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
                        }
                        if (Cnt3 > 3f)
                        {
                            for (int i = 0; i <= 2; i++)
                            {
                                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet3(transform.position, 1);
                            }
                            Cnt3 = 0;
                        }
                        if (Cnt2 > 5)
                        {
                            Cnt2 = 0;
                            GameObject gmobj = Instantiate(gaugebullet) as GameObject;
                            gmobj.GetComponent<ReverseBullet>().SetPosition(this.transform.position);
                            gmobj.GetComponent<ReverseBullet>().SetGaugeFlag(true);
                            GetComponent<BossMove>().action = BossMove.MoveAction.AttackMove;
                        }
                        if (GetComponent<BossHp>().GetHp() < GetComponent<BossHp>().GetMaxHp() * 0.6 && patern == Patern.Second)
                        {
                            patern = Patern.Therd;
                        }
                        break;
                    }
                case Patern.Therd://弾幕三種守り
                    {
                        if (Cnt * Time.deltaTime > 1)
                        {
                            audioSource.PlayOneShot(BulletSE);
                            //GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet4(transform.position, 3);
                            // GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 2);
                            for (int i = 0; i < 3; i++)
                            {
                                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet2(transform.position, 2);

                            }
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet5(transform.position, 3);
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet4(transform.position, 1);
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 1);
                            Cnt = 0;
                            Cnt2++;
                            GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
                        }
                        if (Cnt2 > 5)
                        {
                            GameObject gmobj = Instantiate(gaugebullet) as GameObject;
                            gmobj.GetComponent<ReverseBullet>().SetPosition(this.transform.position);
                            gmobj.GetComponent<ReverseBullet>().SetGaugeFlag(true);
                            Cnt2 = 0;
                            GetComponent<BossMove>().action = BossMove.MoveAction.AttackMove;
                        }
                        if (GetComponent<BossHp>().GetHp() < GetComponent<BossHp>().GetMaxHp() * 0.4 && patern == Patern.Therd)
                        {
                            patern = Patern.Force;
                        }
                        break;
                    }
                case Patern.Force://弾幕三種守り＋レーザーも追加
                    {
                        if (Cnt * Time.deltaTime > 2)
                        {
                            audioSource.PlayOneShot(BulletSE);
                            //GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet4(transform.position, 3);
                            // GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 2);
                            for (int i = 0; i < 3; i++)
                            {
                                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet2(transform.position, 2);

                            }
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet5(transform.position, 3);
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet4(transform.position, 1);

                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 1);
                            Cnt = 0;
                            Cnt2++;
                            Cnt3++;
                            GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
                        }
                        if (Cnt3 > 3f)
                        {
                            for (int i = 0; i <= 2; i++)
                            {
                                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet3(transform.position, 1);
                            }
                            Cnt3 = 0;
                        }
                        if (Cnt2 > 5)
                        {
                            GameObject gmobj = Instantiate(gaugebullet) as GameObject;
                            gmobj.GetComponent<ReverseBullet>().SetPosition(this.transform.position);
                            gmobj.GetComponent<ReverseBullet>().SetGaugeFlag(true);
                            Cnt2 = 0;
                            GetComponent<BossMove>().action = BossMove.MoveAction.AttackMove;
                        }
                        if (GetComponent<BossHp>().GetHp() < GetComponent<BossHp>().GetMaxHp() * 0.2 && patern == Patern.Force)
                        {
                            patern = Patern.Five;
                        }
                        break;
                    }
                case Patern.Five://弾幕三種守り＋レーザーも追加、ついでにシンプルに通常弾幕を増やす
                    {
                        if (Cnt * Time.deltaTime > 1)
                        {
                            audioSource.PlayOneShot(BulletSE);
                            //GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet4(transform.position, 3);
                            // GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 2);
                            for (int i = 0; i < 9; i++)
                            {
                                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet2(transform.position, 2);

                            }
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet5(transform.position, 3);
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet4(transform.position, 1);

                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 1);
                            Cnt = 0;
                            Cnt2++;
                            Cnt3++;
                            GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
                        }
                        if (Cnt3 > 3f)
                        {
                            for (int i = 0; i <= 2; i++)
                            {
                                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet3(transform.position, 1);
                            }
                            Cnt3 = 0;
                        }
                        if (Cnt2 > 5)
                        {
                            GameObject gmobj = Instantiate(gaugebullet) as GameObject;
                            gmobj.GetComponent<ReverseBullet>().SetPosition(this.transform.position);
                            gmobj.GetComponent<ReverseBullet>().SetGaugeFlag(true);
                            Cnt2 = 0;
                            GetComponent<BossMove>().action = BossMove.MoveAction.AttackMove;
                        }
                        break;
                    }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<BossMove>().action == BossMove.MoveAction.AttackMove)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<PlayerControl>().AttackDamage();
            }
            //if (other.gameObject.tag == "Friend")
            //{
            //    other.GetComponent<Enemy>().DeadEnd();
            //}            //if (other.gameObject.tag == "FriendBullet")
            //{
            //    if(other.GetComponent<Rock>! =null)
            //    other.GetComponent<Rock>().AttackBoss();
            //}

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
