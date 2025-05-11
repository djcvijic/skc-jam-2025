using System;

public class EventsNotifier
{
    public event Action<InteractionType, int> OnInteractionEnded;
    public event Action OnGameOverTimerFinished;
    public event Action<string, int> OnAnimationChange;
    public event Action<bool> GrannyAttack;

    public void NotifyInteractionEnded(InteractionType interactionType, int playerId)
        => OnInteractionEnded?.Invoke(interactionType, playerId);

    public void NotifyGameOver()
        => OnGameOverTimerFinished?.Invoke();

    public void TriggerAnimationChange(string animationName, int playerId)
        => OnAnimationChange?.Invoke(animationName, playerId);

    public void TriggerGrannyFight(bool started)
        => GrannyAttack?.Invoke(started);

    public void ResetEvents()
    {
        OnInteractionEnded = null;
        OnGameOverTimerFinished = null;
        OnAnimationChange = null;
        GrannyAttack = null;
    }
}