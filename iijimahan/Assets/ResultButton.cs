using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    [SerializeField, Header("次のシーンを選択した時の音")] public AudioClip SelectSE;
    AudioSource audioSource;
    public GameObject fade;
    private bool sceneChangeFlag = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //fade.GetComponent<FadeStart>().FadeInA();
    }
    public void OnClickStartButton()
    {
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
        audioSource.PlayOneShot(SelectSE);
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
