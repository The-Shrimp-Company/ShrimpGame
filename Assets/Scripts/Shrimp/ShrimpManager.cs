using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpManager : MonoBehaviour
{
    public static ShrimpManager instance;

    private int numberOfShrimp = 0;


    public void Awake()
    {
        instance = this;
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
}
