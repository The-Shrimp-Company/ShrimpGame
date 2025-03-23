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
    public int lifetimeInstances;
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