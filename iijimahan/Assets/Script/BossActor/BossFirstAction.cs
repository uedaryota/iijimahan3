using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFirstAction : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("目的座標")] public float ComOnX, ComOnY;
    [Header("移動幅")] public float Move;
    [Header("移動スピード")] public float Speed;
    public enum MoveAction
    {
        Normal=0, Action1=1,End=2,
    }
    #endregion
    public MoveAction action;
    public bool BattleSwitch1 = false;
    public bool BattleSwitch2 = false;
    bool End=false;
    float Angle;
    Vector3 point;
    // Start is called before the first frame update
    void Start()
    {
        Transform transform = this.gameObject.GetComponent<Transform>();
        Vector2 pos = transform.position;
        pos.x = transform.position.x;
        pos.y = transform.position.y;
        action = MoveAction.Action1;
        BattleSwitch1 = false;
        BattleSwitch2 = false;
        End = false;
        point.x = ComOnX;
        point.y = ComOnY;
    }

    // Update is called once per frame
    void Update()
    {
        if(End)
        {
            return;
        }
        Transform transform = this.gameObject.GetComponent<Transform>();
        Vector3 pos = transform.position;

        switch (action)
        {
            case MoveAction.Normal:
                break;
            case MoveAction.Action1:
                Vector3 dir = point - pos;
                Angle = Mathf.Atan2(point.y - pos.y,point.x - pos.x);
                transform.position += GetDirectionPI(Angle) * Speed * Time.deltaTime;
                if(point.y - pos.y<0.01f)
                {
                    BattleSwitch1 = true;
                }
                if (point.x - pos.x < 0.01f)
                {
                    BattleSwitch2 = true;
                }
                if (BattleSwitch1 && BattleSwitch2)
                {
                    action = MoveAction.End;
                }
                break;
            case MoveAction.End:
                End = true;
                break;
            
        }
    }
    bool EndCheck()
    {
        return End;
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