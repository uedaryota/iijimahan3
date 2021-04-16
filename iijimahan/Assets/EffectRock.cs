﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRock : MonoBehaviour
{
    private Object OriginalRock;
    private Vector3 velocity;
    
    void Start()
    {
        OriginalRock = serchTag(gameObject, "EffectRock");
    }

    void Update()
    {
        if(transform.name == "EffectRock1")
        {

        }
        if(transform.name == "EffectRock2")
        {

        }
    }

    GameObject serchTag(GameObject nowObj, string tagName)
    {
        float tmpDis = 0; //距離用一時変数
        float nearDis = 0; //最も近いオブジェクトの距離
        //string nearObjName = "Rock"; //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach(GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //自身と取得したオブジェクトの距離を取得
            tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

            //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
            //一時変数に距離を格納
            if(nearDis == 0 || nearDis > tmpDis)
            {
                nearDis = tmpDis;
                //nearObjName = obs.name;
                targetObj = obs;
            }
        }
        //最も近かったオブジェクトを返す
        //return gameObject.Find(nearObjName);
        return targetObj;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Rock")
        {
            OriginalRock = other;
        }
    }
}
