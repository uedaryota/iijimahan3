using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBarrerEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject effect;

    public GameObject bullet;

    public GameObject barrier;

    public GameObject target;

    private GameObject player;

    public GameObject barrierPosOBJ;


    [SerializeField, Header("爆発エフェクト")] private Object BurstEffect;
    [SerializeField, Header("破片エフェクト1")] private Object RockEffect1;
    [SerializeField, Header("破片エフェクト2")] private Object RockEffect2;
    public Vector3 rockeffectpos = new Vector3(0, 0, -2);
    private Vector3 effectpos;


    public SpriteRenderer sr;

    Vector3 rote;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {


        //if (other.gameObject.tag == "Boss")
        //{
        //    sr.color = new Color(1, 1, 1, 0);
        //    rote = bullet.GetComponent<BulletControl>().GetRote();
        //    if (!player.GetComponent<PlayerControl>().GetBossBarrierFlag())
        //    {
        //        //UnityEditor.EditorApplication.isPaused = true;
        //        GameObject barriers = Instantiate(barrier) as GameObject;
        //        barriers.GetComponent<BarrierControl>().SetRotation(new Vector3(rote.x, rote.y, rote.z - 180));
        //        barriers.GetComponent<BarrierControl>().SetPosition(new Vector3(-500, -500, -500));
        //        player.GetComponent<PlayerControl>().SetBossBarrierFlag(true);
        //    }

        //    GameObject paticles = Instantiate(effect) as GameObject;
        //    paticles.transform.position = barrierPosOBJ.transform.position;
        //    paticles.transform.localEulerAngles = new Vector3(rote.z - 180, -90, 90);
        //    Destroy(bullet);

        //}
        if (other.gameObject.tag == "Shield")
        {
            if (other.transform.parent.tag == "Enemy")
            {
                Destroy(bullet);
            }
        }
        if (other.gameObject.tag == "Rock")
        {


            Instantiate(RockEffect1, transform.position + effectpos, Quaternion.identity);
            Instantiate(BurstEffect, transform.position + rockeffectpos, Quaternion.identity);
            Instantiate(RockEffect2, transform.position + rockeffectpos, Quaternion.identity);
            Destroy(bullet);
            //UnityEditor.EditorApplication.isPaused = true;
        }




    }
}
