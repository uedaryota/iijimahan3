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
      //  heelEffect.StopParticle();
    }
    
    private void OnTriggerStay(Collider other)
    {
        bool healStart = false;
        if (other.tag == "EnemyHeal" && gameObject.tag == "Enemy")
        {
            target = other.gameObject;
            healStart = true;
            heelEffect.PlayParticle();
            EnemyState state = GetComponent<EnemyState>();
            float healValue = Time.deltaTime * state.GetPower();
            state.Damage(-healValue);
        }

        else if (other.tag == "FriendHeal" && gameObject.tag == "Friend")
        {
            target = other.gameObject;
            healStart = true;
            heelEffect.PlayParticle();
            EnemyState state = GetComponent<EnemyState>();
            float healValue = Time.deltaTime * state.GetPower();
            state.Damage(-healValue);
        }
        else if (other.tag == "FriendHeal" && gameObject.tag == "Player")
        {
            target = other.gameObject;
            healStart = true;
            heelEffect.PlayParticle(); 
        }

       else if (other.tag == "EnemyHeal" && gameObject.tag == "Player")
        {
            heelEffect.StopParticle();
        }

        else if (other.tag == "FriendHeal" && gameObject.tag == "Enemy")
        {
            heelEffect.StopParticle();
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
        if (other == null)
        {
            heelEffect.StopParticle();
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckTarget();
      //  isPlay = false;
    }
    void CheckTarget()
    {
        if (target == null)
        {
            heelEffect.StopParticle();
        }
    }
    public void SetTarget(GameObject _target)
    {
        target = _target;
    }
}
