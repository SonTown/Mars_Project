using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    private IGameManager _gameManager;
    public IGameManager GameManager
    {
        get { return _gameManager; }
    }

    public void Init(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }
    public GameObject Tooltip;
    private Canvas canvas;
    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        Tooltip = Resources.Load<GameObject>("Button");
        Tooltip = Instantiate(Tooltip,canvas.transform);
        Tooltip.transform.position = new Vector3(0, 0, 0);
        Tooltip.SetActive(false);
    }
    public void ShowTooltip(string content)
    {
        Tooltip.SetActive(true);
        // 툴팁 내용 업데이트
        TMP_Text tooltipText = Tooltip.GetComponentInChildren<TMP_Text>();
        tooltipText.transform.position = new Vector3(0, 0, 0);
    }

    public void HideTooltip()
    {
        Tooltip.SetActive(false);
    }
}
