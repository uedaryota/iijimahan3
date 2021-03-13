using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet2 : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("弾速※現状いじれないのでスクリプトの中身から弄ってください")] public float Speed = 6.0f;
    [Header("弾角度")] public float Angle;
    float Cnt = 0;
    Vector3 pos;
    float x, y;
    #endregion

    private bool deadFlag = false;
    void Start()
    {
        Cnt = 0;
        y = Random.Range(-150.0f, 150.0f);
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
        Cnt++;
        if (deadFlag)
        {
            Destroy(this.gameObject);
        }
        Vector3 dir = pos - transform.position;
            Angle = Mathf.Atan2(y,pos.x--);
            transform.position += GetDirectionPI(Angle) * Speed * Time.deltaTime;
        if(Cnt>1400)
        {
            deadFlag = true;
        }
      
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