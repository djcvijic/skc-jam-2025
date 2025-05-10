using UnityEngine;
using UnityEngine.InputSystem;

public class Cat : PlayerInteractor
{
    private IInteractable interactingWith;

    public bool InMischief { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponentInParent<Chair>();
        if (interactable != null)
        {
            Debug.Log($"Reached interactable: {interactable.gameObject.name}");
            interactable.ShowInteract(true, playerId);
            interactingWith = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponentInParent<Chair>();
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
        if (InMischief || interactingWith is null) return;

        switch (context.phase)
        {
            case InputActionPhase.Started:
                InMischief = true;
                interactingWith.InteractStart(type, playerId);
                break;
            case InputActionPhase.Canceled:
                interactingWith.InteractEnd(type, playerId);
                InMischief = false;
                break;
        }
    }
}