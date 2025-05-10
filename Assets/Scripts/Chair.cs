using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Chair : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer pissImage;
    [SerializeField] private SpriteRenderer scratchImage;
    [SerializeField] private SpriteRenderer shedImage;
    [SerializeField] private SpriteRenderer outlineImage;

    private int pissPlayerId;
    private int shedPlayerId;

    private record ScratchData(int PlayerId, int ScratchAmount);

    private ScratchData scratch = new(0, 0);

    private bool isInteracting;
    private SimpleTimer actionTimer;

    public void ShowInteract(bool show, int playerId)
    {
        if (isInteracting) return;
        
        outlineImage.gameObject.SetActive(show);
        outlineImage.color = App.Instance.GameSettings.GetPlayerColor(playerId);
        
        isInteracting = show;
    }

    public bool CanInteract(InteractionType type, int playerId)
    {
        if (isInteracting) return false;
        
        switch (type)
        {
            case InteractionType.Scratch:
                if (scratch.PlayerId == playerId) return false;
                if (scratch.ScratchAmount >= App.Instance.GameSettings.MaxScratchAmount) return false;
                break;
            case InteractionType.Piss:
                if (pissPlayerId == playerId) return false;
                break;
            case InteractionType.Shed:
                if (shedPlayerId == playerId) return false;
                break;
        }

        return true;
    }

    public void InteractStart(InteractionType type, int playerId)
    {
        // start effect


        // start progress
        Destroy(actionTimer);

        actionTimer = gameObject.AddComponent<SimpleTimer>();
        actionTimer.Begin(App.Instance.GameSettings.GetActionDuration(type));

        switch (type)
        {
            case InteractionType.Scratch:
                actionTimer.OnFinish += () => { Scratch(playerId); };
                break;
            case InteractionType.Piss:
                actionTimer.OnFinish += () => { pissPlayerId = playerId; };
                break;
            case InteractionType.Shed:
                actionTimer.OnFinish += () => { shedPlayerId = playerId; };
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private void Scratch(int playerId)
    {
        scratch = new ScratchData(playerId, scratch.ScratchAmount + 1);
    }

    public void InteractEnd(InteractionType type, int playerId)
    {
        // update visuals
    }

    private void UpdateVisuals(InteractionType type, int playerId)
    {
        Color color = App.Instance.GameSettings.GetPlayerColor(playerId);
        switch (type)
        {
            case InteractionType.Scratch:
                break;
            case InteractionType.Piss:
                break;
            case InteractionType.Shed:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}