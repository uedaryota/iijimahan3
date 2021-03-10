using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    [SerializeField, Header("ポーズ画面オブジェクト")] private Object pauzeUI;
    private GameObject pauzeUIInstance;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
    }
}
