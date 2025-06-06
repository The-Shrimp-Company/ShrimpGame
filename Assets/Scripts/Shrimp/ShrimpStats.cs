using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ShrimpStats
{
    public string name;
    public bool gender;  // True = male, False = female
    public float birthTime;  // The time at the point of the shrimp's birth
    public float hunger;  // Value from 0-100
    public float illnessLevel;  // Value from 0-100
    public int temperament;  // Value from 0-100

    public int salineLevel; // Value from 0-100, starting at 50
    public int immunity; // Value from 0-100
    public int metabolism; // Value from 0-100
    public int filtration; // Value from 0-100
    public int temperaturePreference; // Value from 0-100, starting at 50
    
          
    public int geneticSize;
    public int actualSize;

    public Trait primaryColour;
    public Trait secondaryColour;
    public Trait body;
    public Trait head;
    public Trait eyes;
    public Trait pattern;
    public Trait tail;
    public Trait tailFan;
    public Trait legs;

    public int fightHistory;
    public int breedingHistory;
    public int illnessHistory;
    public int moltHistory;
    public bool canBreed;

    public bool[] illness;
    public float[] symptoms;
}
