using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Symptom
{
    public IllnessSO so;
    public float timeTillSymptomShows;
    public bool symptomShowing;

    protected virtual void StartActivity()
    {

    }


    protected virtual void UpdateActivity()
    {

    }


    public virtual void EndActivity()
    {

    }
}