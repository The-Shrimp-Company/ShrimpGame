using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public string ID;  // Matches the trait with the saved genes, do not change once we are able to save the game
    public UpgradeTypes upgradeType;
    public string upgradeName;

    public GameObject upgradePrefab;

    public float cost = 50;
    public float breakRate = 5;
    public float repairCost = 15;
    public float energyCost = 0;

    [Header("Filter")]
    public float filterCapacity;

    [Header("Heater")]
    public float heaterOutput;
    public Thermometer thermometer;
}


public enum UpgradeTypes
{
    Filter,
    Heater,
    Camera,
    PhIndicator,
    MineralRegulator,
    FoodDispenser,
    PriceTag,
    Decorations
}


public enum Thermometer
{
    NoThermometer,
    ThermometerOnly,
    AutomaticThermometer
}