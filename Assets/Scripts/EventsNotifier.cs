using System;

public class EventsNotifier : MonoSingleton<EventsNotifier>
{
    public Action<InteractionType, int> OnInteractionEnded;
    public Action OnGameOverTimerFinished;

    public void NotifyInteractionEnded(InteractionType interactionType, int playerId)
        => OnInteractionEnded?.Invoke(interactionType, playerId);
    public void NotifyGameOver()
        => OnGameOverTimerFinished?.Invoke();
    

    public void ResetEvents()
    {
        OnInteractionEnded = null;
        OnGameOverTimerFinished = null;
    }
}