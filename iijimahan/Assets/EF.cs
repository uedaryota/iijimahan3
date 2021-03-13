using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF : MonoBehaviour
{
    SpriteRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      //  transform.localScale *= Time.deltaTime;

        mr.material.color -= new Color(0.4f * Time.deltaTime, 0.4f * Time.deltaTime, 0.4f * Time.deltaTime, 0.4f * Time.deltaTime);

        if(mr.material.color.a <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
