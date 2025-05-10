using UnityEngine;

public class PlayerInteractor : MonoBehaviour, IInteractor
{
    private readonly float interactionRange = App.Instance.GameSettings.InteractionRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TryInteract();
    }

    private void TryInteract()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (!Physics.Raycast(ray, out RaycastHit hit, interactionRange)) 
            return;

        if (!hit.collider.TryGetComponent<IInteractable>(out var interactable))
            return;

        if (!interactable.CanInteract(this)) 
            return;
        
        interactable.Interact(this);
    }

    public Transform GetTransform()
    {
        return transform;
    }
}