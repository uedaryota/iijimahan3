using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
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
    public bool Chage = false;
    public bool Chage0 = false;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        Cnt = 0;
        deadFlag = false;
        Chage = true;
        Chage0 = true;
        Hutosa=0.1f;
        Pulus = new Vector3(20, 20, 0);
        Mainus = new Vector3(20, -20, 0);
        line = gameObject.AddComponent<LineRenderer>();
        startpos= GameObject.FindGameObjectWithTag("Boss").transform.position;
        goalpos = new Vector3(Random.Range(-100,+100), Random.Range(-100, +100), 0);
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
        line.material.color = Color.blue;
        line.sortingOrder = -1;
        if (Cnt / 30.0f < Speed)
        {
            if(Chage0)
            {
                if (GameObject.FindGameObjectWithTag("Boss").GetComponent<LaserBoss2>() != null)
                {
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<LaserBoss2>().ChargeStart();
                }
                if (GameObject.FindGameObjectWithTag("Boss").GetComponent<LaserBoss>() != null)
                {
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<LaserBoss>().ChargeStart();
                }
                Chage0 = false;
            }
            Angle = Mathf.Atan2(startpos.y - goalpos.y, startpos.x - goalpos.x);
            goalpos += GetDirectionPI(Angle) * 25.0f * Time.deltaTime;
        }
        if (Cnt/30.0f>Speed)
        {
            line.material= new Material(Resources.Load<Material>("Raser"));
            if(Chage)
            {
                if (GameObject.FindGameObjectWithTag("Boss").GetComponent<LaserBoss2>() != null)
                {
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<LaserBoss2>().ChargeFinish();
                }
                if (GameObject.FindGameObjectWithTag("Boss").GetComponent<LaserBoss>() != null)
                {
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<LaserBoss>().ChargeFinish();
                }
                Chage = false;
            }
            line.material.color = new Color(0,1,1,0.3f);
            goalpos = goalpos2;
            Hutosa = 2.0f;
            this.tag = "EnemyBullet";
        } 
        if(Cnt/30.0f>Speed+5.0f)
        {
            Destroy(gameObject);
        }
        line.startWidth = Hutosa;                   // 開始点の太さを0.1にする
        line.endWidth = Hutosa;
        // 終了点の太さを0.1にする
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
    Vector3 GetDirectionPI(float angle)
    {
        return new Vector3
        (
            Mathf.Cos(angle),
            Mathf.Sin(angle),
            0
        );
    }
    public bool CHAGE()
    {
        return Chage;
    }
}
