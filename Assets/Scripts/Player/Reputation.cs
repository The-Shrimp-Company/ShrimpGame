using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reputation
{
    private float reputation = 0;

    public static Reputation instance = new Reputation();

    public static float GetReputation()
    {
        return instance.reputation;
    }

    public static void AddReputation(float add)
    {
        instance.reputation += add;
    }
}
