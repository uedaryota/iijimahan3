using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierControl : MonoBehaviour
{
    // Start is called before the first frame update

    private float alpha = 0.0f;

    //public SpriteRenderer sr;

    private float counter = 0;

    public Material mate;

    private GameObject boss;

    public SpriteRenderer sr;

    void Start()
    {
        mate.color = new Color(1, 1, 0, 1);
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime * 60;

        if (counter > 20)
        {
            alpha += Time.deltaTime * 2f;
            sr.color = sr.color - new Color(0, 0, 0, alpha);
        }

        if (counter > 30)
        {
            Destroy(this.gameObject);
        }

        if(boss != null)
        {
            transform.position = boss.transform.position;
        }
    }

    public void SetRotation(Vector3 rotation)
    {
        transform.localEulerAngles = rotation;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
