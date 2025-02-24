using UnityEngine;

public enum InheritanceType
{
    FullRandom,
    WeightedRandom,
    Punnett,
    FlatAverage
}

public class ShrimpManager : MonoBehaviour
{
    public static ShrimpManager instance;

    private int numberOfShrimp = 0;

    [SerializeField] int weightPercentage;


    [SerializeField] InheritanceType genderInheritance;
    private System.Action genderRandom;
    [SerializeField] InheritanceType sizeInheritance;
    private System.Action sizeRandom;
    [SerializeField] InheritanceType temperamentInheritance;
    private System.Action temperamentRandom;
    [SerializeField] InheritanceType patternInheritance;
    private System.Action patternRandom;
    [SerializeField] InheritanceType bodyPartInheritance;
    private System.Action bodyPartRandom;


    public void Awake()
    {
        instance = this;

        // Bind the corresponding inheritance functions to the gene types
        //genderRandom += BindInheritance(genderInheritance);
        //sizeRandom += BindInheritance(sizeInheritance);
        //temperamentRandom += BindInheritance(temperamentInheritance);
        //patternRandom += BindInheritance(patternInheritance);
        //bodyPartRandom += BindInheritance(bodyPartInheritance);
    }

    //private System.Action BindInheritance(InheritanceType type)
    //{
    //    switch(type)
    //    {
    //        case InheritanceType.FullRandom:
    //            return FullyRandom;
    //        case InheritanceType.WeightedRandom:
    //            return WeightedRandom;
    //        case InheritanceType.Punnett:
    //            return PunnetSquare;
    //        case InheritanceType.FlatAverage:
    //            return FlatAverage;
    //        default:
    //            return FullyRandom;
    //    }
    //}

    private bool FullyRandomBool()
    {
        return (Random.value > 0.5f);
    }
    private int FullyRandomInt(int upperBound)
    {
        return (Random.Range(0, upperBound + 1));
    }

    private int WeightedRandom(int upperBound, float weight, int parentAVal, int parentBVal)
    {
        float val = Random.Range(0, upperBound + 1);
        weight = weight / 100;  // Convert percentage to decimal
        val += (((parentAVal + parentBVal) / 2) - val) * weight;
        return Mathf.RoundToInt(val);
    }

    private void PunnetSquareTrait()
    {

    }

    private int FlatAverage(int parentAVal, int parentBVal)
    {
        return Mathf.RoundToInt((parentAVal + parentBVal) / 2);
    }

    public ShrimpStats CreateShrimp()
    {
        numberOfShrimp++;

        ShrimpStats s = new ShrimpStats();

        s.name = "Shrimp " + numberOfShrimp;
        s.gender = RandomGender();
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


    public ShrimpStats CreateRandomShrimp()
    {
        numberOfShrimp++;

        ShrimpStats s = new ShrimpStats();

        s.name = "Shrimp " + numberOfShrimp;
        s.gender = RandomGender();
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


    private bool RandomGender()
    {
        int i = Random.Range(0, 2);
        if (i == 0) return false;  // Female
        else return true;  // Male
    }

    private int RandomSize()
    {
        if (InheritanceType)
    }
}
