using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reputation
{
    private int reputation = 0;

    public static Reputation instance = new Reputation();

    public static int GetReputation()
    {
        return instance.reputation;
    }

    public static void AddReputation(int add)
    {
        instance.reputation += add;
    }
}
