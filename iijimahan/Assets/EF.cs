using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EF : MonoBehaviour
{
    MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale *= 1.02f;

        mr.material.color -= new Color(0.004f, 0.004f, 0.004f, 0.004f);

        if(mr.material.color.a <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
