using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF : MonoBehaviour
{
    SpriteRenderer mr;
    float alpha = 0;
    float scalex, scaley, scalez;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<SpriteRenderer>();
        scalex = 0;
        scaley = 0;
        scalez = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, alpha);
        if (alpha>=1)
        {
            mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, 2 - alpha);
            alpha += 1 * Time.deltaTime;
            if (alpha >= 2.0f)
            {
                Destroy(this.gameObject);
            }
        }
        alpha += 1 * Time.deltaTime;

        scalex += 1 / 0.5f * Time.deltaTime;
        scaley += 1 / 0.5f * Time.deltaTime;
        scalez += 1 / 0.5f * Time.deltaTime;
        if (scalex >= 1) 
        {
            scalex = 1;
            scaley = 1;
            scalez = 1;
        }
        this.transform.localScale=new Vector3(scalex, scaley, scalez);
       
    }
}
