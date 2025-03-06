using System.Collections.Generic;
using UnityEngine;

public enum InheritanceType
{
    FullRandom,
    WeightedRandom,
    Punnett,
    FlatAverage
}

public class GeneManager : MonoBehaviour
{
    public static GeneManager instance;


    [SerializeField] [Range(0, 100)] int geneWeightingPercentage = 10;  // When using weighted random, how much should it be weighted by?
    [SerializeField][Range(0, 100)] float mutationChance = 5;  // Will pick a random number under 100, if it is under this, the trait will mutate

    [Header("Inheritance")]
    public InheritanceType sizeInheritance = InheritanceType.WeightedRandom;
    public InheritanceType temperamentInheritance = InheritanceType.WeightedRandom;
    public InheritanceType colourInheritance = InheritanceType.Punnett;
    public InheritanceType patternInheritance = InheritanceType.Punnett;
    public InheritanceType bodyPartInheritance = InheritanceType.Punnett;

    [Header("Mutation")]
    public bool sizeCanMutate;
    public bool temperamentCanMutate;
    public bool colourCanMutate = true;
    public bool patternCanMutate = true;
    public bool bodyPartCanMutate = true;

    [Header("Traits")]
    public List<TraitSO> colourSOs = new List<TraitSO>();
    public List<TraitSO> patternSOs = new List<TraitSO>();
    public List<TraitSO> bodySOs = new List<TraitSO>();
    public List<TraitSO> headSOs = new List<TraitSO>();
    public List<TraitSO> eyeSOs = new List<TraitSO>();
    public List<TraitSO> tailSOs = new List<TraitSO>();
    public List<TraitSO> tailFanSOs = new List<TraitSO>();
    public List<TraitSO> antennaSOs = new List<TraitSO>();
    public List<TraitSO> legsSOs = new List<TraitSO>();

    private List<Gene> loadedGlobalGenes = new List<Gene>();



    public void Awake()
    {
        instance = this;
        LoadGenes();
    }


    private bool FullyRandomBool()
    {
        return (Random.value > 0.5f);
    }


    private int FullyRandomInt(int upperBound)
    {
        return (Random.Range(0, upperBound + 1));
    }


    private int WeightedRandomInt(int upperBound, float weight, int parentAVal, int parentBVal)
    {
        float val = Random.Range(0, upperBound + 1);
        weight = weight / 100;  // Convert percentage to decimal
        val += (((parentAVal + parentBVal) / 2) - val) * weight;
        return Mathf.RoundToInt(val);
    }


    private Trait FullyRandomTrait(Trait parentATrait, Trait parentBTrait)
    {
        char type = parentATrait.activeGene.ID[0];
        Trait t = new Trait();

        if (type == 'C')
            t = RandomTraitsFromList(colourSOs);

        else if (type == 'P')
            t = RandomTraitsFromList(patternSOs);

        else if (type == 'B')
            t = RandomTraitsFromList(bodySOs);

        else if (type == 'H')
            t = RandomTraitsFromList(headSOs);

        else if (type == 'E')
            t = RandomTraitsFromList(eyeSOs);

        else if (type == 'T')
            t = RandomTraitsFromList(tailSOs);

        else if (type == 'F')
            t = RandomTraitsFromList(tailFanSOs);

        else if (type == 'A')
            t = RandomTraitsFromList(antennaSOs);

        else if (type == 'L')
            t = RandomTraitsFromList(legsSOs);

        else
            Debug.Log("ID prefix " + type + " could not be found");

        if (t.activeGene.ID == null || t.activeGene.ID == "")
        {
            Debug.LogError("Fully Random Trait using ID " + t.activeGene.ID + " could not be found");
        }

        return t;
    }


    private Trait WeightedRandomTrait(Trait parentATrait, Trait parentBTrait)
    {
        char type = parentATrait.activeGene.ID[0];
        Trait t = new Trait();

        if (type == 'C')
            t = WeightedRandomTraitsFromList(colourSOs);

        else if (type == 'P')
            t = WeightedRandomTraitsFromList(patternSOs);

        else if (type == 'B')
            t = WeightedRandomTraitsFromList(bodySOs);

        else if (type == 'H')
            t = WeightedRandomTraitsFromList(headSOs);

        else if (type == 'E')
            t = WeightedRandomTraitsFromList(eyeSOs);

        else if (type == 'T')
            t = WeightedRandomTraitsFromList(tailSOs);

        else if (type == 'F')
            t = WeightedRandomTraitsFromList(tailFanSOs);

        else if (type == 'A')
            t = WeightedRandomTraitsFromList(antennaSOs);

        else if (type == 'L')
            t = WeightedRandomTraitsFromList(legsSOs);

        else
            Debug.Log("ID prefix " + type + " could not be found");

        if (t.activeGene.ID == null || t.activeGene.ID == "")
        {
            Debug.LogError("Weighted Random Trait using ID " + t.activeGene.ID + " could not be found");
        }

        return t;
    }


