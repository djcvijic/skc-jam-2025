using UnityEngine;

using System;

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
    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var chair = other.GetComponentInParent<Chair>();
        if (chair != null)
        {
            Debug.Log($"Reached chair: {chair.gameObject.name}");
        }
    }
}