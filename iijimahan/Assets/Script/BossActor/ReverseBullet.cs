using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseBullet : MonoBehaviour
{
    private Vector3 velocity;
    public float speed = 5.0f;
    private float timer = 0;
    private bool scaleFlag = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;

        if (timer > 600 && !scaleFlag) Destroy(this.gameObject);
        if (timer > 40 && scaleFlag) Destroy(this.gameObject);

        velocity.Normalize();
        velocity *= speed;
        transform.position += velocity * Time.deltaTime;
        timer += 100 * Time.deltaTime;


        if (scaleFlag)
        {
            float value = 120f;
            transform.localScale += new Vector3(value, value, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (scaleFlag) return;

        if (other.gameObject.tag == "Friend")
        {
            Destroy(this.gameObject);
        }
    }

    public void SetVelocity(Vector3 vec3)
    {
        velocity = vec3;
    }
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public void SetTransform(Vector3 vec3, Vector3 pos)
    {
        velocity = vec3;
        transform.position = pos;
    }
    public void SetGaugeFlag(bool fl)
    {
        scaleFlag = fl;
    }
}
