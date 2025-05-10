using System;

public class EventsNotifier : MonoSingleton<EventsNotifier>
{
    public Action<InteractionType, int> OnInteractionEnded;
    public void NotifyInteractionEnded(InteractionType interactionType, int playerId)
        => OnInteractionEnded?.Invoke(interactionType, playerId);


    public void ResetEvents()
    {
        OnInteractionEnded = null;
    }
}