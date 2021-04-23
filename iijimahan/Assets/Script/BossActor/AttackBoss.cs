using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoss : MonoBehaviour
{
    [SerializeField, Header("ボス攻撃SE")] public AudioClip BulletSE;
    private AudioSource audioSource;
    int Cnt = 0;
    int Cnt2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0) return;
        if (GameObject.FindGameObjectWithTag("Boss") == null) Destroy(gameObject);

        if (GetComponent<BossMove>().nowact() == BossMove.MoveAction.Normal)
        {
            GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
        }
        if (GetComponent<BossMove>().nowact() == BossMove.MoveAction.End)
        {
            GetComponent<BossMove>().action = BossMove.MoveAction.Action1;
        }
        if (GetComponent<BossMove>().nowact()==BossMove.MoveAction.Action1)
        {
            Cnt++;
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
            if(Cnt2>10)
            {
                Cnt2 = 0;
                GetComponent<BossMove>().action = BossMove.MoveAction.AttackMove;
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
