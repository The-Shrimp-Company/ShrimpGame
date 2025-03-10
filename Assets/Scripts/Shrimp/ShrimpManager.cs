using System.Collections.Generic;
using UnityEngine;


public class ShrimpManager : MonoBehaviour
{
    public static ShrimpManager instance;
    private GeneManager geneManager;

    public GameObject shrimpPrefab;

    public List<Shrimp> allShrimp = new List<Shrimp>();
    private int numberOfShrimp = 0;


    [Header("Size")]
    [SerializeField] float maxSizeOfShrimp = 0.75f;
    private int maxGeneticShrimpSize = 100;
    [SerializeField] AnimationCurve sizeThroughChildhood;  // Childhood ends when moltSpeed levels out

    [Header("Hunger")]
    private int maxShrimpHunger = 100;

    [Header("Illness")]
    [HideInInspector] public int maxShrimpIllness = 100;

    [Header("Age")]
    [SerializeField] int maxShrimpAge;
    [SerializeField] AnimationCurve shrimpNaturalDeathAge;
    [SerializeField] AnimationCurve moltSpeed;  // In minutes, realtime

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
        s.birthTime = TimeManager.instance.GetTotalTime();
        s.hunger = 0;
        s.illness = 0;
        s.geneticSize = geneManager.IntGene(geneManager.sizeInheritance, maxGeneticShrimpSize, parentA.geneticSize, parentB.geneticSize, geneManager.sizeCanMutate);
        s.temperament = geneManager.IntGene(geneManager.temperamentInheritance, maxShrimpTemperament, parentA.temperament, parentB.temperament, geneManager.temperamentCanMutate);

        s.primaryColour = geneManager.TraitGene(geneManager.colourInheritance, 0, parentA.primaryColour, parentB.primaryColour, geneManager.colourCanMutate);
        s.secondaryColour = geneManager.TraitGene(geneManager.colourInheritance, 0, parentA.secondaryColour, parentB.secondaryColour, geneManager.colourCanMutate);

        s.pattern = geneManager.TraitGene(geneManager.patternInheritance, 0, parentA.pattern, parentB.pattern, geneManager.patternCanMutate);

