using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Patern
{First,Second,Therd,Force,Five}
enum LaserUpDown
{
    Up,Normal,Down,
}

public class AttackBoss : MonoBehaviour
{
    [SerializeField, Header("ボス攻撃SE")] public AudioClip BulletSE;
    [SerializeField, Header("反転の弾")]
    public GameObject gaugebullet;
    Patern patern;
    LaserUpDown updown;
    private AudioSource audioSource;
    int Cnt = 0;
    int Cnt2 = 0;
    private BossHp hp;
    // Start is called before the first frame update
   
   
    void Start()
    {
        hp = gameObject.GetComponent<BossHp>();
        audioSource = GetComponent<AudioSource>();
        patern = Patern.First;
        updown = LaserUpDown.Up;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.timeScale <= 0) return;
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
                    for (int i = 0; i < 3; i++)
                    {
                        GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet5(transform.position, 3);
                        GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet2(transform.position, 2);
                    }
                    Cnt = 0;
                    Cnt2++;
                    GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
                }
                if (Cnt2 > 10)
                {        
                    GameObject gmobj = Instantiate(gaugebullet) as GameObject;
                    gmobj.GetComponent<ReverseBullet>().SetPosition(this.transform.position);
                    gmobj.GetComponent<ReverseBullet>().SetGaugeFlag(true);
                    Cnt2 = 0;
                    GetComponent<BossMove>().action = BossMove.MoveAction.AttackMove;
                    Debug.Log("Reverse");
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
                            //GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet4(transform.position, 3);
                            // GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 2);
                            for (int i = 0; i < 1; i++)
                            {
                                GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet5(transform.position, 3);
                                switch(updown)
                                {
                                    case LaserUpDown.Up:
                                        GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet7Up(transform.position, 2);
                                        updown = LaserUpDown.Normal;
                                        break;
                                    case LaserUpDown.Normal:
                                        GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet7(transform.position, 2);
                                        updown = LaserUpDown.Down;
                                        break;
                                    case LaserUpDown.Down:
                                        GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet7Down(transform.position, 2);
                                        updown = LaserUpDown.Up;
                                        break;
                                }     
                            }
                            Cnt = 0;
                            Cnt2++;
                            GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
                        }
                        if (Cnt2 > 5)
                        {
                            Cnt2 = 0;
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
                        if (Cnt2 > 10)
                        {
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
                            switch (updown)
                            {
                                case LaserUpDown.Up:
                                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet7Up(transform.position, 2);
                                    updown = LaserUpDown.Normal;
                                    break;
                                case LaserUpDown.Normal:
                                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet7(transform.position, 2);
                                    updown = LaserUpDown.Down;
                                    break;
                                case LaserUpDown.Down:
                                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet7Down(transform.position, 2);
                                    updown = LaserUpDown.Up;
                                    break;
                            }
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 1);
                            Cnt = 0;
                            Cnt2++;
                            GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
                        }
                        if (Cnt2 > 10)
                        {
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
                            switch (updown)
                            {
                                case LaserUpDown.Up:
                                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet7Up(transform.position, 2);
                                    updown = LaserUpDown.Normal;
                                    break;
                                case LaserUpDown.Normal:
                                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet7(transform.position, 2);
                                    updown = LaserUpDown.Down;
                                    break;
                                case LaserUpDown.Down:
                                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet7Down(transform.position, 2);
                                    updown = LaserUpDown.Up;
                                    break;
                            }
                            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 1);
                            Cnt = 0;
                            Cnt2++;
                            GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
                        }
                        if (Cnt2 > 10)
                        {
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
            if (other.gameObject.tag == "Friend")
            {
                other.GetComponent<Enemy>().DeadEnd();
            }
            if (other.gameObject.tag == "FriendBullet")
            {
                other.GetComponent<Rock>().AttackBoss();
            }
        }
    }
}
