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


    public void Awake()
    {
        instance = this;
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


    private Trait PunnetSquareTrait(Trait parentATrait, Trait parentBTrait)
    {
        /*
         * 
         */

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
            type = InheritanceType.FullRandom;
        }

        switch (type)
        {
            case InheritanceType.FullRandom:
            {
                /// ADD RANDOM TRAIT -----------------------------------------------------------
                return PunnetSquareTrait(parentAVal, parentBVal);
            }

            case InheritanceType.WeightedRandom:
            {
                Debug.LogError("Weighted Random is not supported for traits, please ask Aaron to implement this");
                return PunnetSquareTrait(parentAVal, parentBVal);
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
}
