using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using System.IO;
using OfficeOpenXml;
public class GameManager : BaseMonoBehaviour,IGameManager
{
    #region GameManger instancing
    private static GameManager _instance;

    [SerializeField]
    private DataManager _dataManager;

    [SerializeField]
    private TooltipManager _tooltipManager;

    [SerializeField]
    private CraftScrollviewManager _craftScrollviewManager;

    [SerializeField]
    private CharacterManager _characterManager;

    [SerializeField]
    private CraftManager _craftManager;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject managerObject = new GameObject("GameManager");
                    _instance = managerObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            _dataManager = gameObject.AddComponent<DataManager>();
            _craftScrollviewManager = FindObjectOfType<CraftScrollviewManager>();
            _characterManager = FindObjectOfType<CharacterManager>();
            _tooltipManager = gameObject.AddComponent<TooltipManager>();
            _craftManager = gameObject.AddComponent<CraftManager>();
        }
        else
        {
            if (this != _instance)
            {
                Destroy(gameObject);
            }
        }

        // 하위 관리자들에 대한 역참조를 설정
        _dataManager.Init(this);
        _tooltipManager.Init(this);
        _craftScrollviewManager.Init(this);
        _characterManager.Init(this);
    }
    #endregion
    private GameStates gamestate = GameStates.Idle;
    public void PauseGame()
    {
        gamestate = GameStates.Pause;
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        gamestate = GameStates.Idle;
        Time.timeScale = 1f;
    }
    public void SlowGame(float Gamespeed)
    {
        _dataManager.Excelread(Path.Combine(Application.dataPath, "Resources/Craft_Datas/Craft_Data_requests.xlsx"));
        gamestate = GameStates.Slow;
        Debug.Log(Gamespeed);
        Time.timeScale = Gamespeed;
    }
    #region IGameManager Implementation

    public CraftScrollviewManager GetCraftScrollviewManager()
    {
        return _craftScrollviewManager;
    }
    public TooltipManager GetTooltipManager()
    {
        return _tooltipManager;
    }
    public DataManager GetDataManager()
    {
        return _dataManager;
    }
    public CharacterManager GetCharacterManager()
    {
        return _characterManager;
    }
    public CraftManager GetCraftManager()
    {
        return _craftManager;
    }

    #endregion
}
