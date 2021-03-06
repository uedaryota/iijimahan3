using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static Vector2 SineInOut(float t, float totaltime, Vector2 min, Vector2 max)
    {
        max -= min;
        return -max / 2 * (float)(Mathf.Cos(t * Mathf.PI / totaltime) - 1) + min;
    }

}
