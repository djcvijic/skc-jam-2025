using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    public virtual bool CanInteract(IInteractor interactor) => true;

    public abstract void Interact(IInteractor interactor);
}

public interface IInteractable
{
    void Interact(IInteractor interactor);
    bool CanInteract(IInteractor interactor);
}

public interface IInteractor
{
    Transform GetTransform();
}