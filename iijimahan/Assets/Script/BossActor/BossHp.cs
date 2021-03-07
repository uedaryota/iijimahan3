using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossHp : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("ボス最大体力")] public float MaxHp;
    [Header("無敵時間")] public float Superarmor;
    public enum　Status
    {
        Normal, Damege,
    }
    #endregion

    public Status status;
    public float Hp;//ボス現在体力
    float damege = 0;
    float alpha_Sin=255;
    bool SetRev = false;
    Color _color;

    // Start is called before the first frame update
    void Start()
    {
        status = Status.Normal;
        Hp = MaxHp;
        damege = 0;
        alpha_Sin = 255;
        _color = this.gameObject.GetComponent<Renderer>().material.color;
        SetRev = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Hp==0)
        {
            Dead();
        }
        if(status==Status.Damege)
        {
            if (!SetRev)
            {
                alpha_Sin--;
                if(alpha_Sin<1)
                {
                    SetRev = true;
                }
            }
            else
            {
                alpha_Sin++;
                if(alpha_Sin>254)
                {
                    SetRev = false;
                }
            }
            Invoke("ArmorOff",Superarmor);
            Col();
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }
    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag=="EnemyBullet" &&status==0)//味方になったエネミーの弾のタグを設定
        {
            //damege = other.gameObject.GetComponent<>();
            Hp = Hp - damege;
            status = Status.Damege;
        }
    }
    public void ArmorOff()
    {
        this.gameObject.GetComponent<Renderer>().material.color = _color;
        status = Status.Normal;
    }
   public void Col()
   { 
        //_color.a = alpha_Sin;
        this.gameObject.GetComponent<Renderer>().material.color = Color.black;//_color;     
   }
}
