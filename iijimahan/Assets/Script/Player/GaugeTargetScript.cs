using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeTargetScript : MonoBehaviour
{
    [SerializeField]
    public Image GreenGauge;

    public GameObject gaugeEffect;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "GaugeEnergy")
        {
            GameObject effect = Instantiate(gaugeEffect);
            effect.transform.position = transform.position + new Vector3(0,-1,0);
            effect.transform.localScale = new Vector3(2, 6, 2);
            //effect.GetComponent<PlayerBurstScript>().SetModeFlag(true);
        }
    }
}
