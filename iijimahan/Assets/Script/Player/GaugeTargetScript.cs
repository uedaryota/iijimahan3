using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeTargetScript : MonoBehaviour
{
    [SerializeField]
    public Image GreenGauge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pos = 0;
        pos = GreenGauge.fillAmount * 8f - 5;
        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
    }
}
