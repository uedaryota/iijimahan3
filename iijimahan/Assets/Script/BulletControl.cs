using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private Vector3 velocity;
    public float speed = 30.0f;
    private float timer = 0;
    private bool scaleFlag = false;
    [SerializeField, Header("爆発エフェクト")] private Object BurstEffect;
    [SerializeField, Header("破片エフェクト1")] private Object RockEffect1;
    [SerializeField, Header("破片エフェクト2")] private Object RockEffect2;
    public Vector3 rockeffectpos = new Vector3(0, 0, -2);
    private Vector3 effectpos;

    public GameObject barrierTarget;
    public GameObject barrierEffect;
    private Vector3 rote;

    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        effectpos = new Vector3(0, 0, -3);
    }
    // Update is called once per frame
    void Update()
    {

        //ポーズの時に止める
        if (Time.timeScale <= 0) return;


        //Collider[] c = Physics.OverlapSphere(this.transform.position, 1, 1 << 8);
        Collider[] c = Physics.OverlapBox(this.transform.position, new Vector3(0.5f, 0.5f, 0.5f));

        //for(int i = 0; i< c.Length;i++)
        //{
        //    if(c[i].gameObject.tag == "Enemy")
        //    {
        //        UnityEditor.EditorApplication.isPaused = true;
        //    }
        //}


        if (timer > 1 && !scaleFlag) Destroy(this.gameObject);
        if(timer > 1.4 && scaleFlag) Destroy(this.gameObject);

        velocity.Normalize();
        velocity *= speed;
        transform.position += velocity * Time.deltaTime;
        timer += 1*Time.deltaTime;


        if(scaleFlag)
        {
            float value = 120f;
            transform.localScale += new Vector3(value, value, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (scaleFlag) return;

        //if (other.gameObject.tag == "Enemy")
        //{
        //    Destroy(this.gameObject);
        //    //UnityEditor.EditorApplication.isPaused = true;
        //}

        //if(other.gameObject.tag == "Boss")
        //{
        //    //GameObject effect = Instantiate(barrierEffect) as GameObject;
        //    //effect.GetComponent<BarrierControl>().SetRotation(new Vector3(rote.x, rote.y, rote.z - 180));
        //    //effect.GetComponent<BarrierControl>().SetPosition(transform.position);
        //    //GameObject paticles = Instantiate(particle) as GameObject;
        //    //paticles.transform.position = transform.position;
        //    //paticles.transform.localEulerAngles = new Vector3(rote.x, rote.y, rote.z - 180);
        //    //Destroy(this.gameObject);

        //}

        //if (other.gameObject.tag == "Rock" )
        //{


        //    Instantiate(RockEffect1, transform.position + effectpos, Quaternion.identity);
        //    Instantiate(BurstEffect, transform.position + rockeffectpos, Quaternion.identity);
        //    Instantiate(RockEffect2, transform.position + rockeffectpos, Quaternion.identity);
        //    Destroy(this.gameObject);
        //    //UnityEditor.EditorApplication.isPaused = true;
        //}

        //if (other.gameObject.tag == "EnemyBullet")
        //{
        //    Destroy(this.gameObject);
        //    
        //("エネミーの弾と当たった");
        //}

    }

    public void SetVelocity(Vector3 vec3)
    {
        velocity = vec3;
    }
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public void SetTransform(Vector3 vec3, Vector3 pos)
    {
        velocity = vec3;
        transform.position = pos;
    }
    public void SetGaugeFlag(bool fl)
    {
        scaleFlag = fl;
    }

    public void SetRotation(Vector3 rotaion)
    {
        transform.localEulerAngles = rotaion;
        rote = rotaion;
    }

    public Vector3 GetRote()
    {
        return rote;
    }

}
