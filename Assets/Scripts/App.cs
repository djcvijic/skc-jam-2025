public class App : MonoSingleton<App>
{
    public GameSettings GameSettings;
    public Prefabs Prefabs;
    
    private void Awake()
    {
        EventsNotifier.Instance.ResetEvents();
    }

}