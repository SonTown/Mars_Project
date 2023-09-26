using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TooltipGen : BaseMonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TooltipManager tooltipManager; // TooltipManager�� ����

    //Ŭ�� ���ο� �ð��� üũ��, ���� �ð� ��ġ �� tooltip�� ���ϴ�.
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

            // Ŭ���� ���� �ð� �̻� ���ӵǸ� ������ ǥ���մϴ�.
            if (clickTimer >= clickDuration)
            {
                tooltipManager.ShowTooltip(text);
                isClick = false; // Ŭ�� ���� ����
            }
            //Debug.Log(clickTimer);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Ŭ���� ���۵� �� ȣ��Ǵ� �Լ�
        isClick = true;
        Debug.Log("Start");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Ŭ���� ����� �� ȣ��Ǵ� �Լ�
        clickTimer = 0f; // Ŭ�� Ÿ�̸� �ʱ�ȭ
        isClick = false; // Ŭ�� ���� ����
        tooltipManager.HideTooltip();
    }
}
