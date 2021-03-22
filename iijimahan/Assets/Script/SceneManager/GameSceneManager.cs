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

    void Start()
    {
        enemymanager = GameObject.Find("EnemyManager");
        enemymanagerscript = enemymanager.GetComponent<EnemyManager>();
        player = GameObject.Find("Player");
        playerscript = player.GetComponent<PlayerControl>();
        timer = 0;
        ClearTime = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(BGM);
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        ClearTime = timer * Time.deltaTime;
        if(enemymanagerscript.GetGameClear() == true)
        {
            SceneManager.LoadScene("GameClearScene");
        }
        if(playerscript.GetisDead() == true)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    public static float GetClearTime()
    {
        return ClearTime;
    }
}
