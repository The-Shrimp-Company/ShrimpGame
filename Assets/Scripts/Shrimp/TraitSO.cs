using System;
using System.Collections.Generic;
using UnityEngine;


// The gene struct is specifically the gene information that will be saved for a shrimp, all other information is saved in the SO that you can get with the ID
[System.Serializable]
public struct Gene
{
    public string ID;
    public int dominance;
}


// Information that applies to every instance of a gene, it is saved in the gene manager
[System.Serializable]
public struct GlobalGene
{
    public string ID;
    public int dominance;
    public float startingValue;
    public float currentValue;
    public int instancesInStore;
}


// Each trait for a shrimp contains two genes, the active one that is visible on the shrimp and an inactive one that is hidden
[System.Serializable]
public struct Trait 
{
    public Gene activeGene;
    public Gene inactiveGene;
    public bool obfuscated;

    public Trait(Gene a, Gene i)
    {
        this.activeGene = a;
        this.inactiveGene = i;
        obfuscated = false;
    }
    public Trait(GlobalGene a, GlobalGene i)
    {
        this.activeGene = GeneManager.instance.GlobalGeneToGene(a);
        this.inactiveGene = GeneManager.instance.GlobalGeneToGene(i);
        obfuscated = false;
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
    [Range(-100, 100)] public int effect;  // How strong of an effect it will have on that stat. The stats are generally a value from 0-100, this will just add the effect to the stat that was already there
}


// All of the main information for the traits
[CreateAssetMenu(fileName = "Trait", menuName = "ScriptableObjects/Trait")]
public class TraitSO : ScriptableObject
{
    public string ID;  // Matches the trait with the saved genes, do not change once we are able to save the game
    public string traitName;
    public TraitSet set;

    [Space(10)]
    public int weightDominanceTowards;

    [Space(20)]
    public List<Modifier> statModifiers;

    [Space(30)]
    public TraitType type;

    [Header("Colour Trait")]  // Only needs to be filled out if the trait is of type colour
    public Color color = Color.white;

    [Header("Pattern Trait")]  // Only needs to be filled out if the trait is of type pattern
    public Material texture;

    [Header("Body Part Trait")]  // Only needs to be filled out if the trait is of type body part
    public GameObject part;
}



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