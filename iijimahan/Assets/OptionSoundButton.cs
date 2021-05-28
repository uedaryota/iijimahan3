using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionSoundButton : MonoBehaviour
{
    private GameObject option;
    private Option script;
    [SerializeField, Header("項目を選択した時の音")] public AudioClip SelectSE;
    AudioSource audioSource;
    [SerializeField]private GameObject OptionCanvas;
    private OptionSelect script2;
    public GameObject Canvas;
    private float time;
    private bool flag;
    private bool Clickflag;

    void Start()
    {
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        OptionCanvas = GameObject.Find("OptionCanvas");
        script2 = OptionCanvas.GetComponent<OptionSelect>();
        audioSource = GetComponent<AudioSource>();
        time = 0;
        flag = false;
        Clickflag = false;
    }

    void Update()
    {
        if (flag == true)
        {
            time += Time.unscaledDeltaTime;
        }
        if (time >= 0.5f)
        {
            flag = false;
            Clickflag = false;
            time = 0.0f;
            QuitButton();
        }
    }

    public void OnClick()
    {
        if (transform.name == "BGMDown")
        {
            script2.SetBGMDownClick(true);
        }
        if (transform.name == "BGMUp")
        {
            script2.SetBGMUpClick(true);
        }
        if (transform.name == "SEDown")
        {
            script2.SetSEDownClick(true);
        }
        if (transform.name == "SEUp")
        {
            script2.SetSEUpClick(true);
        }
        if(transform.name == "QuitButton" && Clickflag == false)
        {
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            Clickflag = true;
            flag = true;
        }
    }

    private void QuitButton()
    {
        Canvas.SetActive(true);
        OptionCanvas.SetActive(false);
    }
}
