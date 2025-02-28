using System.Collections.Generic;
using UnityEngine;


public class ShrimpManager : MonoBehaviour
{
    public static ShrimpManager instance;
    private GeneManager geneManager;

    public List<Shrimp> allShrimp = new List<Shrimp>();
    private int numberOfShrimp = 0;


    [Header("Size")]
    private int maxShrimpSize = 100;

    [Header("Hunger")]
    private int maxShrimpHunger = 100;

    [Header("Illness")]
    private int maxShrimpIllness = 100;

    [Header("Death")]
    [SerializeField] int maxShrimpAge;
    [SerializeField] AnimationCurve shrimpNaturalDeathAge;

    [Header("Temperament")]
    private int maxShrimpTemperament = 100;


    public void Awake()
    {
        instance = this;
        geneManager = GetComponent<GeneManager>();
    }


    public ShrimpStats CreateShrimpThroughBreeding(ShrimpStats parentA, ShrimpStats parentB)
    {
        numberOfShrimp++;

        ShrimpStats s = new ShrimpStats();

        s.name = "Shrimp " + numberOfShrimp;
        s.gender = geneManager.RandomGender();
        s.age = 0;
        s.hunger = 0;
        s.illness = 0;
        s.geneticSize = geneManager.IntGene(geneManager.sizeInheritance, maxShrimpSize, parentA.geneticSize, parentB.geneticSize, geneManager.sizeCanMutate);
        s.temperament = geneManager.IntGene(geneManager.temperamentInheritance, maxShrimpTemperament, parentA.temperament, parentB.temperament, geneManager.temperamentCanMutate);

        s.primaryColour = geneManager.TraitGene(geneManager.colourInheritance, 0, parentA.primaryColour, parentB.primaryColour, geneManager.colourCanMutate);
        s.secondaryColour = geneManager.TraitGene(geneManager.colourInheritance, 0, parentA.secondaryColour, parentB.secondaryColour, geneManager.colourCanMutate);

        s.pattern = geneManager.TraitGene(geneManager.patternInheritance, 0, parentA.pattern, parentB.pattern, geneManager.patternCanMutate);

        s.eyes = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.eyes, parentB.eyes, geneManager.bodyPartCanMutate);
        s.tail = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.tail, parentB.tail, geneManager.bodyPartCanMutate);
        s.tailFan = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.tailFan, parentB.tailFan, geneManager.bodyPartCanMutate);
        s.antenna = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.antenna, parentB.antenna, geneManager.bodyPartCanMutate);
        s.face = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.face, parentB.face, geneManager.bodyPartCanMutate);
        s.frontLegs = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.frontLegs, parentB.frontLegs, geneManager.bodyPartCanMutate);

        s.fightHistory = 0;
        s.breedingHistory = 0;
        s.illnessHistory = 0;
        s.moltHistory = 0;

        return s;
    }


    public ShrimpStats CreateRandomShrimp()
    {
        numberOfShrimp++;

        ShrimpStats s = new ShrimpStats();

        s.name = "Shrimp " + numberOfShrimp;
        s.gender = geneManager.RandomGender();
        s.age = geneManager.IntGene(InheritanceType.FullRandom, Mathf.RoundToInt(maxShrimpAge * 0.9f), 0, 0, false);
        s.temperament = geneManager.IntGene(InheritanceType.FullRandom, maxShrimpTemperament, 0, 0, false);
        s.geneticSize = geneManager.IntGene(InheritanceType.FullRandom, maxShrimpSize, 0, 0, false);
        s.hunger = geneManager.IntGene(InheritanceType.FullRandom, Mathf.RoundToInt(maxShrimpAge * 0.9f), 0, 0, false);
        s.illness = geneManager.IntGene(InheritanceType.FullRandom, Mathf.RoundToInt(maxShrimpAge * 0.9f), 0, 0, false);

        //Trait t = new Trait();
        //s.primaryColour = geneManager.TraitGene(InheritanceType.FullRandom, 0, t, t, false);
        //s.secondaryColour = geneManager.TraitGene(InheritanceType.FullRandom, 0, t, t, false);

        //s.pattern = geneManager.TraitGene(InheritanceType.FullRandom, 0, t, t, false);

        //s.eyes = geneManager.TraitGene(InheritanceType.FullRandom, 0, t, t, false);
        //s.tail = geneManager.TraitGene(InheritanceType.FullRandom, 0, t, t, false);
        //s.tailFan = geneManager.TraitGene(InheritanceType.FullRandom, 0, t, t, false);
        //s.antenna = geneManager.TraitGene(InheritanceType.FullRandom, 0, t, t, false);
        //s.face = geneManager.TraitGene(InheritanceType.FullRandom, 0, t, t, false);
        //s.frontLegs = geneManager.TraitGene(InheritanceType.FullRandom, 0, t, t, false);

        s.fightHistory = 0;
        s.breedingHistory = 0;
        s.illnessHistory = 0;
        s.moltHistory = 0;

        return s;
    }



    public ShrimpStats CreateBlankShrimp()
    {
        numberOfShrimp++;

        ShrimpStats s = new ShrimpStats();

        s.name = "Shrimp " + numberOfShrimp;
        s.gender = geneManager.RandomGender();
        s.age = 0;
        s.hunger = 0;
        s.illness = 0;
        s.temperament = 0;

        /// Set colour, pattern and body parts to the first options

        s.fightHistory = 0;
        s.breedingHistory = 0;
        s.illnessHistory = 0;
        s.moltHistory = 0;

        return s;
    }



    public bool CheckForNaturalDeath(int age)  // Decides whether the shrimp dies of old age today. Will return true if it should.
    {
        return (Random.value > shrimpNaturalDeathAge.Evaluate(age / maxShrimpAge));
    }
}
