using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    public float LifeTime;
    public AudioClip sound;
    AudioSource soundplayer;
    // Start is called before the first frame update
    void Start()
    {
        soundplayer = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        LifeTime -= Time.deltaTime;
        if(LifeTime<=0)
        {
            Destroy(gameObject);
        }
    }
    public void HitSE()
    {
        soundplayer = gameObject.GetComponent<AudioSource>();
        if (sound != null) 
        {
            soundplayer.PlayOneShot(sound);
        }
    }
}
