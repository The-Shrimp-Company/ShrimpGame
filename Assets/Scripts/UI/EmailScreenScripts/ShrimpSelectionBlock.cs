using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
    public EmailScreen email;
    private ShrimpPurchaseContent parent;

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
        string thistext = "�" + EconomyManager.instance.GetShrimpValue(shrimp).ToSafeString();
        string regEx = @"^.*\.\d$";
        Regex regex = new Regex(regEx);
        Regex otherregex = new Regex(@"^.*\.\d\d+$");
        if (thistext.Contains("."))
        {
            if (regex.IsMatch(thistext))
            {
                price.text = thistext + "0";
            }
            else if (otherregex.IsMatch(thistext))
            {
                price.text = thistext.Substring(0, thistext.IndexOf(".") + 3);
            }
        }
        else
        {
            Debug.Log("HEEEEEEEE");
            price.text = thistext + ".00";
        }
    }

    public void Populate(ShrimpStats shrimp, ShrimpPurchaseContent par)
    {
        parent = par;
        Populate(shrimp);
    }

    public void BuyThis()
    {
        if (screen.BuyShrimp(_shrimp))
        {
            parent._shrimp.Remove(_shrimp);
            Destroy(gameObject);
        }
    }

    public void SellThis()
    {
        email.CloseSelection();
    }
}
