using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRotate : MonoBehaviour
{
    Quaternion a;
    float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        z += 0.01f;
        transform.rotation.Set(x, y, z, 0);
    }
}
