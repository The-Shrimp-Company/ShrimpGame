using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illness : MonoBehaviour
{
    public IllnessSO so;
}

public enum IllnessTriggers
{
    WaterQuality,
    Hunger,
    Temperature,
    Overcrowded
}

public enum IllnessSymptoms
{
    WhiteRing,
    Discolouration,
    LowSpeed,
    NotEating,
    EmittingBubbles
}