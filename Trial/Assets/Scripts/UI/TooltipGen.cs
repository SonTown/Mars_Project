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
    public TooltipManager tooltipManager; // TooltipManager�� ����
    //Ŭ�� ���ο� �ð��� üũ��, ���� �ð� ��ġ �� tooltip�� ���ϴ�.
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

            // Ŭ���� ���� �ð� �̻� ���ӵǸ� ������ ǥ���մϴ�.
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
                isClick = false; // Ŭ�� ���� ����
            }
            //Debug.Log(clickTimer);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Name = this.GetComponentInChildren<TMP_Text>().text;
        // Ŭ���� ���۵� �� ȣ��Ǵ� �Լ�
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
        // Ŭ���� ����� �� ȣ��Ǵ� �Լ�
        clickTimer = 0f; // Ŭ�� Ÿ�̸� �ʱ�ȭ
        isClick = false; // Ŭ�� ���� ����
        tooltipManager.HideTooltip();
    }
}
