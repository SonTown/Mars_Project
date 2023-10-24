using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumTypes;
using Structs;

public class CharacterManager : BaseMonoBehaviour
{
    private IGameManager _gameManager;
    private CharacterController characterController;
    public IGameManager GameManager
    {
        get { return _gameManager; }
    }
    public void Init(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public PlayerStates playerState;
    public CraftTypes craftType;
    public int[] Resources;
    private void Awake()
    {
        playerState = PlayerStates.Idle;
        craftType = CraftTypes.Floor;
        characterController = FindObjectOfType<CharacterController>();
        characterController.Init(this);
        Resources = new int[10];
        for(int i = 0; i < 10; i++)
        {
            Resources[i] = 10;
        }
    }
    // Update is called once per frame
    public CharacterController GetCharacterController()
    {
        return characterController;
    }
}
