using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("弾速")] public float　Speed = 3.0f;
    [Header("弾角度")] public float Angle = 0.0f;
    #endregion
    void Start()
    {

    }
    public void SetParam(float speed, float angle)
    {
        Speed = speed;
        Angle = angle;
    }
    void Update()
    {
        Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 dir = pos - transform.position;
        Angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x);
        print(Angle);
        transform.position += GetDirectionPI(Angle) * Speed * Time.deltaTime;

        // 弾が進行方向を向くようにする
        var angles = transform.localEulerAngles;
        angles.z = Angle * Mathf.Rad2Deg - 90;
        transform.localEulerAngles = angles;
    }
    //角度をベクトルに変換する
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