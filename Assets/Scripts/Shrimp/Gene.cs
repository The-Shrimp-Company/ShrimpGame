using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Gene
{
    public int ID; 
    public string name;
    public int dominance;
    public int value;
}

public struct Trait
{
    public Gene activeGene;
    public Gene inactiveGene;
}