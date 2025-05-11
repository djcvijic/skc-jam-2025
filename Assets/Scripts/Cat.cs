using UnityEngine;
using UnityEngine.InputSystem;

public class Cat : PlayerInteractor
{
    [SerializeField] private ThoughtBubble thoughtBubble;
    
    private IInteractable interactingWith;

    public bool InMischief => interactingWith is { isInteracting: true };
    [SerializeField] Animator animator;

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
                if (InMischief) break;

                if (!interactingWith.CanInteract(type, playerId)) break;

                Debug.Log($"Player {playerId} started {type.ToString()} on {interactingWith.gameObject.name}");
                interactingWith.InteractStart(type, playerId);
                switch (type)
                {
                    case InteractionType.Scratch:
                        CatAnimationEventManager.TriggerAnimationChange("CatScratch", playerId);
                        break;
                    case InteractionType.Piss:
                        CatAnimationEventManager.TriggerAnimationChange("CatPiss", playerId);
                        break;
                    case InteractionType.Shed:
                        CatAnimationEventManager.TriggerAnimationChange("CatShed", playerId);
                        break;
                }
                break;
            case InputActionPhase.Canceled:
                Debug.Log($"Player {playerId} canceled {type.ToString()} on {interactingWith.gameObject.name}");
                interactingWith.InteractCancel(type, playerId);
                CatAnimationEventManager.TriggerAnimationChange("CatIdle", playerId);
                break;
        }
    }
    
    private void OnEnable()
    {
        CatAnimationEventManager.OnAnimationChange += HandleAnimationChange;
    }

    private void OnDisable()
    {
        CatAnimationEventManager.OnAnimationChange -= HandleAnimationChange;
    }

    private void HandleAnimationChange(string animationName, int id)
    {
        if (id == playerId)
        {
            animator.Play(animationName);
        }
    }
}