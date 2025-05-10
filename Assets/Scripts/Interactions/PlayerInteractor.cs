using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private float InteractionRange => App.Instance.GameSettings.InteractionRange;
    protected int playerId;

    protected virtual void Update()
    {
    }
}