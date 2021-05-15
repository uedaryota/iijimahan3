using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    private GameObject option;
    private Option script;
    [SerializeField, Header("次のシーンを選択した時の音")] public AudioClip SelectSE;
    AudioSource audioSource;
    public GameObject fade;
    private bool sceneChangeFlag = true;
    private bool Clickflag;

    void Start()
    {
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        audioSource = GetComponent<AudioSource>();
        Clickflag = false;
        //fade.GetComponent<FadeStart>().FadeInA();
    }
    public void OnClickStartButton()
    {
        audioSource.volume = script.GetSEVolume();
        audioSource.PlayOneShot(SelectSE);
        if (transform.name == "TitleButton" && Clickflag == false)
        {
            Clickflag = true;
            Invoke("Title", 0.5f);
        }
        if (transform.name == "GameEndButton")
        {
            Clickflag = true;
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

    private void Title()
    {
        //SceneManager.LoadScene("TitleScene");
        if (sceneChangeFlag)
        {
            Clickflag = false;
            fade.GetComponent<FadeStart>().FadeOutNextScene("TitleScene");
            sceneChangeFlag = false;
        }
    }
}
