using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static Settings settings = new Settings();
}

[System.Serializable]
public struct Settings
{
    public float cameraSensitivity;
    public float volume;
}