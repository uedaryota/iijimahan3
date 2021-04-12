using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletFrind2 : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("弾速※現状いじれないのでスクリプトの中身から弄ってください")] public float Speed = 10.0f;
    [Header("弾角度")] public float Angle;
    [Header("追尾時間※現状いじれないのでスクリプトの中身から弄ってください")] public float OikakeTime = 60;
    float Cnt = 0;
    float x, y;
    private bool deadFlag = false;
    #endregion
    void Start()
    {
        Cnt = 0;

    }
    public void SetParam(float speed, float angle)
    {
        Speed = speed;
        Angle = angle;
    }
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;
        if (deadFlag)
        {
            Destroy(this.gameObject);
        }
        Cnt++;
        if (Cnt < OikakeTime)
        {
            Vector3 pos = GameObject.FindGameObjectWithTag("Friend").transform.position;
            Vector3 dir = pos - transform.position;
            Angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x);
            x = pos.x - transform.position.x;
            y = pos.y - transform.position.y;
            transform.position += GetDirectionPI(Angle) * Speed * Time.deltaTime;
        }
        else
        {
            Vector3 pos = GameObject.FindGameObjectWithTag("Friend").transform.position;
            Vector3 dir = pos - transform.position;
            Angle = Mathf.Atan2(y, x);
            transform.position += GetDirectionPI(Angle) * Speed * Time.deltaTime;
        }

        if (Cnt > 700)
        {
            deadFlag = true;
        }
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Friend")
        {
            deadFlag = true;
        }
    }
}
