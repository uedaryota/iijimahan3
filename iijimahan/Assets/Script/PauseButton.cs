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
    private float time;
    private bool flag;
    private bool Clickflag;

    void Start()
    {
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        audioSource = GetComponent<AudioSource>();
        time = 0;
        flag = false;
        Clickflag = false;
    }

    void Update()
    {
        if(flag == true)
        {
            time += Time.unscaledDeltaTime;
        }
        if(time >= 0.5f)
        {
            Option();
            time = 0.0f;
            flag = false;
        }
    }

    public void OnClick()
    {
        if(transform.name == "Option" && Clickflag == false)
        {
            Clickflag = true;
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            flag = true;
        }
        if (transform.name == "TitleButton" && Clickflag == false)
        {
            Clickflag = true;
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            Time.timeScale = 1f;
            Invoke("Title", 0.5f);
            //SceneManager.LoadScene("TitleScene");
        }
        if (transform.name == "ResetButton" && Clickflag == false)
        {
            Clickflag = true;
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            Time.timeScale = 1f;
            Invoke("Reset", 0.5f);
            //SceneManager.LoadScene("GameScene");
        }
        if(transform.name == "GameEndButton" && Clickflag == false)
        {
            Clickflag = true;
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            Time.timeScale = 1f;
            Invoke("Quit", 0.5f);
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

    private void Option()
    {
        Clickflag = false;
        Canvas.SetActive(false);
        OptionCanvas.SetActive(true);
    }

    private void Title()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private void Reset()
    {
        SceneManager.LoadScene("GameScene");
    }
}
