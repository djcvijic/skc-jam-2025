using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Extensions;

public class ThoughtBubble : MonoBehaviour
{
    [SerializeField] private SpriteRenderer bubbleRenderer;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0);
    [SerializeField] private float DisplayDuration =>  App.Instance.GameSettings.ThoughBubbleDisplayDuration;

    [SerializeField] private List<SerializedKeyValuePair<EmotionType, Sprite>> emotionSpriteMap;
    
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
        return emotionSpriteMap.FirstOrDefault((emotionSprite) => emotionSprite.Key == emotion).Value;
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

