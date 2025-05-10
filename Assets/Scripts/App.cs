using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    public static App Instance;
    public GameSettings GameSettings;
    public Prefabs Prefabs;
    
    [field: SerializeField] public AppCanvas AppCanvas { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        
        AppCanvas.InitCanvas();
    }
}