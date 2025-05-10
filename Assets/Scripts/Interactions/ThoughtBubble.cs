using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThoughtBubble : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bubbleRenderer;
    [SerializeField] private Vector3 offset = new(0, 1.5f, 0);
    private float DisplayDuration =>  App.Instance.GameSettings.ThoughBubbleDisplayDuration;

    [SerializeField] private List<EmotionSprite> emotionSpriteMap;

    [Serializable]
    private struct EmotionSprite
    {
        [field: SerializeField] public EmotionType Emotion { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
    
    private Transform currentTarget;
    private float timer;

    private void Awake()
    {
        bubbleRenderer.enabled = false;
    }

    private void Update()
    {
        if (currentTarget == null || !bubbleRenderer.enabled)
            return;

        transform.position = currentTarget.position + offset;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            HideBubble();
        }
    }

    public void ShowBubble(Transform target, EmotionType emotion)
    {
        var bubbleSprite = GetSpriteFromEmotion(emotion);

        currentTarget = target;
        bubbleRenderer.sprite = bubbleSprite;
        transform.localPosition = offset;
        bubbleRenderer.enabled = true;
        timer = DisplayDuration;
    }

    private Sprite GetSpriteFromEmotion(EmotionType emotion)
    {
        return emotionSpriteMap.FirstOrDefault(emotionSprite => emotionSprite.Emotion == emotion).Sprite;
    }

    private void HideBubble()
    {
        bubbleRenderer.enabled = false;
        currentTarget = null;
    }
}


public enum EmotionType
{
    Happy,
    Sad,
    Angry,
    Confused,
    Surprised
}

