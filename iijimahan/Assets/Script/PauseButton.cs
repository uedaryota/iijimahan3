using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public void OnClickStartButton()
    {
        if (transform.name == "TitleButton")
        {
            SceneManager.LoadScene("TitleScene");
        }
        if (transform.name == "ResetButton")
        {
            SceneManager.LoadScene("GameScene");
        }
        if(transform.name == "GameEndButton")
        {
            Quit();
        }
        Time.timeScale = 1f;
    }
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
