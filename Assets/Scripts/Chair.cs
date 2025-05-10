using System;
using UnityEngine;

public class Chair : MonoBehaviour, IInteractable
{
    private int linjPlayerId;
    private Tuple<int,int> scratch;
    private int pissPlayerId;

    public void InteractStart(InteractionType type, int playerId)
    {
        throw new NotImplementedException();
    }

    public void InteractEnd(InteractionType type, int playerId)
    {
        throw new NotImplementedException();
    }
}