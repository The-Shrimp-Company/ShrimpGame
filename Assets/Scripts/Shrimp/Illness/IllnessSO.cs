using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All of the main information for the illnesses
[CreateAssetMenu(fileName = "Illness", menuName = "ScriptableObjects/Illness")]
public class IllnessSO : ScriptableObject
{
    [Header("Trigger")]
    public IllnessTriggers trigger;
    public AnimationCurve triggerChance;

    [Header("Symptoms")]
    public List<IllnessSymptoms> symptoms = new List<IllnessSymptoms>();

    [Header("Overall Illness Stat")]
    [Range(0, 100)] public float illnessImpact = 10;  // How much this illness will add to the overall shrimp illness stat every in game day
}


public enum IllnessSymptoms
{
    Discolouration,
    BodySize,
    Bubbles
}

public enum IllnessTriggers
{
    RandomChance,
    WaterQuality,
    Hunger,
    HotWater,
    ColdWater
}