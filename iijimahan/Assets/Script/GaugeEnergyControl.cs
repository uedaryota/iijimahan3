using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeEnergyControl : MonoBehaviour
{
    private int easingCount = 0;
    private bool easingFlag = false;
    public int num = 80;
    public float maxSpeed = 15;
    private float speed = 0.0f;
    private Vector3 playerPos;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        velocity = Move();
        transform.position += velocity * Time.deltaTime;

        easingCount++;
    }

    public Vector3 Move()
    {
        float x = this.transform.position.x - playerPos.x;
        float y = this.transform.position.y - playerPos.y;

        Vector3 v = new Vector3(-x, -y, 0);
        v.Normalize();

        return v;
    }
}