        s.body = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.body, parentB.body, geneManager.bodyPartCanMutate);
        s.head = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.head, parentB.head, geneManager.bodyPartCanMutate);
        s.eyes = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.eyes, parentB.eyes, geneManager.bodyPartCanMutate);
        s.tail = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.tail, parentB.tail, geneManager.bodyPartCanMutate);
        s.tailFan = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.tailFan, parentB.tailFan, geneManager.bodyPartCanMutate);
        s.antenna = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.antenna, parentB.antenna, geneManager.bodyPartCanMutate);
        s.legs = geneManager.TraitGene(geneManager.bodyPartInheritance, 0, parentA.legs, parentB.legs, geneManager.bodyPartCanMutate);

        s.fightHistory = 0;
        s.breedingHistory = 0;
        s.illnessHistory = 0;
        s.moltHistory = 0;

        geneManager.ApplyStatModifiers(s);

        return s;
    }


    public ShrimpStats CreateRandomShrimp()
    {
        numberOfShrimp++;

        ShrimpStats s = new ShrimpStats();

        s.name = "Shrimp " + numberOfShrimp;
        s.gender = geneManager.RandomGender();
        s.birthTime = TimeManager.instance.CalculateBirthTimeFromAge(geneManager.IntGene(InheritanceType.FullRandom, Mathf.RoundToInt((maxShrimpAge - 1) * 0.9f), 0, 0, false) + Random.value);
        s.temperament = geneManager.IntGene(InheritanceType.FullRandom, maxShrimpTemperament, 0, 0, false);
        s.geneticSize = geneManager.IntGene(InheritanceType.FullRandom, maxGeneticShrimpSize, 0, 0, false);
        s.hunger = geneManager.IntGene(InheritanceType.FullRandom, Mathf.RoundToInt(maxShrimpAge * 0.9f), 0, 0, false);
        s.illness = geneManager.IntGene(InheritanceType.FullRandom, Mathf.RoundToInt(maxShrimpAge * 0.9f), 0, 0, false);

        Trait t = new Trait();
        t.activeGene.ID = "C";
        s.primaryColour = geneManager.TraitGene(InheritanceType.WeightedRandom, 0, t, t, false);
        s.secondaryColour = geneManager.TraitGene(InheritanceType.WeightedRandom, 0, t, t, false);

        t.activeGene.ID = "P";
        s.pattern = geneManager.TraitGene(InheritanceType.WeightedRandom, 0, t, t, false);

        t.activeGene.ID = "B";
        s.body = geneManager.TraitGene(InheritanceType.WeightedRandom, 0, t, t, false);

        t.activeGene.ID = "H";
        s.head = geneManager.TraitGene(InheritanceType.WeightedRandom, 0, t, t, false);

        t.activeGene.ID = "E";
        s.eyes = geneManager.TraitGene(InheritanceType.WeightedRandom, 0, t, t, false);

        t.activeGene.ID = "T";
        s.tail = geneManager.TraitGene(InheritanceType.WeightedRandom, 0, t, t, false);

        t.activeGene.ID = "F";
        s.tailFan = geneManager.TraitGene(InheritanceType.WeightedRandom, 0, t, t, false);

        t.activeGene.ID = "A";
        s.antenna = geneManager.TraitGene(InheritanceType.WeightedRandom, 0, t, t, false);

        t.activeGene.ID = "L";
        s.legs = geneManager.TraitGene(InheritanceType.WeightedRandom, 0, t, t, false);

        s.fightHistory = 0;
        s.breedingHistory = 0;
        s.illnessHistory = 0;
        s.moltHistory = 0;

        geneManager.ApplyStatModifiers(s);

        return s;
    }



    public ShrimpStats LoadShrimp()
    {
        numberOfShrimp++;

        ShrimpStats s = new ShrimpStats();

        return s;
    }



    public ShrimpStats CreateBlankShrimp()
    {
        numberOfShrimp++;

        ShrimpStats s = new ShrimpStats();

        s.name = "Shrimp " + numberOfShrimp;
        s.gender = geneManager.RandomGender();
        s.birthTime = TimeManager.instance.GetTotalTime();
        s.hunger = 0;
        s.illness = 0;
        s.temperament = 0;

        s.primaryColour = geneManager.GlobalGeneToTrait(geneManager.GetGlobalGene(geneManager.colourSOs[0].ID));
        s.secondaryColour = geneManager.GlobalGeneToTrait(geneManager.GetGlobalGene(geneManager.colourSOs[0].ID));
        s.pattern = geneManager.GlobalGeneToTrait(geneManager.GetGlobalGene(geneManager.patternSOs[0].ID));

        s.body = geneManager.GlobalGeneToTrait(geneManager.GetGlobalGene(geneManager.bodySOs[0].ID));
        s.head = geneManager.GlobalGeneToTrait(geneManager.GetGlobalGene(geneManager.headSOs[0].ID));
        s.eyes = geneManager.GlobalGeneToTrait(geneManager.GetGlobalGene(geneManager.eyeSOs[0].ID));
        s.tail = geneManager.GlobalGeneToTrait(geneManager.GetGlobalGene(geneManager.tailSOs[0].ID));
        s.tailFan = geneManager.GlobalGeneToTrait(geneManager.GetGlobalGene(geneManager.tailFanSOs[0].ID));
        s.antenna = geneManager.GlobalGeneToTrait(geneManager.GetGlobalGene(geneManager.antennaSOs[0].ID));
        s.legs = geneManager.GlobalGeneToTrait(geneManager.GetGlobalGene(geneManager.legsSOs[0].ID));

        s.fightHistory = 0;
        s.breedingHistory = 0;
        s.illnessHistory = 0;
        s.moltHistory = 0;

        return s;
    }



    public bool CheckForMoltFail(int age)  // Decides whether the shrimp dies of old age today. Will return true if it should.
    {
        return (Random.value < shrimpNaturalDeathAge.Evaluate(age / maxShrimpAge));
    }

    public float GetMoltTime(int age)
    {
        return moltSpeed.Evaluate(age / maxShrimpAge);
    }

    //public float GetShrimpSize(int )
    //{
    //    return sizeThroughChildhood.Evaluate()
    //}
}
