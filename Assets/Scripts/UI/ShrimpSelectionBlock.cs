using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ShrimpSelectionBlock : ContentBlock
{
    [SerializeField] private TextMeshProUGUI gender, age, pattern, body, legs, eyes, tail, tailFan, head;
    [SerializeField] private Image primaryColour, secondaryColour;

    private Shrimp _shrimp;

    public void Populate(Shrimp shrimp)
    {
        _shrimp = shrimp;
        text.text = _shrimp.stats.name;
        age.text = "Age: " + TimeManager.instance.GetShrimpAge(_shrimp.stats.birthTime).ToString();
        gender.text = "Gender: " + (_shrimp.stats.gender == true ? "M" : "F");
        pattern.text = "Pattern: " + GeneManager.instance.GetTraitSO(_shrimp.stats.pattern.activeGene.ID).traitName;
        body.text = "Body: " + GeneManager.instance.GetTraitSO(_shrimp.stats.body.activeGene.ID).set;
        legs.text = "Legs: " + GeneManager.instance.GetTraitSO(_shrimp.stats.legs.activeGene.ID).set;
        eyes.text = "Eyes: " + GeneManager.instance.GetTraitSO(_shrimp.stats.eyes.activeGene.ID).set;
        tail.text = "Tail: " + GeneManager.instance.GetTraitSO(_shrimp.stats.tail.activeGene.ID).set;
        head.text = "Head: " + GeneManager.instance.GetTraitSO(_shrimp.stats.head.activeGene.ID).set;
        tailFan.text = "Tail Fan: " + GeneManager.instance.GetTraitSO(_shrimp.stats.tailFan.activeGene.ID).set;
        primaryColour.color = GeneManager.instance.GetTraitSO(_shrimp.stats.primaryColour.activeGene.ID).color;
        secondaryColour.color = GeneManager.instance.GetTraitSO(_shrimp.stats.secondaryColour.activeGene.ID).color;
    }
}
