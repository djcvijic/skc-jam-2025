using System;
using UnityEngine;

public class SimpleTimer : MonoBehaviour
{
    public float timeRemaining;
    public int duration;
    public bool isCountingDown = false;
    public Action<float> OnTick;
    public Action OnFinish;

    public void Begin(int duration)
    {
        if (!isCountingDown)
        {
            isCountingDown = true;
            this.duration = duration;
            timeRemaining = duration;
            Invoke("Tick", 0.1f);
        }
    }

    private void Tick()
    {
        timeRemaining -= 0.1f;
        if (timeRemaining > 0)
        {
            Invoke("Tick", 0.1f);
        }
        else
        {
            isCountingDown = false;
            OnFinish?.Invoke();
            Debug.Log("Moze FINISH");
            return;
        }

        OnTick?.Invoke(((float)duration - (float)timeRemaining) / (float)duration);
    }
}