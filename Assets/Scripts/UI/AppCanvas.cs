using System;
using UnityEngine;

public class AppCanvas : MonoBehaviour
{
    public void InitCanvas()
    {
        Instantiate(App.Instance.Prefabs.MainMenuUi, transform);
    }
}