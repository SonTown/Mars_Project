using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumTypes
{
    public enum CraftTypes
    {
        Floor, Column, Wall, Tower,Turret,Others
    }
    public enum GameStates
    {
        Idle, Pause, Slow
    }
    public enum PlayerStates
    {
        Idle, Build, Fight
    }
    public enum BuildResources
    {
        Iron, Copper, Nickel, Titanium, aluminum, Tungsten, diamond, platinum, uranium
    }
}
