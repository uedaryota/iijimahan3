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
    float x, y;
    private bool deadFlag = false;
    private float Hutosa;
    private LineRenderer line;
    Vector3[] positions;
    Vector3 startpos;
    Vector3 goalpos;
    Vector3 Pulus;
    Vector3 Mainus;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Cnt = 0;
        deadFlag = false;
        Hutosa=0.1f;
        Pulus = new Vector3(20, 20, 0);
        Mainus = new Vector3(20, -20, 0);
        line = gameObject.AddComponent<LineRenderer>();
        startpos= GameObject.FindGameObjectWithTag("Boss").transform.position;
        goalpos = new Vector3(Random.Range(-35,+35), Random.Range(-100, +100), 0);
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

        Cnt++;
            //startpos = GameObject.FindGameObjectWithTag("Boss").transform.position;
            positions = new Vector3[]{
        startpos,               // 開始点
        goalpos,               // 終了点
    };
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.red;
        line.endColor = Color.red;

        if (Cnt/30.0f>Speed)
        {
            Hutosa = 2.0f;
            this.tag = "EnemyBullet";
        } 
        if(Cnt/30.0f>Speed*2)
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
        foreach(var obj in hits)
        {
            if(obj.collider.GetComponent<EnemyState>()!=null)
            {
                obj.collider.GetComponent<EnemyState>().LaserDamage();
            }
         
            if(obj.collider.GetComponent<PlayerControl>()!=null)
            {
                obj.collider.GetComponent<PlayerControl>().LaserDamage();
            }
        }
    }
}
