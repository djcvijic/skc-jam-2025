using UnityEngine;

public class SpriteProgressBar : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fillRenderer;
    [SerializeField] private float maxWidth = 1f;
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = fillRenderer.transform.localScale;
        maxWidth = originalScale.x;
    }

    public void SetProgress(float value)
    {
        value = Mathf.Clamp01(value);
        var scale = fillRenderer.transform.localScale;
        scale.x = maxWidth * value;
        fillRenderer.transform.localScale = scale;
    }
}