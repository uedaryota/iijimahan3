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
    [SerializeField, Header("インターバル")] private float interval = 0.5f;
    [SerializeField, Header("ポーズ画面を開いた時の音(ポーズ画面じゃなければ入れなくていい)")]public AudioClip pauseSE;
    [SerializeField, Header("項目を移動する時の音")] public AudioClip selectSE;
    AudioSource audioSource;
    [SerializeField]int indexnum;
    [SerializeField]float timer;

    public Text hardText;

    private bool oneSEFlag = false;

    void Start()
    {
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        indexnum = 0;
        timer = 5.0f;
        button[0].Select();
        audioSource = GetComponent<AudioSource>();
        if (gameObject.name == "pause")
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        if (gameObject.name == "Canvas")
        {
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(pauseSE);
        }
    }

    void Update()
    {
        if(hardText != null)
        {
            if (script.GetClearFlag() == true && hardText.color.a == 1 && !oneSEFlag)
            {
                hardText.color = new Color(1, 1, 1, 1);
                var suzuSE = Resources.Load<AudioClip>("Sound/鈴");
                audioSource.PlayOneShot(suzuSE);
                oneSEFlag = true;
                hardText.GetComponent<Outline>().effectColor = new Color(0.764f, 0, 0.325f, 1f);
            }
        }
        

        timer += Time.unscaledDeltaTime;
        if (sort == 1)
        {
            if (Input.GetKeyDown(KeyCode.W) && indexnum != 0)
            {
                timer = 0.0f;
                indexnum--;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
            }
            if (Input.GetKeyDown(KeyCode.S) && indexnum != button.Length - 1)
            {
                timer = 0.0f;
                indexnum++;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && indexnum != 0)
            {
                timer = 0.0f;
                indexnum--;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && indexnum != button.Length - 1)
            {
                timer = 0.0f;
                indexnum++;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
            }
            if (PadControlUp() && indexnum != 0 && timer >= interval)
            {
                timer = 0.0f;
                indexnum--;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
            }
            if (PadControlDown() && indexnum != button.Length - 1 && timer >= interval)
            {
                timer = 0.0f;
                indexnum++;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
            }
        }
        if (sort == 2)
        {
            if (Input.GetKeyDown(KeyCode.A) && indexnum != 0)
            {
                timer = 0.0f;
                indexnum--;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
            }
            if (Input.GetKeyDown(KeyCode.D) && indexnum != button.Length - 1)
            {
                timer = 0.0f;
                indexnum++;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && indexnum != 0)
            {
                timer = 0.0f;
                indexnum--;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && indexnum != button.Length - 1)
            {
                timer = 0.0f;
                indexnum++;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
            }
            if (PadControlLeft() && indexnum != 0 && timer >= interval)
            {
                timer = 0.0f;
                indexnum--;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
            }
            if (PadControlRight() && indexnum != button.Length - 1 && timer >= interval)
            {
                timer = 0.0f;
                indexnum++;
                audioSource.volume = script.GetSEVolume();
                audioSource.PlayOneShot(selectSE);
            }
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

    public void SetIndexNum(int num)
    {
        indexnum = num;
    }
}
