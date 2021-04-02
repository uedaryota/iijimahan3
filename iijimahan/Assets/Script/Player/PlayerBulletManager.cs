using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletManager : MonoBehaviour
{
    private Transform parentTransform;

    public GameObject oya;
    private float counter = 0;

    private bool isdeadFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        parentTransform = GameObject.Find("").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isdeadFlag)
        {
            counter += 60 * Time.deltaTime;
        }

        if(counter>2)
        {
            Destroy(oya);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isdeadFlag = true;
            //Destroy(this.gameObject);

            //UnityEditor.EditorApplication.isPaused = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isdeadFlag = true;
            //Destroy(this.gameObject);
            
            //UnityEditor.EditorApplication.isPaused = true;
        }
    }
    public bool IsDeadCheck()
    {
        return isdeadFlag;
    }
}
