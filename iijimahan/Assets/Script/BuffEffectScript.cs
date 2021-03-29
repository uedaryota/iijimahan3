using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffectScript : MonoBehaviour
{
    Enemy enemy;
    ShootEnemy shootEnemy;
    GameObject parent;

    // Vector3 def;

    // Start is called before the first frame update
    void Start()
    {
        enemy = parent.GetComponent<Enemy>();
        shootEnemy = parent.GetComponent<ShootEnemy>();
      //  def = transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 _parent = transform.parent.transform.localRotation.eulerAngles;

        // //修正箇所
        //// transform.localRotation = Quaternion.Euler(def - _parent);
        if(DeadCheck())
        {
            return;
        }
        if(this.gameObject!=null)
        {
            transform.position = parent.transform.position;
            if (shootEnemy != null && shootEnemy.GetBuffLevel() != 0)
            {
                if (!this.gameObject.GetComponent<ParticleSystem>().isPlaying)
                {
                    this.gameObject.GetComponent<ParticleSystem>().Play();

                }

            }

            else if (enemy != null && enemy.GetBuffLevel() != 0)
            {
                if (!this.gameObject.GetComponent<ParticleSystem>().isPlaying)
                {
                    this.gameObject.GetComponent<ParticleSystem>().Play();

                }
            }
            else
            {
                this.gameObject.GetComponent<ParticleSystem>().Stop();
            }

        }

    }
    public void SetParent(GameObject parent)
    {
        this.parent = parent;
    }
    bool DeadCheck()
    {
        if (parent == null) 
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
