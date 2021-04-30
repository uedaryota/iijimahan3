using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSelect : MonoBehaviour
{
    [SerializeField, Header("インターバル")] private float interval = 0.5f;
    [SerializeField, Header("項目を移動する時の音")] public AudioClip selectSE;
    [SerializeField]private Slider[] Slider = new Slider[2];
    [SerializeField, Header("text")]private GameObject[] obj2 = new GameObject[3];
    [SerializeField, Header("Back")]private GameObject[] obj3 = new GameObject[2];
    private Text text1;
    private Text text2;
    private Text text3;
    private Text text4;
    private Text text5;
    public GameObject Canvas;
    private GameObject option;
    public GameObject OptionCanvas;
    private Option script;
    AudioSource audioSource;
    int indexnum;
    float timer;
    [SerializeField]private float BGMVolume;
    [SerializeField]private float SEVolume;
    private Color color = new Vector4(0, 1, 0, 1);
    private Color color2 = new Vector4(1, 1, 1, 1);
    private float maxValue = 10.0f * 100;
    private bool BGMdown;
    private bool BGMup;
    private bool SEdown;
    private bool SEup;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        audioSource = GetComponent<AudioSource>();
        text1 = obj2[0].GetComponent<Text>();
        text2 = obj2[1].GetComponent<Text>();
        text3 = obj2[2].GetComponent<Text>();
        text4 = obj3[0].GetComponent<Text>();
        text5 = obj3[1].GetComponent<Text>();
        Slider[0].maxValue = maxValue;
        Slider[1].maxValue = maxValue;
    }
    
    void Update()
    {
        timer += Time.unscaledDeltaTime;
        BGMVolume = script.GetBGMVolume() * 10;
        SEVolume = script.GetSEVolume() * 10;
        Slider[0].value = BGMVolume * 100;
        Slider[1].value = SEVolume * 100;
        if (Input.GetKeyDown(KeyCode.W) && indexnum != 0 && timer >= interval)
        {
            timer = 0.0f;
            indexnum--;
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(selectSE);
        }
        if (Input.GetKeyDown(KeyCode.S) && indexnum != 2 && timer >= interval)
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
        if (PadControlDown() && indexnum != 2 && timer >= interval)
        {
            timer = 0.0f;
            indexnum++;
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(selectSE);
        }
        if (BGMdown == true && BGMVolume > 0)
        {
            BGMVolume -= 1.0f;
            if (BGMVolume < 0)
            {
                BGMVolume = 0;
            }
            script.SetBGMVolume(BGMVolume / 10);
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(selectSE);
            BGMdown = false;
        }
        if (BGMup && BGMVolume < 10)
        {
            BGMVolume += 1.0f;
            if (BGMVolume > 10)
            {
                BGMVolume = 10;
            }
            script.SetBGMVolume(BGMVolume / 10);
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(selectSE);
            BGMup = false;
        }
        if (SEdown && SEVolume > 0)
        {
            SEVolume -= 1.0f;
            if (SEVolume < 0)
            {
                SEVolume = 0;
            }
            script.SetSEVolume(SEVolume / 10);
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(selectSE);
            SEdown = false;
        }
        if (SEup && SEVolume < 10)
        {
            SEVolume += 1.0f;
            if (SEVolume > 10)
            {
                SEVolume = 10;
            }
            script.SetSEVolume(SEVolume / 10);
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(selectSE);
            SEup = false;
        }
        text4.text = "" + (int)BGMVolume;
        text5.text = "" + (int)SEVolume;

        switch (indexnum)
        {
            case 0:
                timer += Time.unscaledDeltaTime;
                text1.color = color;
                text2.color = color2;
                text3.color = color2;
                if (Input.GetKeyDown(KeyCode.A) && BGMVolume > 0 && timer >= interval)
                {
                    timer = 0;
                    BGMVolume -= 1.0f;
                    if(BGMVolume  < 0)
                    {
                        BGMVolume = 0;
                    }
                    script.SetBGMVolume(BGMVolume / 10);
                    audioSource.volume = script.GetSEVolume();
                    audioSource.PlayOneShot(selectSE);
                }
                if (PadControlLeft() && BGMVolume > 0 && timer >= interval)
                {
                    timer = 0;
                    BGMVolume -= 1.0f;
                    if (BGMVolume < 0)
                    {
                        BGMVolume = 0;
                    }
                    script.SetBGMVolume(BGMVolume / 10);
                    audioSource.volume = script.GetSEVolume();
                    audioSource.PlayOneShot(selectSE);
                }
                if (Input.GetKeyDown(KeyCode.D) && BGMVolume < 10 && timer >= interval)
                {
                    timer = 0;
                    BGMVolume += 1.0f;
                    if (BGMVolume > 10)
                    {
                        BGMVolume = 10;
                    }
                    script.SetBGMVolume(BGMVolume / 10);
                    audioSource.volume = script.GetSEVolume();
                    audioSource.PlayOneShot(selectSE);
                }
                if (PadControlRight() && BGMVolume < 10 && timer >= interval)
                {
                    timer = 0;
                    BGMVolume += 1.0f;
                    if (BGMVolume > 10)
                    {
                        BGMVolume = 10;
                    }
                    script.SetBGMVolume(BGMVolume / 10);
                    audioSource.volume = script.GetSEVolume();
                    audioSource.PlayOneShot(selectSE);
                }
                return;

            case 1:
                timer += Time.unscaledDeltaTime;
                text1.color = color2;
                text2.color = color;
                text3.color = color2;
                if (Input.GetKeyDown(KeyCode.A) && SEVolume > 0 && timer >= interval)
                {
                    timer = 0;
                    SEVolume -= 1.0f;
                    if (SEVolume < 0)
                    {
                        SEVolume = 0;
                    }
                    script.SetSEVolume(SEVolume / 10);
                    audioSource.volume = script.GetSEVolume();
                    audioSource.PlayOneShot(selectSE);
                }
                if (PadControlLeft() && SEVolume > 0 && timer >= interval)
                {
                    timer = 0;
                    SEVolume -= 1.0f;
                    if (SEVolume < 0)
                    {
                        SEVolume = 0;
                    }
                    script.SetSEVolume(SEVolume / 10);
                    audioSource.volume = script.GetSEVolume();
                    audioSource.PlayOneShot(selectSE);
                }
                if (Input.GetKeyDown(KeyCode.D) && SEVolume < 10 && timer >= interval)
                {
                    timer = 0;
                    SEVolume += 1.0f;
                    if (SEVolume > 10)
                    {
                        SEVolume = 10;
                    }
                    script.SetSEVolume(SEVolume / 10);
                    audioSource.volume = script.GetSEVolume();
                    audioSource.PlayOneShot(selectSE);
                }
                if (PadControlRight() && SEVolume < 10 && timer >= interval)
                {
                    timer = 0;
                    SEVolume += 1.0f;
                    if (SEVolume > 10)
                    {
                        SEVolume = 10;
                    }
                    script.SetSEVolume(SEVolume / 10);
                    audioSource.volume = script.GetSEVolume();
                    audioSource.PlayOneShot(selectSE);
                }
                return;

            case 2:
                timer += Time.unscaledDeltaTime;
                text1.color = color2;
                text2.color = color2;
                text3.color = color;
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    Canvas.SetActive(true);
                    OptionCanvas.SetActive(false);
                }
                    return;

            default:
                return;
        }
    }

    bool PadControlUp()
    {
        if (Input.GetAxis("L_Vertical") <= -0.5f)
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
        if (Input.GetAxis("L_Horizontal") <= -0.5f)
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

    public bool SetBGMDownClick(bool flag)
    {
        return BGMdown = flag;
    }

    public bool SetBGMUpClick(bool flag)
    {
        return BGMup = flag;
    }

    public bool SetSEDownClick(bool flag)
    {
        return SEdown = flag;
    }
    public bool SetSEUpClick(bool flag)
    {
        return SEup = flag;
    }


}
