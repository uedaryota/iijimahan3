using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffectScript : MonoBehaviour
{
    EnemyState enemy;
    GameObject parent;

    // Vector3 def;

    // Start is called before the first frame update
    void Start()
    {
        enemy = parent.GetComponent<EnemyState>();

        //  def = transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 _parent = transform.parent.transform.localRotation.eulerAngles;

        // //修正箇所
        //// transform.localRotation = Quaternion.Euler(def - _parent);
        if (DeadCheck())
        {
            return;
        }
        if (this.gameObject != null)
        {
            transform.position = parent.transform.position;
            if (enemy != null && enemy.GetBuffLevel() != 0)
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
    public void ChangeLevel1()
    {
        this.gameObject.GetComponent<ParticleSystem>().startColor = Color.blue;
    }
    public void ChangeLevel2()
    {

        this.gameObject.GetComponent<ParticleSystem>().startColor = Color.cyan;
    }
    public void ChangeLevel3()
    {
        this.gameObject.GetComponent<ParticleSystem>().startColor = Color.white;
    }
}
