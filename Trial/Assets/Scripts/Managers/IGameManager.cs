using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    CraftScrollviewManager GetCraftScrollviewManager();
    TooltipManager GetTooltipManager();
    DataManager GetDataManager();
    CharacterManager GetCharacterManager();
    CraftManager GetCraftManager();
}
