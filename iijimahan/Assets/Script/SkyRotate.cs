using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotate : MonoBehaviour
{
    float skyrotate;
    public Material skybox;
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        skyrotate = Mathf.Repeat(skybox.GetFloat("_Rotation") + rotateSpeed, 360);
        skybox.SetFloat("_Rotation",skyrotate);
    }
}
