using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    [SerializeField, Header("次のシーンを選択した時の音")] public AudioClip SelectSE;
    AudioSource audioSource;
    public GameObject fade;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
       // fade.GetComponent<FadeStart>().FadeInA();
        //Application.targetFrameRate = 60;
    }
    public void OnClickStartButton()
    {
        if (transform.name == "StartButton")
        {
            fade.GetComponent<FadeStart>().FadeOutNextScene("GameScene");
            //SceneManager.LoadScene("GameScene");
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
