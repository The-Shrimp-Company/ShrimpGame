using System;
using System.Collections.Generic;
using UnityEngine;


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
    public Color colour = Color.white;
    public Color deadColour = Color.white;
    public Color discolourationColour = Color.white;

    [Header("Pattern Trait")]  // Only needs to be filled out if the trait is of type pattern
    public Material cherryPattern;
    public Material anomalisPattern;
    public Material carididPattern;
    public Material nylonPattern;

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