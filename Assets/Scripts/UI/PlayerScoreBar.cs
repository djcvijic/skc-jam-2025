using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreBar : MonoBehaviour
{
    [SerializeField] private Slider playerScoreSlider;
    [SerializeField] private Image playerIcon;
    [SerializeField] private float lerpDuration = 0.5f;

    private Coroutine lerpRoutine;

    private void Start()
    {
        playerScoreSlider.maxValue = App.Instance.GameSettings.MaxPossibleScore;
    }

    public void OnScoreChanged(int newScore)
    {
        if (lerpRoutine != null)
            StopCoroutine(lerpRoutine);

        if (lerpRoutine != null)
            StopCoroutine(lerpRoutine);
        
        lerpRoutine = StartCoroutine(LerpScore(newScore));
    }

    private IEnumerator LerpScore(int targetScore)
    {
        float startValue = playerScoreSlider.value;
        float time = 0f;

        while (time < lerpDuration)
        {
            time += Time.deltaTime;
            float t = time / lerpDuration;
            playerScoreSlider.value = Mathf.Lerp(startValue, targetScore, t);
            yield return null;
        }

        playerScoreSlider.value = targetScore;
        lerpRoutine = null;
    }
}
