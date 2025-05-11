using UnityEngine;

public interface IInteractable
{
    public GameObject gameObject { get; }
    public bool isInteracting { get; }
    void ShowInteract(bool show, int playerId);
    bool CanInteract(InteractionType type, int playerId);
    void InteractStart(InteractionType type, int playerId);
    void InteractCancel(InteractionType type, int playerId);
}