using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public GameObject text_obj;
    private GameObject manager;
    private EnemyManager script;
    private int wave = 0;
    private int old_wave = 0;
    void Start()
    {
        manager = GameObject.Find("EnemyManager");
        script = manager.GetComponent<EnemyManager>();
    }

    void Update()
    {
        old_wave = wave;
        wave = script.GetWave();
        Text text = text_obj.GetComponent<Text>();
        if(script.GetBonusWave() == true)
        {
            text.text = "BonusWave!";
        }
        else
        {
            text.text = "Wave " + wave;
        }

    }
}
