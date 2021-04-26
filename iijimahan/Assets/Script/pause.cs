using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    [SerializeField]private GameObject pauzeUI;
    private bool OnOff = false;
    
    void Update()
    {
        
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
                Time.timeScale = 1f;
            }
        }
    }
}