    private Trait PunnetSquareTrait(Trait parentATrait, Trait parentBTrait)
    {
        Gene AA = parentATrait.activeGene;
        Gene AB = parentATrait.inactiveGene;
        Gene BA = parentBTrait.activeGene;
        Gene BB = parentBTrait.inactiveGene;


        Trait[] c = new Trait[4];
        c[0] = new Trait(AA, BA);
        c[1] = new Trait(AA, BB);
        c[2] = new Trait(AB, BA);
        c[3] = new Trait(AB, BB);

        int[] d = new int[4];
        d[0] = c[0].activeGene.dominance + c[0].inactiveGene.dominance;
        d[1] = c[1].activeGene.dominance + c[1].inactiveGene.dominance;
        d[2] = c[2].activeGene.dominance + c[2].inactiveGene.dominance;
        d[3] = c[3].activeGene.dominance + c[3].inactiveGene.dominance;

        // Find the total dominance of all possible options and pick a random number below that
        int totalDominance = d[0] + d[1] + d[2] + d[3];
        int rand = Random.Range(0, totalDominance + 1);

        int chosenTrait;

        // 
        if (rand <= d[0])  // Trait c0 has been chosen (AA, BA)
            chosenTrait = 0;

        else if (rand <= d[0] + d[1])  // Trait c1 has been chosen (AA, BB)
            chosenTrait = 1;

        else if (rand <= d[0] + d[1] + d[2])  // Trait c2 has been chosen (AB, BA)
            chosenTrait = 2;

        else  // Trait c3 has been chosen (AB, BB)
            chosenTrait = 3;




        Trait RT;  // Trait to return
        if (c[chosenTrait].activeGene.dominance >= c[chosenTrait].inactiveGene.dominance)  // If gene A is more dominant, set it as the active gene
        {
            RT.activeGene = c[chosenTrait].activeGene; 
            RT.inactiveGene = c[chosenTrait].inactiveGene;
        }
        else
        {
            RT.activeGene = c[chosenTrait].inactiveGene;
            RT.inactiveGene = c[chosenTrait].activeGene;
        }
        return RT;
    }


    private int FlatAverageInt(int parentAVal, int parentBVal)
    {
        return Mathf.RoundToInt((parentAVal + parentBVal) / 2);
    }


    public bool RandomGender()
    {
        return FullyRandomBool();  // 0 = Female, 1 = Male
    }


    public int IntGene(InheritanceType type, int upperBound, int parentAVal, int parentBVal, bool canMutate)
    {
        if (Random.value * 100 < mutationChance)  // Gene mutation
        {
            type = InheritanceType.FullRandom;
        }

        switch (type)
        {
            case InheritanceType.FullRandom:
            {
                return FullyRandomInt(upperBound);
            }

            case InheritanceType.WeightedRandom:
            {
                return WeightedRandomInt(upperBound, geneWeightingPercentage, parentAVal, parentBVal);
            }

            case InheritanceType.Punnett:
            {
                Debug.LogError("Punnet squares are not supported for integer genes, please ask Aaron to implement this");
                return FullyRandomInt(upperBound);
            }

            case InheritanceType.FlatAverage:
            {
                return FlatAverageInt(parentAVal, parentBVal);
            }

            default:  // Error case
            {
                return FullyRandomInt(upperBound);
            }
        }
    }


    public Trait TraitGene(InheritanceType type, int upperBound, Trait parentAVal, Trait parentBVal, bool canMutate)
    {
        if (Random.value * 100 < mutationChance)  // Gene mutation
        {
            type = InheritanceType.WeightedRandom;
        }

        switch (type)
        {
            case InheritanceType.FullRandom:
            {
                return FullyRandomTrait(parentAVal, parentBVal);
            }

            case InheritanceType.WeightedRandom:
            {
                return WeightedRandomTrait(parentAVal, parentBVal);
            }

            case InheritanceType.Punnett:
            {
                return PunnetSquareTrait(parentAVal, parentBVal);
            }

            case InheritanceType.FlatAverage:
            {
                Debug.LogError("Flat Average is not supported for traits, please ask Aaron to implement this");
                return PunnetSquareTrait(parentAVal, parentBVal);
            }

            default:  // Error case
            {
                return PunnetSquareTrait(parentAVal, parentBVal);
            }
        }
    }



    private void LoadGenes()
    {
        loadedGlobalGenes.Clear();

        LoadTraitType(colourSOs);
        LoadTraitType(patternSOs);
        LoadTraitType(bodySOs);
        LoadTraitType(headSOs);
        LoadTraitType(eyeSOs);
        LoadTraitType(tailSOs);
        LoadTraitType(tailFanSOs);
        LoadTraitType(antennaSOs);
        LoadTraitType(legsSOs);
    }

    private void LoadTraitType(List<TraitSO> l)
    {
        foreach (TraitSO t in l)
        {
            Gene g = new Gene();
            g.ID = t.ID;
            g.dominance = (t.minDominance + t.maxDominance) / 2;
            g.value = (t.minValue + t.maxValue) / 2;
            loadedGlobalGenes.Add(g);
        }
    }


