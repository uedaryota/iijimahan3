using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    [SerializeField]private GameObject pauzeUI;
    [SerializeField]private GameObject optionUI;
    [SerializeField, Header("ポーズ画面用SE")]private AudioClip pauzeSE;
    private GameObject FadeUI;
    private FadeImage script;
    private bool OnOff = false;
    private AudioSource audiosource;
    private GameObject option;
    private Option script2;
    
    void Start()
    {
        FadeUI = GameObject.Find("FadeCanvas");
        script = FadeUI.GetComponent<FadeImage>();
        audiosource = gameObject.GetComponent<AudioSource>();
        option = GameObject.Find("Option");
        script2 = option.GetComponent<Option>();
    }

    void Update()
    {
        if (script.fadeflag == true) return;
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if(OnOff == false)
            {
                audiosource.volume = script2.GetSEVolume();
                audiosource.PlayOneShot(pauzeSE);
                OnOff = true;
                pauzeUI.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                OnOff = false;
                pauzeUI.SetActive(false);
                optionUI.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}
