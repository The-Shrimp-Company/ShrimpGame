using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Symptom
{
    public string symptomName;
    public Shrimp shrimp;
    public float severity;

    private float severityOverTime = 0.25f;

    public virtual void StartSymptom()
    {
        severity = 0;
    }


    public virtual void UpdateSymptom(float elapsedTime)
    {
        severity = Mathf.Clamp(severity + (severityOverTime * elapsedTime), 0, 100);
    }


    public virtual void EndSymptom()
    {

    }
}