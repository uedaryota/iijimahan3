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
        if (other.tag == "EnemyHeal" && gameObject.tag == "Enemy") 
        {
            heelEffect.PlayParticle();
            EnemyState state = GetComponent<EnemyState>();
            float healValue = Time.deltaTime * state.GetPower();
            state.Damage(-healValue);
            Debug.Log(healValue);
        }
        if (other.tag == "FriendHeal" && gameObject.tag == "Friend") 
        {
            heelEffect.PlayParticle();
            EnemyState state = GetComponent<EnemyState>();
            float healValue = Time.deltaTime * state.GetPower();
            state.Damage(-healValue);
            Debug.Log(healValue);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyHeal")
        {
            heelEffect.StopParticle();
        }
        if (other.tag == "FriendHeal") 
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
