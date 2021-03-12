﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
