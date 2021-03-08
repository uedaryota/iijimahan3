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

    void Start()
    {
        enemymanager = GameObject.Find("EnemyManager");
        enemymanagerscript = enemymanager.GetComponent<EnemyManager>();
        player = GameObject.Find("Player");
        playerscript = player.GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemymanagerscript.GetGameClear() == true)
        {
            SceneManager.LoadScene("GameClearScene");
        }
        if(playerscript.GetisDead() == true)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
