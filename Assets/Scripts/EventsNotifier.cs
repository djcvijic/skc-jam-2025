using System;

public class EventsNotifier
{
    public event Action<bool> EventExample;

    public void NotifyGraceChanged(bool value)
        => EventExample?.Invoke(value);
}