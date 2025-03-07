using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;


    [Header("Starting Trait Values")]
    [SerializeField] float minTraitValue = 5;
    [SerializeField] float maxTraitValue = 30;

    [Header("Value Fluctuation")]
    [SerializeField] float valueFluctuation;  // How much the value is changed when a shrimp is sold. Will be multiplied by the valueFluctuationStrength
    [SerializeField] AnimationCurve valueFluctuationStrength;  // 0 is the starting value

    public void Awake()
    {
        instance = this;
    }

    public void UpdateTraitValues(bool purchased, ShrimpStats traits)
    {
        UpdateValue(purchased, traits.primaryColour.activeGene);
        UpdateValue(purchased, traits.secondaryColour.activeGene);
        UpdateValue(purchased, traits.pattern.activeGene);

        UpdateValue(purchased, traits.body.activeGene);
        UpdateValue(purchased, traits.head.activeGene);
        UpdateValue(purchased, traits.eyes.activeGene);
        UpdateValue(purchased, traits.tail.activeGene);
        UpdateValue(purchased, traits.tailFan.activeGene);
        UpdateValue(purchased, traits.antenna.activeGene);
        UpdateValue(purchased, traits.legs.activeGene);
    }

    private void UpdateValue(bool purchased, Gene g)
    {
        // 

        //Gene global = GeneManager.instance.GetGlobalGene(g.ID)
        //float x = (minTraitValue + maxTraitValue) / 2;
        //float min = global.value - x;
        //float max = global.value + x;

        //if (purchased)
        //{

        //    g.value = Mathf.Clamp(g.value + (valueFluctuation * valueFluctuationStrength.Evaluate()), minTraitValue, maxTraitValue);
        //}

        //else if (!purchased)
        //{

        //}
    }

    public void GetShrimpValue()
    {
        // Add trait values

        // Apply multipliers for things like pure shrimp and health
    }
}

