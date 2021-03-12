using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    void OnClickStartButton()
    {
        if (transform.name == "TitleButton")
        {
            SceneManager.LoadScene("TitleScene");
        }
        if (transform.name == "ResetButton")
        {
            SceneManager.LoadScene("GameScene");
        }
        Time.timeScale = 1f;
    }
}
