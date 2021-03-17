using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    [SerializeField] private Button[] button;
    [SerializeField, Header("インターバル")] private float interval = 10f;
    //Button button;
    //Button button2;
    int indexnum;
    float timer;

    void Start()
    {
        indexnum = 0;
        timer = 5.0f;
        button[0].Select();
    }

    void Update()
    {
        timer++;
        if (Input.GetKeyDown(KeyCode.UpArrow) && indexnum != 0 && timer >= interval)
        {
            timer = 0.0f;
            indexnum--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && indexnum != button.Length - 1 && timer >= interval)
        {
            timer = 0.0f;
            indexnum++;
        }
        if (PadControlUp() && indexnum != 0 && timer >= interval)
        {
            timer = 0.0f;
            indexnum--;
        }
        if (PadControlDown() && indexnum != button.Length - 1 && timer >= interval)
        {
            timer = 0.0f;
            indexnum++;
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
