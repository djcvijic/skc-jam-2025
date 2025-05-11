using System;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

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
    public int shit;
    public int shedPoints = 1;

    public int MaxScratchAmount = 4;

    public int NumberOfChairs = 8;
    public float MaxScoreFactor = 0.6f;
    
    public int MaxPossibleScore => (int)((scratchPoints * MaxScratchAmount + pissPoints + shedPoints) * NumberOfChairs * MaxScoreFactor);
    public float ThoughBubbleDisplayDuration = 2f;

    [field: SerializeField] public float CatAcceleration { get; private set; } = 1f;
    [field: SerializeField] public float CatMaxSpeed { get; private set; } = 1f;
    [field: SerializeField] public float CatFriction { get; private set; } = 1f;
    [field: SerializeField] public float StunDuration { get; private set; } = 5f;

    public Sprite Piss1;
    public Sprite Piss2;
    public Sprite Scratch1;
    public Sprite Scratch2;
    public Sprite Shed1;
    public Sprite Shed2;

    public float TimePerGame = 300f;
    public GameObject pissEffect;
    public GameObject scratchEffect;
    public GameObject shedEffect;


    public Color GetPlayerColor(int playerId) 
    {
        switch (playerId)
        {
            case 1: return Player1Color;
            case 2: return Player2Color;
        }
        
        return Color.white;
    }

    public Sprite GetPlayerActionSprite(InteractionType type, int playerId)
    {
        switch (type)
        {
            case InteractionType.Scratch:
                return playerId == 1 ? Scratch1 : Scratch2;
            case InteractionType.Piss:
                return playerId == 1 ? Piss1 : Piss2;
            case InteractionType.Shed:
                return playerId == 1 ? Shed1 : Shed2;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public int GetActionDuration(InteractionType type)
    {
        switch (type)
        {
            case InteractionType.Scratch:
                return scratchDuration;
            case InteractionType.Piss:
                return pissDuration;
            case InteractionType.Shed:
                return shedDuration;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public GameObject GetInteractionParticles(InteractionType type)
    {
        switch (type)
        {
            case InteractionType.Scratch:
                return scratchEffect;
            case InteractionType.Piss:
                return pissEffect;
            case InteractionType.Shed:
                return shedEffect;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}