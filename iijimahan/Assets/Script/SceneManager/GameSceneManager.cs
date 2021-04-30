using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private GameObject option;
    private Option optionscript;
    [SerializeField, Header("ピッチの上がるまでのスピード")] private float pitchup = 0.1f;
    [SerializeField, Header("ピッチの下がるまでのスピード")] private float pitchdown = 0.1f;
    [SerializeField, Header("ピッチの最大")] private float pitchhight = 2.0f;

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
    private bool oneFlagFade = false;

    private GameObject manager;
    private EnemyManager script;
    private int wave = 0;
    private int old_wave = 0;
    private bool pitchchange;

    void Start()
    {
        option = GameObject.Find("Option");
        optionscript = option.GetComponent<Option>();
        Application.targetFrameRate = 60;
        //fade.GetComponent<FadeStart>().FadeInA();
        enemymanager = GameObject.Find("EnemyManager");
        enemymanagerscript = enemymanager.GetComponent<EnemyManager>();
        player = GameObject.Find("Player");
        playerscript = player.GetComponent<PlayerControl>();
        timer = 0;
        ClearTime = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGM;
        audioSource.Play();
        sceneChangeFlag = true;

        manager = GameObject.Find("EnemyManager");
        script = manager.GetComponent<EnemyManager>();
        pitchchange = false;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = optionscript.GetBGMVolume();
        if (!oneFlagFade)
        {
            fade.GetComponent<FadeStart>().FadeInA();
            oneFlagFade = true;
        }

        old_wave = wave;
        wave = script.GetWave();
        if (old_wave != wave)
        {
            pitchchange = true;
        }
        if (pitchchange == true)
        {
            if (wave % 2 == 0)
            {
                Pitch(1);
            }
            else
            {
                Pitch(2);
            }
        }

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
                fade.GetComponent<FadeStart>().FadeOutNextScene("GameOverScene",2.3f,2.5f);
                sceneChangeFlag = false;
            }
            
            //SceneManager.LoadScene("GameOverScene");
        }
    }

    private void Pitch(int num)
    {
        switch(num)
        {
            case 1:
                if (pitchhight > audioSource.pitch)
                {
                    audioSource.pitch += Time.deltaTime * pitchup;
                }
                else if(pitchhight < audioSource.pitch)
                {
                    audioSource.pitch = pitchhight;
                }
                else
                {
                    pitchchange = false;
                }
                return;

            case 2:
                if(1 <= audioSource.pitch)
                {
                    audioSource.pitch -= Time.deltaTime * pitchdown;
                }
                else if(1 < audioSource.pitch)
                {
                    audioSource.pitch = 1;
                }
                else
                {
                    pitchchange = false;
                }
                return;

            default:
                return;
        }
    }

    public static float GetClearTime()
    {
        return ClearTime;
    }
}
