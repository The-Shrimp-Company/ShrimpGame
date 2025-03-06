using System.Collections.Generic;
using UnityEngine;


public struct Gene
{
    public string ID;
    public int dominance;
    public int value;
}


public struct Trait
{
    public Gene activeGene;
    public Gene inactiveGene;

    public Trait(Gene a, Gene i)
    {
        this.activeGene = a;
        this.inactiveGene = i;
    }
}


public enum TraitType
{
    Colour,
    Pattern,
    BodyPart
}

public enum TraitSet
{
    Cherry,
    Anomalis,
    Caridid,
    Nylon
}

public enum ModifierEffects
{
    temperament,
    geneticSize
}

[System.Serializable]
public struct Modifier
{
    public ModifierEffects type;
    [Range(-100, 100)] public float effect;
}


[CreateAssetMenu(fileName = "Trait", menuName = "ScriptableObjects/Trait")]
public class TraitSO : ScriptableObject
{
    public string ID;  // Matches the trait with the saved genes
    public string traitName;

    [Space(10)]
    public int minValue;
    public int maxValue;

    [Space(30)]
    public List<Modifier> statModifiers;

    [Space(30)]
    public TraitType type;

    [Header("Colour Trait")]  // Only needs to be filled out if the trait is of type colour
    public Color color = Color.white;

    [Header("Pattern Trait")]  // Only needs to be filled out if the trait is of type pattern
    public Sprite texture;

    [Header("Body Part Trait")]  // Only needs to be filled out if the trait is of type body part
    public GameObject part;
}


/*
 * List of traits in manager
 * Create structs in manager for each trait to save values
 * Function to get the SO using the trait ID
 * Function to get the manager version of the trait using the trait ID
 * Modifiers
 */



/*
 * 
 * IDs
 * 
 * Prefix:
 * C - Colour
 * P - Pattern
 * B - Body Part
 * 
 */