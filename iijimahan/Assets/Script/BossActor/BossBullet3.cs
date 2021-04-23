using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet3 : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("弾速※現状いじれないのでスクリプトの中身から弄ってください")] public float Speed = 10.0f;
    [Header("弾角度")] public float Angle;
    float Cnt = 0;
    float x, y;
    private bool deadFlag = false;
    Vector3 pos;
    #endregion
    void Start()
    {
        Cnt = 0;
        pos = GameObject.FindGameObjectWithTag("Player").transform.position;
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
        Vector3 dir = pos - transform.position;
        Angle = Mathf.Atan2(pos.y - transform.position.y, pos.x - transform.position.x);
        x = pos.x - transform.position.x;
        y = pos.y - transform.position.y;
        transform.position += GetDirectionPI(Angle) * Speed * 2 * Time.deltaTime;
        if(Mathf.Abs(x)<0.25&&Mathf.Abs(y)<0.25)
        {
            deadFlag = true;
        }
        if (Cnt * Time.deltaTime > 8)
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
