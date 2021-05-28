using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorCheck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Header("ボタンが何番目に登録されているボタンか")] private int num;
    [SerializeField, Header("カーソルが降れているボタンの番号を送るスクリプト")] private ButtonSelect select;
    [SerializeField, Header("カーソルが降れているボタンの番号を送るスクリプト")] private OptionSelect optionselect;

    private bool flag;
    void Start()
    {
        num -= 1;
        flag = false;
    }

    void OnEnable()
    {
        flag = false;
    }

    void Update()
    {
        if(flag == true && select != null)
        {
            select.SetIndexNum(num);
        }

        if(flag == true && optionselect != null)
        {
            optionselect.SetIndexNum(num);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        flag = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        flag = false;
    }
}
