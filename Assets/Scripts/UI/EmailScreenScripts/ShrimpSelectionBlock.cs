using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ShrimpSelectionBlock : ContentBlock
{
    [SerializeField] private TextMeshProUGUI gender, age, pattern, body, legs, eyes, tail, tailFan, head, price;
    [SerializeField] private Image primaryColour, secondaryColour;

    private ShrimpStats _shrimp;

    public BuyScreen screen;

    public void Populate(ShrimpStats shrimp)
    {
        _shrimp = shrimp;
        text.text = _shrimp.name;
        age.text = "Age: " + TimeManager.instance.GetShrimpAge(_shrimp.birthTime).ToString();
        gender.text = "Gender: " + (_shrimp.gender == true ? "M" : "F");
        pattern.text = "Pattern: " + GeneManager.instance.GetTraitSO(_shrimp.pattern.activeGene.ID).traitName;
        body.text = "Body: " + GeneManager.instance.GetTraitSO(_shrimp.body.activeGene.ID).set;
        legs.text = "Legs: " + GeneManager.instance.GetTraitSO(_shrimp.legs.activeGene.ID).set;
        eyes.text = "Eyes: " + GeneManager.instance.GetTraitSO(_shrimp.eyes.activeGene.ID).set;
        tail.text = "Tail: " + GeneManager.instance.GetTraitSO(_shrimp.tail.activeGene.ID).set;
        head.text = "Head: " + GeneManager.instance.GetTraitSO(_shrimp.head.activeGene.ID).set;
        tailFan.text = "Tail Fan: " + GeneManager.instance.GetTraitSO(_shrimp.tailFan.activeGene.ID).set;
        primaryColour.color = GeneManager.instance.GetTraitSO(_shrimp.primaryColour.activeGene.ID).color;
        secondaryColour.color = GeneManager.instance.GetTraitSO(_shrimp.secondaryColour.activeGene.ID).color;
        price.text = EconomyManager.instance.GetShrimpValue(shrimp).ToSafeString();
    }

    public void BuyThis()
    {
        screen.BuyShrimp(_shrimp);
    }
}
