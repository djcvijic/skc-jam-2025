using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prefabs", menuName = "Prefabs")]
public class Prefabs : ScriptableObject
{
    [field: SerializeField] public MainMenu MainMenuUi { get; private set; }
}
