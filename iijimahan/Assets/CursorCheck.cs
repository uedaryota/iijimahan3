using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorCheck : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField, Header("ボタンが何番目に登録されているボタンか")] private int num;
    [SerializeField, Header("Select用の親オブジェクト")] private GameObject Canvas;
    void Start()
    {
        num -= 1;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(gameObject.name == "StartButton")
        {
            
        }

        if(gameObject.name == "HardModeButton")
        {
            
        }

        if(gameObject.name == "OptionButton")
        {

        }

        if(gameObject.name == "GameEndButton")
        {

        }
    }
}
