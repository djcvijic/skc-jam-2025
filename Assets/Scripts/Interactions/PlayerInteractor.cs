using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private float InteractionRange => App.Instance.GameSettings.InteractionRange;
    private int id;

    protected virtual void Update()
    {
    }
}