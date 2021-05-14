using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("移動制限")] public float MinX, MaxX, MinY, MaxY;
    [Header("移動幅")] public float Move;
    [Header("移動スピード")] public float Speed;
    public enum MoveAction
    {
        Normal,Action1,End,AttackMove,
    }
    #endregion

    public MoveAction action;
    public bool Xswitch = false;
    public bool Yswitch = false;
    public bool BattleSwitch1 = false;
    public bool BattleSwitch2 = false;
    Vector3 movpoi1, movpoi2, movpoi3, movpoi4, movpoi5,movpoi6;
    public float Angle;
    float x, y;
    int Lesson=1;
    private BossHp hp;
    // Start is called before the first frame update
    void Start()
    {
        Transform transform = this.gameObject.GetComponent<Transform>();
        Vector2 pos = transform.position;
        pos.x = transform.position.x;
        pos.y = transform.position.y;
        action = MoveAction.Normal;
        Xswitch = false;
        Yswitch = false;
        BattleSwitch1 = false;
        BattleSwitch2 = false;
        movpoi1 = new Vector2(20, 0);
        movpoi2 = new Vector2(20, 5);
        movpoi3 = new Vector2(-20, 5);
        movpoi4 = new Vector2(-20, -5);
        movpoi5 = new Vector2(20, -5);
        movpoi6 = new Vector2(13, 0);
        Lesson = 1;
        hp = gameObject.GetComponent<BossHp>();
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;
        if (hp.Hp <= 0) return;
        Transform transform = this.gameObject.GetComponent<Transform>();
        Vector2 pos = transform.position;
        switch (action)
        {
            case MoveAction.Normal:
                break;
            case MoveAction.Action1:
                {
                    if (!BattleSwitch1)
                    {
                        if (!Xswitch)
                        {
                            if (pos.x < MaxX)
                            {
                                pos.x += Speed/10 * Move;
                            }
                            else
                            {
                                Xswitch = true;
                            }
                        }
                        if (Xswitch)
                        {
                            if (pos.x > MinX)
                            {
                                pos.x -= Speed/10 * Move;
                            }
                            else
                            {
                                BattleSwitch1 = true;
                            }
                        }
                    }
                    if (!BattleSwitch2)
                    {
                        if (!Yswitch)
                        {
                            if (pos.y < MaxY)
                            {
                                pos.y += Speed/10 * Move;
                            }
                            else
                            {
                                Yswitch = true;
                            }
                        }
                        if (Yswitch)
                        {
                            if (pos.y > MinY)
                            {
                                pos.y -= Speed/10 * Move;
                            }
                            else
                            {
                                BattleSwitch2 = true;
                            }
                        }
                    }
                    transform.position = pos;
                    if (BattleSwitch1 && BattleSwitch2)
                    {
                        BattleSwitch1 = false;
                        BattleSwitch2 = false;
                        Xswitch = false;
                        Yswitch = false;
                        action = MoveAction.End;
                    }
                    break;
                }
            case MoveAction.AttackMove:
                {
                    if (Lesson == 1)
                    {
                        Vector3 dir = movpoi1 - transform.position;
                        Angle = Mathf.Atan2(movpoi1.y - transform.position.y, movpoi1.x - transform.position.x);
                        x = movpoi1.x - transform.position.x;
                        y = movpoi1.y - transform.position.y;
                        transform.position += GetDirectionPI(Angle) * Speed * 10 * Time.deltaTime;
                        if(x<0.3f&&y<0.3f)
                        {
                            Lesson = 2;
                        }
                    }
                    if (Lesson == 2)
                    {
                        Vector3 dir = movpoi2 - transform.position;
                        Angle = Mathf.Atan2(movpoi2.y - transform.position.y, movpoi2.x - transform.position.x);
                        x = movpoi2.x - transform.position.x;
                        y = movpoi2.y - transform.position.y;
                        transform.position += GetDirectionPI(Angle) * Speed * 10 * Time.deltaTime;
                        if (x < 0.3f && y < 0.3f)
                        {
                            Lesson = 3;
                        }
                    }
                    if (Lesson == 3)
                    {
                        Vector3 dir = movpoi3 - transform.position;
                        Angle = Mathf.Atan2(movpoi3.y - transform.position.y, movpoi3.x - transform.position.x);
                        x = movpoi3.x - transform.position.x;
                        y = movpoi3.y - transform.position.y;
                        transform.position += GetDirectionPI(Angle) * Speed * 10 * Time.deltaTime;
                        if (Mathf.Abs(x) < 0.3f && Mathf.Abs(y) < 0.3f)
                        {
                            Lesson = 4;
                        }
                    }
                    if (Lesson == 4)
                    {
                        Vector3 dir = movpoi4 - transform.position;
                        Angle = Mathf.Atan2(movpoi4.y - transform.position.y, movpoi4.x - transform.position.x);
                        x = movpoi4.x - transform.position.x;
                        y = movpoi4.y - transform.position.y;
                        transform.position += GetDirectionPI(Angle) * Speed * 10 * Time.deltaTime;
                        if (Mathf.Abs(x) < 0.3f && Mathf.Abs(y) < 0.3f)
                        {
                            Lesson = 5;
                        }
                    }
                    if (Lesson == 5)
                    {
                        Vector3 dir = movpoi5 - transform.position;
                        Angle = Mathf.Atan2(movpoi5.y - transform.position.y, movpoi5.x - transform.position.x);
                        x = movpoi5.x - transform.position.x;
                        y = movpoi5.y - transform.position.y;
                        transform.position += GetDirectionPI(Angle) * Speed * 10 * Time.deltaTime;
                        if (Mathf.Abs(x) < 0.3f && Mathf.Abs(y) < 0.3f)
                        {
                            Lesson = 6;
                        }
                    }
                    if (Lesson == 6)
                    {
                        Vector3 dir = movpoi6 - transform.position;
                        Angle = Mathf.Atan2(movpoi6.y - transform.position.y, movpoi6.x - transform.position.x);
                        x = movpoi6.x - transform.position.x;
                        y = movpoi6.y - transform.position.y;
                        transform.position += GetDirectionPI(Angle) * Speed * 10 * Time.deltaTime;
                        if (Mathf.Abs(x) < 0.3f && Mathf.Abs(y) < 0.3f)
                        {
                            action = MoveAction.Action1;
                            Lesson = 1;
                        }
                    }
                    break;
                }
        }
    }
    public MoveAction nowact()
    {
        return action;
    }
    Vector3 GetDirectionPI(float angle)
    {
        return new Vector3
        (
            Mathf.Cos(angle),
            Mathf.Sin(angle),
            0
        );
    }
}
