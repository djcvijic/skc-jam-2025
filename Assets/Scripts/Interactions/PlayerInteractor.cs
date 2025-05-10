using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private readonly float interactionRange = App.Instance.GameSettings.InteractionRange;
    protected int playerId;

    private void Update()
    {
    }
}