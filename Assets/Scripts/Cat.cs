using UnityEngine;
using UnityEngine.InputSystem;

public class Cat : PlayerInteractor
{
    [SerializeField] private ThoughtBubble thoughtBubble;
    
    private IInteractable interactingWith;
    private InteractionType? interactionInProgress;

    public bool InMischief => interactionInProgress.HasValue;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponentInParent<IInteractable>();
        if (interactable != null)
        {
            Debug.Log($"Reached interactable: {interactable.gameObject.name}");
            interactable.ShowInteract(true, playerId);
            interactingWith = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponentInParent<IInteractable>();
        if (interactable != null)
        {
            interactable.ShowInteract(false, playerId);
            interactingWith = null;
        }
    }

    public void OnScratch(InputAction.CallbackContext context)
        => OnInteraction(InteractionType.Scratch, context);

    public void OnPiss(InputAction.CallbackContext context)
        => OnInteraction(InteractionType.Piss, context);

    public void OnShed(InputAction.CallbackContext context)
        => OnInteraction(InteractionType.Shed, context);

    private void OnInteraction(InteractionType type, InputAction.CallbackContext context)
    {
        if (interactingWith is null) return;

        if (!interactingWith.CanInteract(type, playerId)) return;
        
        switch (context.phase)
        {
            case InputActionPhase.Started:
                if (interactionInProgress.HasValue) break;

                if (!interactingWith.CanInteract(type, playerId)) break;

                Debug.Log($"Player {playerId} started {type.ToString()} on {interactingWith.gameObject.name}");
                interactionInProgress = type;
                interactingWith.InteractStart(type, playerId);
                break;
            case InputActionPhase.Canceled:
                Debug.Log($"Player {playerId} canceled {type.ToString()} on {interactingWith.gameObject.name}");
                interactingWith.InteractEnd(type, playerId);
                interactionInProgress = null;
                break;
        }
    }
}