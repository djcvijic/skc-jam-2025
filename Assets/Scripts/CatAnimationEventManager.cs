using System;
using UnityEngine;

public static class CatAnimationEventManager
{
    public static event Action<string, int> OnAnimationChange;

    public static void TriggerAnimationChange(string animationName, int playerId)
    {
        OnAnimationChange?.Invoke(animationName, playerId);
    }
}