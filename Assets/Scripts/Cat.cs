using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cat : MonoBehaviour
{
    public int playerId;
    [SerializeField] Animator animator;
    [SerializeField] private ThoughtBubble thoughtBubble;
    [SerializeField] private Transform sprite;
    
    private readonly Vector3 upsideDown = new(1, -1, 1);
    
    private IInteractable interactingWith;
    private InteractionType currentInteractionType;

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
        if (IsStunned) return;

        if (interactingWith is null) return;

        if (!interactingWith.CanInteract(type, playerId)) return;
        
        switch (context.phase)
        {
            case InputActionPhase.Started:
                if (InMischief) break;

                if (!interactingWith.CanInteract(type, playerId)) break;

                Debug.Log($"Player {playerId} started {type.ToString()} on {interactingWith.gameObject.name}");
                interactingWith.InteractStart(type, playerId);
                currentInteractionType = type;
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
        App.Instance.Notifier.GrannyAttack += HandleGrannyAttack;
    }

    private void OnDisable()
    {
        App.Instance.Notifier.OnAnimationChange -= HandleAnimationChange;
        App.Instance.Notifier.GrannyAttack -= HandleGrannyAttack;
    }

    private void HandleAnimationChange(string animationName, int pid)
    {
        if (pid != playerId) return;

        if (IsStunned) return;

        animator.Play(animationName);
    }

    private void HandleGrannyAttack(int pid)
    {
        if (pid != playerId) return;

        if (stunnedCoroutine != null)
            StopCoroutine(stunnedCoroutine);

        stunnedCoroutine = StartCoroutine(Stun());
    }

    private IEnumerator Stun()
    {
        interactingWith?.InteractCancel(currentInteractionType, playerId);
        sprite.localScale = upsideDown;
        App.Instance.AudioManager.CatFight();

        var stunDuration = App.Instance.GameSettings.StunDuration;
        yield return new WaitForSeconds(stunDuration);

        sprite.localScale = Vector3.one;
        stunnedCoroutine = null;
    }
}