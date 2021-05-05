using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRock : MonoBehaviour
{
    [SerializeField, Header("エフェクト隕石の角度")] private float angle = 55;
    [SerializeField, Header("速度")] private float speed = 10.0f;
    [SerializeField, Header("消えるまでの時間")]public float interval = 0.5f;
    private Object OriginalRock;
    private Vector3 Originalpos;
    private Vector3 velocity;
    private float timer;
    
    void Start()
    {
        Originalpos = serchTag(gameObject, "Rock").GetComponent<Transform>().position;
        velocity = Vector3.Normalize(Originalpos - transform.position);
        velocity = -velocity;
        velocity.z = 0.0f;
        if (transform.name == "EffectRock1(Clone)")
        {
            velocity = Quaternion.Euler(0, 0, angle) * velocity;
        }
        if (transform.name == "EffectRock2(Clone)")
        {
            velocity = Quaternion.Euler(0, 0, -angle) * velocity;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= interval)
        {
            Destroy(gameObject);
        }
        transform.position += speed * velocity * Time.deltaTime;
    }

    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0; //距離用一時変数
        float nearDis = 0; //最も近いオブジェクトの距離
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach(GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if(nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                targetObj = obs;
            }
        }
        //最も近かったオブジェクトを返す
        return targetObj;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Rock")
        {
            OriginalRock = other;
        }
    }
}
