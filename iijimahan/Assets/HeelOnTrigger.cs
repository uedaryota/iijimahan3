using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeelOnTrigger : MonoBehaviour
{
    GameObject target;
    GameObject heelObject;
    HeelEffect heelEffect;
    // Start is called before the first frame update
    void Start()
    {
        heelObject = Instantiate(Resources.Load<GameObject>("HeelEffect"));
        heelEffect = heelObject.GetComponent<HeelEffect>();
        heelEffect.SetParent(gameObject);
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Heel")
        {
            heelEffect.PlayParticle();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Heel")
        {
            heelEffect.StopParticle();
        }
    }
    // Update is called once per frame
    void Update()
    {
    
      //  isPlay = false;
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
    }
}
