using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public float InteractionRange = 2f;
    public Color Player1Color = Color.yellow;
    public Color Player2Color = Color.black;
    
    public int scratchDuration = 5;
    public int pissDuration = 5;
    public int shedDuration = 5;

    public int scratchPoints = 1;
    public int pissPoints = 1;
    public int shedPoints = 1;

    public int MaxScratchAmount = 4;

    public int NumberOfChairs = 8;
    public float MaxScoreFactor = 1.2f;
    public int MaxPossibleScore => (int)((scratchPoints * MaxScratchAmount + pissPoints + shedPoints) * NumberOfChairs * MaxScoreFactor);

    public float TimePerGame => 300f;

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
            case InteractionType.Shed:
                return shedDuration;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}