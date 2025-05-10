using UnityEngine;

public class Cat : PlayerInteractor
{
    IInteractable interactingWith;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
        {
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
}