using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    public float LifeTime;
    public AudioClip sound;
    AudioSource soundplayer;
    private GameObject option;
    private Option optionscript;
    // Start is called before the first frame update
    void Start()
    {
        option = GameObject.Find("Option");
        optionscript = option.GetComponent<Option>();
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
        option = GameObject.Find("Option");
        optionscript = option.GetComponent<Option>();
        if (sound != null)
        {
            soundplayer.volume = optionscript.GetSEVolume();
            soundplayer.PlayOneShot(sound);
        }
    }
}
