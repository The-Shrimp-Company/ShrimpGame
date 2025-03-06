using System.Collections.Generic;
using UnityEngine;


// The gene struct is specifically the gene information that will be saved for a shrimp, all other information is saved in the SO that you can get with the ID
[System.Serializable]
public struct Gene
{
    public string ID;
    public int dominance;
    public int value;
}


// Each trait for a shrimp contains two genes, the active one that is visible on the shrimp and an inactive one that is hidden
[System.Serializable]
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


// The different types of trait that a shrimp can have
public enum TraitType
{
    Colour,
    Pattern,
    body,
    head,
    eyes,
    tail,
    tailFan,
    antenna,
    legs
}

// Used for checking to see if a shrimp has matching parts and is a 'pure shrimp'
public enum TraitSet
{
    None,
    Cherry,
    Anomalis,
    Caridid,
    Nylon
}


// Some traits can have effects on other stats in the shrimp
public enum ModifierEffects
{
    temperament,
    geneticSize
}

[System.Serializable]
public struct Modifier
{
    public ModifierEffects type;
    [Range(-100, 100)] public float effect;  // How strong of an effect it will have on that stat. The stats are generally a value from 0-100, this will just add the effect to the stat that was already there
}


// All of the main information for the traits
[CreateAssetMenu(fileName = "Trait", menuName = "ScriptableObjects/Trait")]
public class TraitSO : ScriptableObject
{
    public string ID;  // Matches the trait with the saved genes, do not change once we are able to save the game
    public string traitName;
    public TraitSet set;

    [Space(10)]
    public int minValue;
    public int maxValue;

    [Space(10)]
    public int minDominance;
    public int maxDominance;

    [Space(20)]
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
 * B - Body
 * H - Head
 * E - Eye
 * T - Tail
 * F - Tail Fan
 * A - Antenna
 * L - Legs
 * 
 */