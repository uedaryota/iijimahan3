using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public AudioClip BGM;
    [SerializeField, Header("ボタンを押したときの音")] public AudioClip SelectSE;
    AudioSource audioSource;

    void Start()
    {
        audioSource.Play();
    }

    void Update()
    {
        if(Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            SceneManager.LoadScene("GameScene");
            audioSource.PlayOneShot(SelectSE);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 1"))
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
