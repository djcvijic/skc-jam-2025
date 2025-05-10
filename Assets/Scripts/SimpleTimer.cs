using System;
using UnityEngine;

public class SimpleTimer : MonoBehaviour
{
    public int timeRemaining;
    public bool isCountingDown = false;
    public Action OnTick;
    public Action OnFinish;

    public void Begin(int duration)
    {
        if (!isCountingDown)
        {
            isCountingDown = true;
            timeRemaining = duration;
            Invoke("Tick", 1f);
        }
    }

    private void Tick()
    {
        timeRemaining--;
        if (timeRemaining > 0)
        {
            Invoke("Tick", 1f);
        }
        else
        {
            isCountingDown = false;
            OnFinish?.Invoke();
            return;
        }

        OnTick?.Invoke();
    }
}