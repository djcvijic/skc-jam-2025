public interface IInteractable
{
    void InteractStart(InteractionType type, int playerId);
    void InteractEnd(InteractionType type, int playerId);
}