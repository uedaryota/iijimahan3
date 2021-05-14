using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    [SerializeField]private GameObject pauzeUI;
    [SerializeField]public GameObject optionUI;
    private GameObject FadeUI;
    private FadeImage script;
    private bool OnOff = false;
    
    void Start()
    {
        FadeUI = GameObject.Find("FadeCanvas");
        script = FadeUI.GetComponent<FadeImage>();
    }

    void Update()
    {
        if (script.fadeflag == true) return;
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if(OnOff == false)
            {
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
