using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBurstScript : MonoBehaviour
{
    public float LifeTime;
    public AudioClip sound;
    AudioSource soundplayer;

    bool mode = false;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        soundplayer = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mode)
        {
            transform.position = player.transform.position;
        }
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
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
    public void SetModeFlag(bool fl)
    {
        mode = fl;
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
