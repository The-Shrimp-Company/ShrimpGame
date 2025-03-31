using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : PartScript
{
    [SerializeField] private Transform tFanNode;
    public void Construct(ShrimpStats s)
    {
        this.s = s;
        Instantiate(GeneManager.instance.GetTraitSO(s.tailFan.activeGene.ID).part, tFanNode).GetComponent<TFan>().Construct(s);
        SetMaterials(GeneManager.instance.GetTraitSO(s.tail.activeGene.ID).set);
    }

    public void ChangeColours(ColourTypes colour)
    {
        tFanNode.GetChild(0).GetComponent<TFan>().ChangeColours(colour);

        SetColour(colour);
    }
}
