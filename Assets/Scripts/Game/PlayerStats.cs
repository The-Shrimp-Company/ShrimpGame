using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    public static Stats stats = new Stats();
}

[System.Serializable]
public struct Stats
{
    public int timesGameLoaded;

    public int shelfCount;
    public int tankCount;
    public int smallTankCount;
    public int largeTankCount;

    public float totalMoney;
    public float moneyMade;
    public float moneySpent;
    public int totalShrimp;
    public float totalPlaytime;

    public int shrimpSold;
    public int shrimpSoldThroughOpenTank;
    public int requestsCompleted;

    public int shrimpBred;
    public int shrimpBought;
    public int shrimpMoved;

    public int tanksNamed;
    public int shrimpNamed;

    public int illnessesGained;
    public int illnessesCured;

    public int shrimpDeaths;
    public int shrimpDeathsThroughAge;
    public int shrimpDeathsThroughOverpopulation;
    public int shrimpDeathsThroughHunger; //

    public int timesShrimpShopAppOpened;
    public int timesMailAppOpened;
    public int timesSellingAppOpened;
    public int timesItemShopAppOpened;
    public int timesInventoryAppOpened;
    public int timesSettingsAppOpened;

    public float timeSpentMoving;
    public float timeSpentFocusingTank;
    public float timeSpentFocusingShrimp;
}