    public TraitSO GetTraitSO(string ID)
    {
        TraitSO t = null;

        if (ID != null && ID != "")
        {
            if (ID[0] == 'C')
                t = GetSOFromList(colourSOs, ID);

            else if (ID[0] == 'P')
                t = GetSOFromList(patternSOs, ID);

            else if (ID[0] == 'B')
                t = GetSOFromList(bodySOs, ID);

            else if (ID[0] == 'H')
                t = GetSOFromList(headSOs, ID);

            else if (ID[0] == 'E')
                t = GetSOFromList(eyeSOs, ID);

            else if (ID[0] == 'T')
                t = GetSOFromList(tailSOs, ID);

            else if (ID[0] == 'F')
                t = GetSOFromList(tailFanSOs, ID);

            else if (ID[0] == 'A')
                t = GetSOFromList(antennaSOs, ID);

            else if (ID[0] == 'L')
                t = GetSOFromList(legsSOs, ID);

            else
                Debug.Log("ID " + ID + " prefix could not be found");
        }


        if (t == null)
        {
            Debug.LogError("Trait SO with ID " + ID + " could not be found");
        }

        return t;
    }

    private TraitSO GetSOFromList(List<TraitSO> l, string ID)
    {
        foreach (TraitSO t in l)
        {
            if (t.ID == ID)
            {
                return t;
            }
        }

        return null;
    }


    private Trait RandomTraitsFromList(List<TraitSO> l)
    {
        if (l.Count == 0) return new Trait();

        return new Trait(
            GetGlobalGene(l[Random.Range(0, l.Count)].ID), 
            GetGlobalGene(l[Random.Range(0, l.Count)].ID));
    }


    private Trait RandomTraitFromList(List<TraitSO> l)
    {
        if (l.Count == 0) return new Trait();

        return GeneToTrait(GetGlobalGene(l[Random.Range(0, l.Count)].ID));
    }


    private Trait WeightedRandomTraitsFromList(List<TraitSO> l)
    {
        Trait r = new Trait();
        if (l.Count == 0) return r;

        // Total rarity
        int totalRarity = 0;
        foreach(TraitSO t in l)
        {
            totalRarity += t.traitRarity;
        }


        // Active gene
        int rand = Random.Range(0, totalRarity);
        int i = 0;
        foreach (TraitSO t in l)
        {
            i += t.traitRarity;
            if (rand <= i)
            {
                r.activeGene = GetGlobalGene(t.ID);
                break;
            }
        }

        // Inactive gene
        rand = Random.Range(0, totalRarity);
        i = 0;
        foreach (TraitSO t in l)
        {
            i += t.traitRarity;
            if (rand <= i)
            {
                r.inactiveGene = GetGlobalGene(t.ID);
                break;
            }
        }

        // Error message
        if (r.activeGene.ID == null || r.activeGene.ID == "" || r.inactiveGene.ID == null || r.inactiveGene.ID == "")
        {
            Debug.LogError("Weighted Random Trait failed. Rand - " + rand + ". Total Rarity - " + totalRarity);
        }

        return r;
    }


    public Gene GetGlobalGene(string ID)
    {
        Gene r = new Gene();

        foreach (Gene g in loadedGlobalGenes)
        {
            if (g.ID == ID)
            {
                r = g; 
                break;
            }
        }

        if (r.ID == null || r.ID == "")
        {
            Debug.LogError("Global gene with ID " + ID + " could not be found");
        }

        return r;
    }


    public Trait GeneToTrait(Gene gene)
    {
        return new Trait(gene, gene);
    }


    public ShrimpStats ApplyStatModifiers(ShrimpStats s)
    {
        s = ApplyStatModifier(s.primaryColour.activeGene.ID, s);
        s = ApplyStatModifier(s.secondaryColour.activeGene.ID, s);

        s = ApplyStatModifier(s.pattern.activeGene.ID, s);

        s = ApplyStatModifier(s.body.activeGene.ID, s);
        s = ApplyStatModifier(s.head.activeGene.ID, s);
        s = ApplyStatModifier(s.eyes.activeGene.ID, s);
        s = ApplyStatModifier(s.tail.activeGene.ID, s);
        s = ApplyStatModifier(s.tailFan.activeGene.ID, s);
        s = ApplyStatModifier(s.antenna.activeGene.ID, s);
        s = ApplyStatModifier(s.legs.activeGene.ID, s);

        return s;
    }


    private ShrimpStats ApplyStatModifier(string ID, ShrimpStats s)
    {
        if (ID != null && ID != "")
        {
            foreach (Modifier m in GetTraitSO(ID).statModifiers)
            {
                switch (m.type)
                {
                    case ModifierEffects.temperament:
                        {
                            s.temperament += m.effect;
                            break;
                        }

                    case ModifierEffects.geneticSize:
                        {
                            s.geneticSize += m.effect;
                            break;
                        }
                }
            }
        }

        return s;
    }
}
