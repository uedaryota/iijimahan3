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

    void Start()
    {
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        OptionCanvas = GameObject.Find("OptionCanvas");
        script2 = OptionCanvas.GetComponent<OptionSelect>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnClickStartButton()
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
    }
}
