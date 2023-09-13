using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
public class GameManager : BaseMonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
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
        gamestate = GameStates.Slow;
        Time.timeScale = Gamespeed;
    }
}
