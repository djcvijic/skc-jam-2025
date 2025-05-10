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

        isInteracting = show;
    }

    public bool CanInteract(InteractionType type, int playerId)
    {
        if (isInteracting) return false;

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
                if (shedPlayerId == playerId) return false;
                break;
        }

        return true;
    }

    public void InteractStart(InteractionType type, int playerId)
    {
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
        // update visuals
        progressBar.gameObject.SetActive(false);
    }

    private void UpdateVisuals(InteractionType type, int playerId)
    {
        Color color = App.Instance.GameSettings.GetPlayerColor(playerId);
        switch (type)
        {
            case InteractionType.Scratch:
                scratchImage.gameObject.SetActive(true);
                scratchImage.color = color;
                break;
            case InteractionType.Piss:
                pissImage.gameObject.SetActive(true);
                pissImage.color = color;
                break;
            case InteractionType.Shed:
                shedImage.gameObject.SetActive(true);
                shedImage.color = color;
                break;
        }
    }
}