using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;
using Structs;
using EnumTypes;
public class TooltipGen : BaseMonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TooltipManager tooltipManager; // TooltipManager를 선언
    //클릭 여부와 시간을 체크해, 일정 시간 터치 시 tooltip을 띄웁니다.
    public PreBuildControl prebuild;
    private string text;
    private Dataset_craft dataSave;
    private string Name;
    private float clickTimer;
    private bool isClick;
    private float clickDuration=1f;
    private void Awake()
    {
        Debug.Log("Turn On!");
        tooltipManager=GameManager.Instance.GetTooltipManager();
        prebuild = GameManager.Instance.GetCraftManager().GetPreBuild();
    }
    void Update()
    {
        if (isClick)
        {
            clickTimer += Time.deltaTime;

            // 클릭이 일정 시간 이상 지속되면 툴팁을 표시합니다.
            if (clickTimer >= clickDuration)
            {
                dataSave = GameManager.Instance.GetDataManager().craftRequests[(CraftTypes)Enum.Parse(typeof(CraftTypes), Name)];
                text = "";
                for(int i = 0; i < dataSave.item_name.Length; i++)
                {
                    if (dataSave.item_name[i] == null)
                    {
                        continue;
                    }
                    print(dataSave.item_num[0, i]);
                    text = text + dataSave.item_name[i] + ": "+dataSave.item_num[0,i]+"piece needed\n";
                }
                tooltipManager.ShowTooltip(text);
                isClick = false; // 클릭 상태 종료
            }
            //Debug.Log(clickTimer);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Name = this.GetComponentInChildren<TMP_Text>().text;
        // 클릭이 시작될 때 호출되는 함수
        isClick = true;
        Debug.Log("Start");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isClick)
        {
             GameManager.Instance.GetCharacterManager().craftType = (CraftTypes)Enum.Parse(typeof(CraftTypes), Name);
             print(GameManager.Instance.GetCharacterManager().craftType);
            prebuild.SetActiveBuildingType(GameManager.Instance.GetCharacterManager().craftType);
        }
        // 클릭이 종료될 때 호출되는 함수
        clickTimer = 0f; // 클릭 타이머 초기화
        isClick = false; // 클릭 상태 종료
        tooltipManager.HideTooltip();
    }
}
