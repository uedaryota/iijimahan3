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
        Normal,Action1,End,
    }
    #endregion

    public MoveAction action;
    public bool Xswitch = false;
    public bool Yswitch = false;
    public bool BattleSwitch1 = false;
    public bool BattleSwitch2 = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = this.gameObject.GetComponent<Transform>();
        Vector2 pos = transform.position;
        switch (action)
        {
            case MoveAction.Normal:
                break;
            case MoveAction.Action1:
                if (!BattleSwitch1)
                {
                    if (!Xswitch)
                    {
                        if (pos.x < MaxX)
                        {
                            pos.x += Speed * Move;
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
                            pos.x -= Speed * Move;
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
                            pos.y += Speed * Move;
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
                            pos.y -= Speed * Move;
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

                    action = MoveAction.Normal;
                }
                break;
        }
    }
}
