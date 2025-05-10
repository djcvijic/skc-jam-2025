using System;
using UnityEngine;
using UnityEngine.UI;

public class Chair : MonoBehaviour, IInteractable
{
    [SerializeField] private Image pissImage;
    [SerializeField] private Image scratchImage;
    [SerializeField] private Image linjImage;

    private int linjPlayerId;

    /// <summary>
    /// 1 - player id | 2 - scratchAmount
    /// </summary>
    private Tuple<int, int> scratch;

    private int pissPlayerId;

    private SimpleTimer actionTimer;

    public bool CanInteract(InteractionType type, int playerId)
    {
        switch (type)
        {
            case InteractionType.Scrach:
                if (scratch.Item1 == playerId) return false;
                if (scratch.Item2 >= App.Instance.GameSettings.MaxScratchAmount) return false;
                break;
            case InteractionType.Piss:
                if (pissPlayerId == playerId) return false;
                break;
            case InteractionType.Linj:
                if (linjPlayerId == playerId) return false;
                break;
            default:
                return false;
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
            case InteractionType.Scrach:
                actionTimer.OnFinish += () => { Scratch(playerId); };
                break;
            case InteractionType.Piss:
                actionTimer.OnFinish += () => { pissPlayerId = playerId; };
                break;
            case InteractionType.Linj:
                actionTimer.OnFinish += () => { linjPlayerId = playerId; };
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private void Scratch(int playerId)
    {
        var newTuple = new Tuple<int, int>(playerId, scratch.Item2+1);
        scratch = newTuple;
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
            case InteractionType.Scrach:
                break;
            case InteractionType.Piss:
                break;
            case InteractionType.Linj:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}