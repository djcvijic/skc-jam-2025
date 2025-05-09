using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    public static App Instance;
    public GameSettings GameSettings;

    private void Start()
    {
        Instance = this;
    }
}