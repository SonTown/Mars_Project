using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TooltipGen : BaseMonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TooltipManager tooltipManager; // TooltipManager를 선언

    //클릭 여부와 시간을 체크해, 일정 시간 터치 시 tooltip을 띄웁니다.
    private string text;
    private float clickTimer;
    private bool isClick;
    private float clickDuration=0.1f;
    private void Awake()
    {
        Debug.Log("Turn On!");
        text = this.GetComponentInChildren<TMP_Text>().text;
        tooltipManager=GameManager.Instance.GetTooltipManager();
    }
    void Update()
    {
        if (isClick)
        {
            clickTimer += Time.deltaTime;

            // 클릭이 일정 시간 이상 지속되면 툴팁을 표시합니다.
            if (clickTimer >= clickDuration)
            {
                tooltipManager.ShowTooltip(text);
                isClick = false; // 클릭 상태 종료
            }
            //Debug.Log(clickTimer);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 클릭이 시작될 때 호출되는 함수
        isClick = true;
        Debug.Log("Start");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 클릭이 종료될 때 호출되는 함수
        clickTimer = 0f; // 클릭 타이머 초기화
        isClick = false; // 클릭 상태 종료
        tooltipManager.HideTooltip();
    }
}
