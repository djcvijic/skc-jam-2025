using UnityEngine;

public class Chair : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer pissImage;
    [SerializeField] private SpriteRenderer scratchImage;
    [SerializeField] private SpriteRenderer shedImage;
    [SerializeField] private SpriteRenderer outlineImage;
    [SerializeField] private SpriteProgressBar progressBar;

    private int pissPlayerId = -1;
    private int shedPlayerId = -1;

    private record ScratchData(int PlayerId, int ScratchAmount);

    private ScratchData scratch = new(0, 0);

    private bool isInteracting;
    private SimpleTimer actionTimer;

    public void ShowInteract(bool show, int playerId)
    {
        if (!show) progressBar.gameObject.SetActive(false);
        if (isInteracting && show) return;

        outlineImage.gameObject.SetActive(show);
        outlineImage.color = App.Instance.GameSettings.GetPlayerColor(playerId);
    }

    public bool CanInteract(InteractionType type, int playerId)
    {
        switch (type)
        {
            case InteractionType.Scratch:
                if (scratch.PlayerId == playerId) return false;
                if (scratch.ScratchAmount >= App.Instance.GameSettings.MaxScratchAmount) return false;
                break;
            case InteractionType.Piss:
                if (pissPlayerId == playerId) return false;
                break;
            case InteractionType.Shed:
                if (shedPlayerId == playerId || shedPlayerId != -1) return false;
                break;
        }

        return true;
    }

    public void InteractStart(InteractionType type, int playerId)
    {
        isInteracting = true;

        // start effect
        // ParticleSystem.Instantiate();

        // start progress
        Destroy(actionTimer);

        actionTimer = gameObject.AddComponent<SimpleTimer>();
        actionTimer.Begin(App.Instance.GameSettings.GetActionDuration(type));
        actionTimer.OnTick += (progress) => { progressBar.SetProgress(progress); };

        switch (type)
        {
            case InteractionType.Scratch:
                actionTimer.OnFinish += () =>
                {
                    Scratch(playerId);
                    UpdateVisuals(InteractionType.Scratch, playerId);
                };
                break;
            case InteractionType.Piss:
                actionTimer.OnFinish += () =>
                {
                    pissPlayerId = playerId;
                    UpdateVisuals(InteractionType.Piss, playerId);
                };
                break;
            case InteractionType.Shed:
                actionTimer.OnFinish += () =>
                {
                    shedPlayerId = playerId;
                    UpdateVisuals(InteractionType.Shed, playerId);
                };
                break;
        }

        progressBar.gameObject.SetActive(true);
    }

    private void Scratch(int playerId)
    {
        scratch = new ScratchData(playerId, scratch.ScratchAmount + 1);
    }

    public void InteractEnd(InteractionType type, int playerId)
    {
        Debug.Log("Interaction ended");
        isInteracting = false;
        EventsNotifier.Instance.NotifyInteractionEnded(type, playerId);
        // update visuals
        progressBar.gameObject.SetActive(false);
    }

    private void UpdateVisuals(InteractionType type, int playerId)
    {
        Sprite actionSprite = App.Instance.GameSettings.GetPlayerActionSprite(type, playerId);
        switch (type)
        {
            case InteractionType.Scratch:
                scratchImage.gameObject.SetActive(true);
                scratchImage.sprite = actionSprite;
                break;
            case InteractionType.Piss:
                pissImage.gameObject.SetActive(true);
                pissImage.sprite = actionSprite;
                break;
            case InteractionType.Shed:
                shedImage.gameObject.SetActive(true);
                shedImage.sprite = actionSprite;
                break;
        }
    }

    public int GetScoreForPlayer(int playerId)
    {
        var pissValue = playerId == pissPlayerId ? App.Instance.GameSettings.pissPoints : 0;
        var scratchValue = playerId == scratch.PlayerId ? App.Instance.GameSettings.scratchPoints * scratch.ScratchAmount : 0;
        var shedValue = playerId == shedPlayerId ? App.Instance.GameSettings.shedPoints : 0;

        return pissValue + scratchValue + shedValue;
    }
    
}