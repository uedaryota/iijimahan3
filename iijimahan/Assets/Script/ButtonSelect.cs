using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    Button button;
    Button button2;
    string num;

    void Start()
    {
        button = GameObject.Find("pause/Panel/TitleButton").GetComponent<Button>();
        button2 = GameObject.Find("pause/Panel/ResetButton").GetComponent<Button>();
        button.Select();
    }
    
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Joystick1Button0))
        //{
        //    button.Select();
        //}
        //if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        //{
        //    button2.Select();
        //}
    }
}
