using UnityEngine;
using UnityEngine.InputSystem;

public class Cat : PlayerInteractor
{
    private IInteractable interactingWith;

    public bool InMischief { get; private set; }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            Debug.Log($"Reached interactable: {interactable.gameObject.name}");
            interactable.ShowInteract(true, playerId);
            interactingWith = interactable;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
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