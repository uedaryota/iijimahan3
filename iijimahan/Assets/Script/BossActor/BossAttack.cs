using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Acter
{
    Start,Attack,
    End,
}
public class BossAttack : MonoBehaviour
{
    int Cnt = 0;
    int Cnt2=0;
    Acter act;
    // Start is called before the first frame update
    void Start()
    {
        act = Acter.Start;
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;
        switch (act)
        {
            case Acter.Start:
                if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossFirstAction>().action==BossFirstAction.MoveAction.End)
                {
                    act = Acter.Attack;
                }
                break;
            case Acter.Attack:
                if (Cnt == 200)
                {
                    for (int x = 0; x < 10; x++)
                    {
                        GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet2(transform.position, 0);
                    }
                    Cnt = 0;
                    Cnt2++;
                    if (Cnt2 == 5)
                    {
                        GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBulletManager>().FBulletFactory[0].CreateBullet(transform.position, 1);
                        Cnt2 = 0;
                    }
                }
                Cnt++;
                break;
        }
    }

}
