using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour//自動追尾弾だぜ
{
    #region//インスペクターで設定する
    [Header("弾速※現状いじれないのでスクリプトの中身から弄ってください")] public float　Speed=10.0f;
    [Header("弾角度")] public float Angle;
    [Header("追尾時間※現状いじれないのでスクリプトの中身から弄ってください")] public float OikakeTime=700;
    float Cnt = 0;
    Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
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
        if (deadFlag)
        {
            Destroy(this.gameObject);
        }
        Cnt++;
        if (Cnt < OikakeTime)
        {
            pos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 dir = pos - transform.position;
            Angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x);
            x = pos.x - transform.position.x;
            y = pos.y - transform.position.y;
            transform.position += GetDirectionPI(Angle) * Speed * Time.deltaTime;
        }
        else
        {
            Vector3 dir = pos - transform.position;
            Angle = Mathf.Atan2(y, x);
            transform.position += GetDirectionPI(Angle) * Speed * Time.deltaTime;
        }

        if(Cnt>1400)
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
        if (other.gameObject.tag == "Player")
        {
            deadFlag = true;
        }
    }
}