using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testenemy : MonoBehaviour
{
    private Vector3 velocity;
    public float speed = 0.1f;
    
    void Update()
    {
        velocity -= new Vector3(speed, 0, 0);
        transform.position += velocity * Time.deltaTime;

        if(velocity.x <= -40.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
