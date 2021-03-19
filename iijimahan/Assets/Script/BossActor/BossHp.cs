using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossHp : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("ボス最大体力")] public float MaxHp;
    [Header("無敵時間")] public float Superarmor;
    [Header("ボス点滅インターバル")] public float interval=0.1f;
    [Header("ボスモデル")] public Material bossmodel;
    public enum　Status
    {
        Normal, Damege,
    }
    #endregion

    public Status status;
    public float Hp;//ボス現在体力
    float damege = 1;
    float alpha_Sin=255;
    bool SetRev = false;
    Color _color;
   

    // Start is called before the first frame update
    void Start()
    {
        status = Status.Normal;
        Hp = MaxHp;
        damege = 1;
        alpha_Sin = 255;
        bossmodel.color = Color.green;
        SetRev = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;
        if (Hp==0)
        {
            Dead();
        }
        if(status==Status.Damege)
        {
            Blink();
            Col();
            Invoke("ArmorOff",Superarmor);
        }
    }

    void Dead()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="FriendBullet" &&status ==Status.Normal || other.gameObject.tag == "Friend" && status == Status.Normal)//味方になったエネミーの弾のタグを設定
        {
            //damege = other.gameObject.GetComponent<>();
            Hp = Hp - damege;
            status = Status.Damege;
        }
    }
    public void ArmorOff()
    {
        bossmodel.color=Color.green;
        status = Status.Normal;
    }
   public void Col()
   { 
        //_color.a = alpha_Sin;
        bossmodel.color = Color.black;//_color;     
   }
    IEnumerator Blink()
    {
        while (true)
        {
            var renderComponent = GetComponent<MeshRenderer>();
            renderComponent.enabled = !renderComponent.enabled;
            yield return new WaitForSeconds(interval);
        }
    }
}
