using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("猶予時間")] public float Speed = 10.0f;
    [Header("弾角度")] public float Angle;
    [Header("継続攻撃時間")] public float LimitTime = 700;
    float Cnt = 0;
    Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
    float x, y;
    private bool deadFlag = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Cnt = 0;
        deadFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;
        Cnt++;
        if(Cnt>Speed)
        {
            this.tag = "EnemyBullet";
        }

    }
}
