using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeelEffect : MonoBehaviour
{
    GameObject parent;
    ParticleSystem effect;
    // Start is called before the first frame update
    void Start()
    {
        effect = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDead();
        if (parent != null)
        {
            transform.position = parent.transform.position;
        }
    }
    void CheckDead()
    {
        if (parent == null)
        {
            Destroy(gameObject);
        }
    }
    void CheckState()
    {
        
    }
    public void SetParent(GameObject _parent)
    {
        parent = _parent;
    }
    public void PlayParticle()
    {
        if (!effect.isPlaying)
        {
            effect.Play();
        }

    }
    public void StopParticle()
    {
        if (effect.isPlaying)
        {
            effect.Stop();
        }
    }
}
