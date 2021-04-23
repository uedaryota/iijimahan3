using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    private GameObject option;
    private Option script;
    [SerializeField] private Button[] button;
    [SerializeField, Header("縦並び:1 横並び:2")] private int sort = 1;
    [SerializeField, Header("インターバル")] private float interval = 10f;
    [SerializeField, Header("ポーズ画面を開いた時の音(ポーズ画面じゃなければ入れなくていい)")]public AudioClip pauseSE;
    [SerializeField, Header("項目を移動する時の音")] public AudioClip selectSE;
    AudioSource audioSource;
    int indexnum;
    float timer;
    bool Mouse;

    void Start()
    {
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        indexnum = 0;
        timer = 5.0f;
        button[0].Select();
        audioSource = GetComponent<AudioSource>();
        if (gameObject.name == "pause(Clone)")
        {
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(pauseSE);
        }
        Mouse = true;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Mouse = true;
        }
        timer++;
        if (sort == 1)
        {
            if (Input.GetKeyDown(KeyCode.W) && indexnum != 0 && timer >= interval)
            {
                timer = 0.0f;
                indexnum--;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
                Mouse = false;
            }
            if (Input.GetKeyDown(KeyCode.S) && indexnum != button.Length - 1 && timer >= interval)
            {
                timer = 0.0f;
                indexnum++;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
                Mouse = false;
            }
            if (PadControlUp() && indexnum != 0 && timer >= interval)
            {
                timer = 0.0f;
                indexnum--;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
                Mouse = false;
            }
            if (PadControlDown() && indexnum != button.Length - 1 && timer >= interval)
            {
                timer = 0.0f;
                indexnum++;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
                Mouse = false;
            }
        }
        if (sort == 2)
        {
            if (Input.GetKeyDown(KeyCode.A) && indexnum != 0 && timer >= interval)
            {
                timer = 0.0f;
                indexnum--;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
                Mouse = false;
            }
            if (Input.GetKeyDown(KeyCode.D) && indexnum != button.Length - 1 && timer >= interval)
            {
                timer = 0.0f;
                indexnum++;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
                Mouse = false;
            }
            if (PadControlLeft() && indexnum != 0 && timer >= interval)
            {
                timer = 0.0f;
                indexnum--;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
                Mouse = false;
            }
            if (PadControlRight() && indexnum != button.Length - 1 && timer >= interval)
            {
                timer = 0.0f;
                indexnum++;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
                Mouse = false;
            }
        }
        if (Mouse == true)
        {
            button[indexnum].Select();
        }
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

    bool PadControlLeft()
    {
        if(Input.GetAxis("L_Horizontal") <= -0.5f)
        {
            return true;
        }
        return false;
    }

    bool PadControlRight()
    {
        if (Input.GetAxis("L_Horizontal") >= 0.5f)
        {
            return true;
        }
        return false;
    }
}
