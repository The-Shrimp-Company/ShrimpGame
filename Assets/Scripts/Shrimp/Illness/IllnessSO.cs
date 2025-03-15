using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All of the main information for the illnesses
[CreateAssetMenu(fileName = "Illness", menuName = "ScriptableObjects/Illness")]
public class IllnessSO : ScriptableObject
{
    public Illness illnessScript;

    [Header("Trigger")]
    public IllnessTriggers trigger;
    public AnimationCurve triggerChance;

    [Header("Overall Illness Stat")]
    [Range(0, 100)] public float illnessImpact;
}