using UnityEngine;


public class ShrimpManager : MonoBehaviour
{
    public static ShrimpManager instance;
    private GeneManager geneManager;

    private int numberOfShrimp = 0;

    [Header("Death")]
    [SerializeField] int maxShrimpAge;
    [SerializeField] AnimationCurve shrimpNaturalDeathAge;


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
        s.temperament = geneManager.IntGene(geneManager.temperamentInheritance, 100, parentA.temperament, parentB.temperament);

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
        s.age = geneManager.IntGene(InheritanceType.FullRandom, maxShrimpAge, 0, 0);
        s.hunger = 0;
        s.illness = 0;
        s.temperament = 0;

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
