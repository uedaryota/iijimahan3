using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScaleChange : MonoBehaviour
{
    RectTransform rect;
    Text text;
    float scale = 10;
    float alphatime = 3;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        text = GetComponent<Text>();
        scale = 10;
        alphatime = 3;
    }

    // Update is called once per frame
    void Update()
    {
        scale -= Time.deltaTime*10;
        alphatime -= Time.deltaTime;
        if (scale < 1)
        {
            scale = 1;
        }
        if (alphatime < 1)
        {
            alphatime = 1;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, 3 - alphatime);
        rect.localScale =new Vector3(scale, scale, scale);
    }
}
