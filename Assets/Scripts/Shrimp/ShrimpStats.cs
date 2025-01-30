using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ShrimpStats
{
    public string name;
    public bool gender;  // True = male, False = female
    public int age;
    public UInt16 hunger;  // Value from 0-100
    public UInt16 illness;  // Value from 0-100
    public UInt16 temperament;  // Value from 0-100

    public Trait geneticSize;
    public int actualSize;

    public Trait primaryColour;
    public Trait secondaryColour;
    public Trait type;
    public Trait eyes;
    public Trait pattern;
    public Trait tail;
    public Trait tailFan;
    public Trait antenna;
    public Trait face;
    public Trait frontLegs;

    public int fightHistory;
    public int breedingHistory;
    public int illnessHistory;
    public int moltHistory;
}