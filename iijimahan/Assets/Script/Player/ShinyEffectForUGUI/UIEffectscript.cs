using Coffee.UIExtensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffectscript : MonoBehaviour
{

    public ShinyEffectForUGUI m_shiny;
    public Image image;
    float counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 1秒かけて再生
       // m_shiny.Play();

        // 指定した秒数かけて再生
        m_shiny.Play(1);

        // 指定した秒数かけて再生（タイムスケールを無視）
       // m_shiny.Play(1, AnimatorUpdateMode.UnscaledTime);
    }

    // Update is called once per frame
    void Update()
    {
        counter += 1*Time.deltaTime;
        float num = image.fillAmount;
        Debug.Log(num);
        if(counter>1 && num>=0.3888f)
        {
            m_shiny.Play();
            counter = 0;
        }
       
    }
}
