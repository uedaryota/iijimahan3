using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFade : MonoBehaviour
{
    Text text;
    Button button;
    float alpha = 5;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        button = GetComponent<Button>();
        alpha = 5;
    }

    // Update is called once per frame
    void Update()
    {
        alpha -= Time.deltaTime*1.5f;
        if(alpha<0)
        {
            alpha = 0;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, (3 - alpha) / 3);
        button.GetComponentInChildren<Image>().color = new Color
            (button.GetComponentInChildren<Image>().color.r,
            button.GetComponentInChildren<Image>().color.g,
            button.GetComponentInChildren<Image>().color.b,
          (  3 - alpha) / 3);
    }
}
