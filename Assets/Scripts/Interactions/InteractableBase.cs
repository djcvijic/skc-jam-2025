using UnityEngine;

public interface IInteractable
{
    public GameObject gameObject { get; }
    void ShowInteract(bool show, int playerId);
    bool CanInteract(InteractionType type, int playerId);
    void InteractStart(InteractionType type, int playerId);
    void InteractEnd(InteractionType type, int playerId);
}