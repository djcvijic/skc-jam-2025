using System;
using UnityEngine;

public class App : MonoSingleton<App>
{
    public GameSettings GameSettings;
    public Prefabs Prefabs;

    [field: SerializeField] public FoteljaAudioManager AudioManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        EventsNotifier.Instance.ResetEvents();
    }
}