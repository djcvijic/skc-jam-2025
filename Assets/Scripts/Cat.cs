using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cat : MonoBehaviour
{
    public int playerId;
    [SerializeField] Animator animator;
    [SerializeField] private ThoughtBubble thoughtBubble;
    
    private IInteractable interactingWith;

    public bool InMischief => interactingWith is { isInteracting: true };

    private Coroutine stunnedCoroutine;
    
    public bool IsStunned => stunnedCoroutine != null;

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
                break;
            case InputActionPhase.Canceled:
                Debug.Log($"Player {playerId} canceled {type.ToString()} on {interactingWith.gameObject.name}");
                interactingWith.InteractCancel(type, playerId);
                break;
        }
    }
    
    private void OnEnable()
    {
        App.Instance.Notifier.OnAnimationChange += HandleAnimationChange;
    }

    private void OnDisable()
    {
        App.Instance.Notifier.OnAnimationChange -= HandleAnimationChange;
    }

    private void HandleAnimationChange(string animationName, int id)
    {
        if (id == playerId)
        {
            animator.Play(animationName);
        }
    }

    public void Stun()
    {
        if (stunnedCoroutine != null)
            StopCoroutine(stunnedCoroutine);

        stunnedCoroutine = StartCoroutine(BeStunned());
    }

    private IEnumerator BeStunned()
    {
        var stunDuration = App.Instance.GameSettings.StunDuration;
        yield return new WaitForSeconds(stunDuration);

        stunnedCoroutine = null;
    }
}