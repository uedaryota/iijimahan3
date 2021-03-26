using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    [SerializeField, Header("ポーズ画面を選択した時の音")]public AudioClip SelectSE;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
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
        audioSource.PlayOneShot(SelectSE);
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
