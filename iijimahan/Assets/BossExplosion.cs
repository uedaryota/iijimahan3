using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosion : MonoBehaviour
{
    [SerializeField]private GameObject Explosion;
    private BossHp hp;
    private bool EffectFlag;
    private Vector3 effectScaleSmall = new Vector3(1.0f, 1.0f, 1.0f);
    private Vector3 effectScaleBig = new Vector3(2.5f, 2.5f, 2.5f);
    private Vector3 speed = new Vector3(-0.1f, -0.1f, 0.0f);
    

    void Start()
    {
        hp = gameObject.GetComponent<BossHp>();
        EffectFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp.Hp <= 0 && EffectFlag == false)
        {
            this.tag = "Untagged";
            StartCoroutine("ExplosionEffect");
            EffectFlag = true;
        }
        if(EffectFlag == true)
        {
            transform.position += speed * Time.deltaTime;
        }
    }

    IEnumerator ExplosionEffect()
    {
        GameObject effect = Instantiate(Explosion);
        effect.transform.position = transform.position + new Vector3(0.5f, 0.7f, -2.0f);
        effect.transform.localScale = effectScaleSmall;
        yield return new WaitForSeconds(0.5f);

        GameObject effect2 = Instantiate(Explosion);
        effect2.transform.position = transform.position + new Vector3(-0.5f, -0.7f, -2.0f);
        effect2.transform.localScale = effectScaleSmall;
        yield return new WaitForSeconds(0.5f);

        GameObject effect3 = Instantiate(Explosion);
        effect3.transform.position = transform.position + new Vector3(0.2f, 0.5f, -2.0f);
        effect3.transform.localScale = effectScaleSmall;
        yield return new WaitForSeconds(0.5f);
        
        GameObject effect4 = Instantiate(Explosion);
        effect4.transform.position = transform.position + new Vector3(0.0f, 0.0f, -2.0f);
        effect4.transform.localScale = effectScaleBig;
        yield return new WaitForSeconds(0.1f);

        gameObject.SetActive(false);
    }
}
