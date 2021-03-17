using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    [SerializeField, Header("ポーズ画面オブジェクト")] private Object pauzeUI;
    private GameObject pauzeUIInstance;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if(pauzeUIInstance == null)
            {
                pauzeUIInstance = GameObject.Instantiate(pauzeUI) as GameObject;
                Time.timeScale = 0f;
            }
            else
            {
                Destroy(pauzeUIInstance);
                Time.timeScale = 1f;
            }
        }

        //if (Input.GetKeyDown("joystick button 0"))
        //{
        //    Debug.Log("0");
        //}
        //if (Input.GetKeyDown("joystick button 1"))
        //{
        //    Debug.Log("1");
        //}
        //if (Input.GetKeyDown("joystick button 2"))
        //{
        //    Debug.Log("2");
        //}
        //if (Input.GetKeyDown("joystick button 3"))
        //{
        //    Debug.Log("3");
        //}
        //if (Input.GetKeyDown("joystick button 4"))
        //{
        //    Debug.Log("4");
        //}
        //if (Input.GetKeyDown("joystick button 5"))
        //{
        //    Debug.Log("5");
        //}
        //if (Input.GetKeyDown("joystick button 6"))
        //{
        //    Debug.Log("6");
        //}
        //if (Input.GetKeyDown("joystick button 7"))
        //{
        //    Debug.Log("7");
        //}
        //if (Input.GetKeyDown("joystick button 8"))
        //{
        //    Debug.Log("8");
        //}
        //if (Input.GetKeyDown("joystick button 9"))
        //{
        //    Debug.Log("9");
        //}
        //if (Input.GetKeyDown("joystick button 10"))
        //{
        //    Debug.Log("10");
        //}
        //if (Input.GetKeyDown("joystick button 11"))
        //{
        //    Debug.Log("11");
        //}
        //if (Input.GetKeyDown("joystick button 12"))
        //{
        //    Debug.Log("12");
        //}
        //if (Input.GetKeyDown("joystick button 13"))
        //{
        //    Debug.Log("13");
        //}
        //if (Input.GetKeyDown("joystick button 14"))
        //{
        //    Debug.Log("14");
        //}
        //if (Input.GetKeyDown("joystick button 15"))
        //{
        //    Debug.Log("15");
        //}
        //if (Input.GetKeyDown("joystick button 16"))
        //{
        //    Debug.Log("16");
        //}
        //if (Input.GetKeyDown("joystick button 17"))
        //{
        //    Debug.Log("17");
        //}
        //if (Input.GetKeyDown("joystick button 18"))
        //{
        //    Debug.Log("18");
        //}
        //if (Input.GetKeyDown("joystick button 19"))
        //{
        //    Debug.Log("19");
        //}
    }
}
