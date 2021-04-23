﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSuperLaser : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("猶予時間")] public float Speed = 10.0f;
    [Header("弾角度")] public float Angle;
    [Header("継続攻撃時間")] public float LimitTime = 700;
    public Material Line;
    float Cnt = 0;
    float x, y;
    private bool deadFlag = false;
    private float Hutosa;
    private LineRenderer line;
    Vector3[] positions;
    Vector3 startpos;
    Vector3 goalpos;
    Vector3 goalpos2;
    Vector3 Pulus;
    Vector3 Mainus;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Cnt = 0;
        deadFlag = false;
        Hutosa = 0.1f;
        Pulus = new Vector3(20, 20, 0);
        Mainus = new Vector3(20, -20, 0);
        line = gameObject.AddComponent<LineRenderer>();
        startpos = GameObject.FindGameObjectWithTag("Boss").transform.position;
        goalpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (goalpos.x > 0)
        {
            goalpos.x = goalpos.x + 10;
        }
        else
        {
            goalpos.x = goalpos.x - 10;
        }
        if (goalpos.y > 0)
        {
            goalpos.y = goalpos.y + 10;
        }
        else
        {
            goalpos.y = goalpos.y - 10;
        }
        goalpos2 = goalpos;
        positions = new Vector3[]{
        startpos,               // 開始点
        new Vector3(20, 0, 0),               // 終了点
    };
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;
        if (GameObject.FindGameObjectWithTag("Boss") == null) Destroy(gameObject);

        Cnt++;
        //startpos = GameObject.FindGameObjectWithTag("Boss").transform.position;
        positions = new Vector3[]{
        startpos,               // 開始点
        goalpos,               // 終了点
    };
        startpos = GameObject.FindGameObjectWithTag("Boss").transform.position;
        line.textureMode = LineTextureMode.Tile;
        line.generateLightingData = true;
        line.material = new Material(Resources.Load<Material>("Unlit_DashLine"));//new Material(Shader.Find("Resources/Materials/Unlit_DashLine"));//Sprites/Default"));
        line.material.color = Color.yellow;
        line.startColor = Color.yellow;
        line.endColor = Color.red;
        if (Cnt / 30.0f < 8.0f)
        {
            goalpos = GameObject.FindGameObjectWithTag("Player").transform.position;
            if (goalpos.x > 0)
            {
                goalpos.x = goalpos.x + 10;
            }
            else if(goalpos.x < 1 && goalpos.x > -1)
            {
                goalpos.x = 0;
            }
            else
            {
                goalpos.x = goalpos.x - 10;
            }
            if (goalpos.y > 0)
            {
                goalpos.y = goalpos.y + 10;
            }
            else if(goalpos.y<1&& goalpos.y >-1)
            {
                goalpos.y = 0;
            }
            else
            {
                goalpos.y = goalpos.y - 10;
            }
            goalpos2 = goalpos;
        }
        if (Cnt / 30.0f < Speed)
        {
            Angle = Mathf.Atan2(startpos.y - goalpos.y, startpos.x - goalpos.x);
            goalpos += GetDirectionPI(Angle) * 25.0f * Time.deltaTime;
        }
        if (Cnt / 30.0f > Speed)
        {
            line.material = new Material(Resources.Load<Material>("Def"));

            line.material.color = Color.red;
            goalpos = goalpos2;
            Hutosa = 5.0f;
            this.tag = "EnemyBullet";
        }
        if (Cnt / 30.0f > Speed + 5.0f)
        {
            Destroy(gameObject);
        }
        line.startWidth = Hutosa;                   // 開始点の太さを0.1にする
        line.endWidth = Hutosa;                     // 終了点の太さを0.1にする

        line.SetPositions(positions);
        if (this.tag == "EnemyBullet")
        {
            Ray();
        }

    }
    void Ray()
    {
        Ray ray = new Ray(startpos, goalpos);
        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
        foreach (var obj in hits)
        {
            if (obj.collider.GetComponent<EnemyState>() != null)
            {
                obj.collider.GetComponent<EnemyState>().LaserDamage();
            }

            if (obj.collider.GetComponent<PlayerControl>() != null)
            {
                obj.collider.GetComponent<PlayerControl>().LaserDamage();
            }
        }
        Ray ray2 = new Ray(startpos, new Vector3(goalpos.x, goalpos.y+20, goalpos.z));
        RaycastHit[] hits2 = Physics.RaycastAll(ray2, Mathf.Infinity);
        foreach (var obj in hits2)
        {
            if (obj.collider.GetComponent<EnemyState>() != null)
            {
                obj.collider.GetComponent<EnemyState>().LaserDamage();
            }

            if (obj.collider.GetComponent<PlayerControl>() != null)
            {
                obj.collider.GetComponent<PlayerControl>().LaserDamage();
            }
        }
        Ray ray3 = new Ray(startpos, new Vector3(goalpos.x, goalpos.y - 20, goalpos.z));
        RaycastHit[] hits3 = Physics.RaycastAll(ray3, Mathf.Infinity);
        foreach (var obj in hits)
        {
            if (obj.collider.GetComponent<EnemyState>() != null)
            {
                obj.collider.GetComponent<EnemyState>().LaserDamage();
            }

            if (obj.collider.GetComponent<PlayerControl>() != null)
            {
                obj.collider.GetComponent<PlayerControl>().LaserDamage();
            }
        }
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
