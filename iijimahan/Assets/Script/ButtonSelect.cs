using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    [SerializeField] private Button[] button;
    [SerializeField, Header("インターバル")] private float interval = 10f;
    [SerializeField, Header("ポーズ画面を開いた時の音")]public AudioClip pauseSE;
    [SerializeField, Header("ポーズ画面の項目を移動する時の音")] public AudioClip selectSE;
    AudioSource audioSource;
    int indexnum;
    float timer;

    void Start()
    {
        indexnum = 0;
        timer = 5.0f;
        button[0].Select();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(pauseSE);
    }

    void Update()
    {
        timer++;
        if (Input.GetKeyDown(KeyCode.UpArrow) && indexnum != 0 && timer >= interval)
        {
            timer = 0.0f;
            indexnum--;
            audioSource.PlayOneShot(selectSE);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && indexnum != button.Length - 1 && timer >= interval)
        {
            timer = 0.0f;
            indexnum++;
            audioSource.PlayOneShot(selectSE);
        }
        if (PadControlUp() && indexnum != 0 && timer >= interval)
        {
            timer = 0.0f;
            indexnum--;
            audioSource.PlayOneShot(selectSE);
        }
        if (PadControlDown() && indexnum != button.Length - 1 && timer >= interval)
        {
            timer = 0.0f;
            indexnum++;
            audioSource.PlayOneShot(selectSE);
        }

        button[indexnum].Select();
    }

    bool PadControlUp()
    {
        if(Input.GetAxis("L_Vertical") <= -0.5f)
        {
            return true;
        }
        return false;
    }

    bool PadControlDown()
    {
        if (Input.GetAxis("L_Vertical") >= 0.5f)
        {
            return true;
        }
        return false;
    }
}
