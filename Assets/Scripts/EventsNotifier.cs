using System;

public class EventsNotifier
{
    public Action<InteractionType, int> OnInteractionEnded;
    public Action OnGameOverTimerFinished;
    public Action<string, int> OnAnimationChange;

    public void NotifyInteractionEnded(InteractionType interactionType, int playerId)
        => OnInteractionEnded?.Invoke(interactionType, playerId);

    public void NotifyGameOver()
        => OnGameOverTimerFinished?.Invoke();

    public void TriggerAnimationChange(string animationName, int playerId)
        => OnAnimationChange.Invoke(animationName, playerId);

    public void ResetEvents()
    {
        OnInteractionEnded = null;
        OnGameOverTimerFinished = null;
        OnAnimationChange = null;
    }
}