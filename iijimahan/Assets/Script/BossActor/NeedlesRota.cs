using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedlesRota : MonoBehaviour
{
    [Header("フレーム回転数")] public float x;
    public bool Stop;
    // Start is called before the first frame update
    void Start()
    {
        Stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズの時に止める
        if (Time.timeScale <= 0) return;
        if (Stop == true) return;
        transform.Rotate(new Vector3(0, x, 0));
    }
    public void Rota()
    {
        Stop = false;
    }
    public void Sto()
    {
        Stop = true;
    }
}
