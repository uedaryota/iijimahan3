using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField, Header("enemyboxオブジェクト")] List<Object> enemyboxes;
    [SerializeField, Header("enemyboxオブジェクトのそれぞれのエネミー数")] List<int> enemynum;
    [SerializeField, Header("bossオブジェクト")] Object boss;
    [SerializeField, Header("生成間隔")] float interval = 3;
    [SerializeField, Header("enemyのランダム生成用のy座標(高)")] float y_high = 0;
    [SerializeField, Header("enemyのランダム生成用のy座標(低)")] float y_low = 0;
    [SerializeField, Header("enemyのランダム生成用のx座標(高)")] float x_high = 0;
    [SerializeField, Header("enemyのランダム生成用のx座標(低)")] float x_low = 0;
    [SerializeField, Header("enemyの生成上限")] int enemycountlimit = 10;
    [SerializeField, Header("wave")] int wave = 1;
    
    private float timer;
    private float x_rnd;
    private float y_rnd;
    private int old_wave;

    private int enemyrnd;
    private int enemycount;//エネミー生成用のカウント
    private int enemydeathcount = 0;//エネミーの倒れた数のカウント
    
    void Update()
    {
        old_wave = wave;
        if (Input.GetKeyDown(KeyCode.J))
        {
            wave = 1;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            wave = 2;
        }
        switch (wave)
        {
            case 1:
                enemyrnd = Random.Range(0, 1);
                EnemyRespawn();
                return;

            case 2:
                enemyrnd = Random.Range(0, 1);
                BossRespawn();
                EnemyRespawn();
                return;
        }
    }

    void EnemyRespawn()
    {
        timer += Time.deltaTime;
        
        if (enemycountlimit >= enemycount)
        {
            if (timer > interval)
            {
                timer = 0;

                enemycount++;

                //x座標のランダム生成
                x_rnd = Random.Range(x_low, x_high);

                //y座標のランダム生成
                y_rnd = Random.Range(y_low, y_high);

                //ランダム座標を割り当て
                transform.position = new Vector3(x_rnd, y_rnd, 0);

                //エネミーの生成用ランダム
                //enemyrnd = Random.Range(0, enemyboxes.Count);

                //エネミーの生成
                Instantiate(enemyboxes[enemyrnd], transform.position, Quaternion.identity);
            }
        }
    }

    void BossRespawn()
    {
        if (wave != old_wave)
        {
            Instantiate(boss, transform.position, Quaternion.identity);
        }
    }

    public void SetEnemyDeathCount(int count)
    {
        enemydeathcount += count;
    }

    public void SceneChange()
    {

    }
}
