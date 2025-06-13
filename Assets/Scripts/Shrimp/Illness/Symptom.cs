using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Symptom
{
    public IllnessSymptoms symptom;
    public Shrimp shrimp;
    public float severity;

    private float severityOverTime = 1f;

    public virtual void StartSymptom()
    {
        severity = 0;
    }


    public virtual void UpdateSymptom(float elapsedTime)
    {
        severity = Mathf.Clamp(severity + (severityOverTime * elapsedTime * shrimp.illnessCont.severityCurve.Evaluate(severity)), 0, 100);
        shrimp.stats.illnessLevel = Mathf.Clamp(shrimp.stats.illnessLevel + severity, 0, 100);
    }


    public virtual void EndSymptom()
    {

    }
}