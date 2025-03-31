using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Symptom
{
    public string symptomName;
    public Shrimp shrimp;
    public IllnessSO so;
    public float severity;
    public float timeTillSymptomShows;
    public bool symptomShowing;

    public virtual void StartSymptom()
    {

    }


    public virtual void UpdateSymptom()
    {

    }


    public virtual void EndSymptom()
    {

    }
}