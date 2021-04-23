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

    void Start()
    {
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        audioSource = GetComponent<AudioSource>();
        //fade.GetComponent<FadeStart>().FadeInA();
    }
    public void OnClickStartButton()
    {
        audioSource.volume = script.GetSEVolume();
        audioSource.PlayOneShot(SelectSE);
        if (transform.name == "TitleButton")
        {
            //SceneManager.LoadScene("TitleScene");
            if(sceneChangeFlag)
            {
                fade.GetComponent<FadeStart>().FadeOutNextScene("TitleScene");
                sceneChangeFlag = false;
            }
            
        }
        if (transform.name == "GameEndButton")
        {
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
