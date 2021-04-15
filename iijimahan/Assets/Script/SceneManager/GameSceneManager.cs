using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private GameObject enemymanager;
    private EnemyManager enemymanagerscript;
    private GameObject player;
    private PlayerControl playerscript;
    private float timer = 0;
    private static float ClearTime = 0;
    public AudioClip BGM;
    AudioSource audioSource;
    public GameObject fade;
    private bool sceneChangeFlag = true;

    void Start()
    {
        Application.targetFrameRate = 60;
        fade.GetComponent<FadeStart>().FadeInA();
        enemymanager = GameObject.Find("EnemyManager");
        enemymanagerscript = enemymanager.GetComponent<EnemyManager>();
        player = GameObject.Find("Player");
        playerscript = player.GetComponent<PlayerControl>();
        timer = 0;
        ClearTime = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        sceneChangeFlag = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        timer++;
        ClearTime = timer * Time.deltaTime;
        if(enemymanagerscript.GetGameClear() == true)
        {
            //戻すならコメントアウト
            player.GetComponent<PlayerControl>().SetClearFlag(true);

            if (sceneChangeFlag && playerscript.GetClearSceneFlag() == true)
            {
                //SceneManager.LoadScene("GameClearScene");
                //fade.GetComponent<FadeStart>().FadeOutNextScene("GameClearScene");

                //***********戻す時は以下をコメントアウト

                fade.GetComponent<FadeStart>().FadeOutNextScene("GameClearScene", 1.5f, 1.7f);

                sceneChangeFlag = false;
            }

           
            //*********************ここまで

            //SceneManager.LoadScene("GameClearScene");
        }
        if(playerscript.GetisDead() == true)
        {
            if(sceneChangeFlag)
            {
                fade.GetComponent<FadeStart>().FadeOutNextScene("GameOverScene",1.5f,1.9f);
                sceneChangeFlag = false;
            }
            
            //SceneManager.LoadScene("GameOverScene");
        }
    }

    public static float GetClearTime()
    {
        return ClearTime;
    }
}
