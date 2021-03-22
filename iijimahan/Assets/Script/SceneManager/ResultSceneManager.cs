using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    public GameObject text_obj;
    private PlayerControl script;
    private int gauge;
    private float time;
    private float interval;
    public AudioClip BGM;
    [SerializeField, Header("ボタンを押したときの音")]public AudioClip SelectSE;
    AudioSource audioSource;

    void Start()
    {
        interval = 0;
        gauge = PlayerControl.GetGaugeCount();
        time = GameSceneManager.GetClearTime();
        Text text = text_obj.GetComponent<Text>();
        text.text = "ゲージ使用回数 : " + gauge + "      タイム : " + time;
        audioSource.Play();
    }
    void Update()
    {
        interval += Time.deltaTime;
        if (interval >= 2.0f)
        {
            if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                audioSource.PlayOneShot(SelectSE);
                SceneManager.LoadScene("TitleScene");
            }
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
