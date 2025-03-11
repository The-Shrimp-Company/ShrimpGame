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

    [Header("Value Multipliers")]
    [SerializeField] float pureShrimpMultiplier = 1.5f;
    [SerializeField] AnimationCurve healthMultiplier;

    public void Awake()
    {
        instance = this;
    }

    public void UpdateTraitValues(bool purchased, ShrimpStats traits)
    {
        UpdateValueOfGene(purchased, traits.primaryColour.activeGene);
        UpdateValueOfGene(purchased, traits.secondaryColour.activeGene);
        UpdateValueOfGene(purchased, traits.pattern.activeGene);

        UpdateValueOfGene(purchased, traits.body.activeGene);
        UpdateValueOfGene(purchased, traits.head.activeGene);
        UpdateValueOfGene(purchased, traits.eyes.activeGene);
        UpdateValueOfGene(purchased, traits.tail.activeGene);
        UpdateValueOfGene(purchased, traits.tailFan.activeGene);
        UpdateValueOfGene(purchased, traits.antenna.activeGene);
        UpdateValueOfGene(purchased, traits.legs.activeGene);
    }

    private void UpdateValueOfGene(bool purchased, Gene g)
    {
        GlobalGene global = GeneManager.instance.GetGlobalGene(g.ID);
        float x = (minTraitValue + maxTraitValue) / 2;
        float min = global.startingValue - x;
        float max = global.startingValue + x;
        float l = Mathf.InverseLerp(min, max, global.currentValue);

        if (purchased)
        {
            global.currentValue = Mathf.Clamp(global.currentValue + (valueFluctuation * valueFluctuationStrength.Evaluate(l)), minTraitValue, maxTraitValue);
        }

        else if (!purchased)
        {
            global.currentValue = Mathf.Clamp(global.currentValue - (valueFluctuation * valueFluctuationStrength.Evaluate(l)), minTraitValue, maxTraitValue);
        }

        GeneManager.instance.SetGlobalGene(global);
    }

    public float GetShrimpValue(ShrimpStats s)
    {
        // Add trait values
        float t = 0;
        t += GeneManager.instance.GetGlobalGene(s.primaryColour.activeGene.ID).currentValue;
        t += GeneManager.instance.GetGlobalGene(s.secondaryColour.activeGene.ID).currentValue;
        t += GeneManager.instance.GetGlobalGene(s.pattern.activeGene.ID).currentValue;

        t += GeneManager.instance.GetGlobalGene(s.body.activeGene.ID).currentValue;
        t += GeneManager.instance.GetGlobalGene(s.head.activeGene.ID).currentValue;
        t += GeneManager.instance.GetGlobalGene(s.eyes.activeGene.ID).currentValue;
        t += GeneManager.instance.GetGlobalGene(s.tail.activeGene.ID).currentValue;
        t += GeneManager.instance.GetGlobalGene(s.tailFan.activeGene.ID).currentValue;
        t += GeneManager.instance.GetGlobalGene(s.antenna.activeGene.ID).currentValue;
        t += GeneManager.instance.GetGlobalGene(s.legs.activeGene.ID).currentValue;


        // Apply multipliers

        if (GeneManager.instance.CheckForPureShrimp(s)) t *= pureShrimpMultiplier;  // Pure Shrimp

        t *= healthMultiplier.Evaluate(s.illness / ShrimpManager.instance.maxShrimpIllness);  // Shrimp Health



        t = Mathf.Round(t * 100f) / 100f;  // Round to 2 decimal places

        return t;
    }
}

