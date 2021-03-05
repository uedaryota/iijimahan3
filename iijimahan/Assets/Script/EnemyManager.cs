using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField, Header("enemyboxオブジェクト")] List<Object> enemyboxes;
    [SerializeField, Header("生成間隔")] private float interval;
    [SerializeField, Header("enemyのランダム生成用のy座標(高)")] float y_high = 0;
    [SerializeField, Header("enemyのランダム生成用のy座標(低)")] float y_low = 0;
    [SerializeField, Header("enemyのランダム生成用のx座標(高)")] float x_high = 0;
    [SerializeField, Header("enemyのランダム生成用のx座標(低)")] float x_low = 0;
    private float timer;
    private float x_rnd;
    private float y_rnd;
    private int enemyrnd;
    
    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer > interval)
        {
            timer = 0;

            //x座標のランダム生成
            x_rnd = Random.Range(x_low, x_high);

            //y座標のランダム生成
            y_rnd = Random.Range(y_low, y_high);

            //ランダム座標を割り当て
            transform.position = new Vector3(x_rnd, y_rnd, 0);

            //エネミーの生成用ランダム
            enemyrnd = Random.Range(0, enemyboxes.Count);
            
            //エネミーの生成
            Instantiate(enemyboxes[enemyrnd], transform.position, Quaternion.identity);
        }
    }
}
