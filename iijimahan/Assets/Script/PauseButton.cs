using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    [SerializeField, Header("ポーズ画面を選択した時の音")]public AudioClip SelectSE;
    AudioSource audioSource;
    private GameObject option;
    private Option script;
    public GameObject OptionCanvas;
    public GameObject Canvas;

    void Start()
    {
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        audioSource = GetComponent<AudioSource>();
    }
    public void OnClickStartButton()
    {
        if(transform.name == "Option")
        {
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            Canvas.SetActive(false);
            OptionCanvas.SetActive(true);
        }
        if (transform.name == "TitleButton")
        {
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            Time.timeScale = 1f;
            SceneManager.LoadScene("TitleScene");
        }
        if (transform.name == "ResetButton")
        {
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameScene");
        }
        if(transform.name == "GameEndButton")
        {
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            Time.timeScale = 1f;
            Quit();
        }
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
