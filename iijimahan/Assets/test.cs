using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField, Header("ポーズ画面を選択した時の音")] public AudioClip SelectSE;
    AudioSource audioSource;
    private GameObject option;
    private Option script;
    // Start is called before the first frame update
    void Start()
    {
        option = GameObject.Find("Option");
        script = option.GetComponent<Option>();
        audioSource = GetComponent<AudioSource>();
    }
    
    public void OnClick()
    {
        if (transform.name == "Button")
        {
            audioSource.volume = script.GetSEVolume();
            audioSource.PlayOneShot(SelectSE);
            Invoke("a", 0.5f);
        }
    }

    public void a()
    {
        gameObject.SetActive(false);
    }
}
