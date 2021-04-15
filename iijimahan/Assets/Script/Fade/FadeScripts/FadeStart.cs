﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeStart : MonoBehaviour
{

    public Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    FadeInNextScene();
        //}
        //else if (Input.GetKeyDown(KeyCode.X))
        //{
        //    FadeOutNextScene("GameScene");
        //}
    }

    public void FadeOutNextScene(string scene)//おわり
    {
        //StopAllCoroutines();
        fade.FadeIn(0.5f);
        StartCoroutine(NextScene(scene));
    }

    public void FadeInA()//はじめ
    {
        fade.FadeOut(1f);
    }
    private IEnumerator NextScene(string scene)
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(scene);
    }

   


}
