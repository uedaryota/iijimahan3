using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLifeHanabi : MonoBehaviour
{

    private float count = 0.0f;

    private GameObject player;

    private Vector3 playerpos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime * 60;


        playerpos = new Vector3( player.transform.position.x, player.transform.position.y, player.transform.position.z);

        Vector3 screen_playerPos2 = RectTransformUtility.WorldToScreenPoint(Camera.main, playerpos);

        screen_playerPos2.z = 10f;

        screen_playerPos2 = Camera.main.ScreenToWorldPoint(screen_playerPos2);

        transform.position = screen_playerPos2;

        if (count > 90)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
