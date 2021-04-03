using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private Vector3 velocity;
    public float speed = 30.0f;
    private int timer = 0;
    private bool scaleFlag = false;
    [SerializeField, Header("爆発エフェクト")] private Object effect;
    // Start is called before the first frame update
    void Start()
    {

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


        if (timer > 60 && !scaleFlag) Destroy(this.gameObject);
        if(timer > 80 && scaleFlag) Destroy(this.gameObject);

        velocity.Normalize();
        velocity *= speed;
        transform.position += velocity * Time.deltaTime;
        timer++;


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
        if (other.gameObject.tag == "Rock")
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //UnityEditor.EditorApplication.isPaused = true;
        }
        //if (other.gameObject.tag == "EnemyBullet")
        //{
        //    Destroy(this.gameObject);
        //    Debug.Log("エネミーの弾と当たった");
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
    }

}
