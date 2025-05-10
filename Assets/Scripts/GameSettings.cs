using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public float InteractionRange = 2f;
    public Color Player1Color = Color.yellow;
    public Color Player2Color = Color.black;
    
    public int scratchDuration = 5;
    public int pissDuration = 5;
    public int linjDuration = 5;

    public int scratchPoints = 1;
    public int pissPoints = 1;
    public int linjPoints = 1;

    public int MaxScratchAmount = 4;

    public Color GetPlayerColor(int playerId) 
    {
        switch (playerId)
        {
            case 1: return Player1Color;
            case 2: return Player2Color;
        }
        
        return Color.white;
    }

    public int GetActionDuration(InteractionType type)
    {
        switch (type)
        {
            case InteractionType.Scrach:
                return scratchDuration;
            case InteractionType.Piss:
                return pissDuration;
            case InteractionType.Linj:
                return linjDuration;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}