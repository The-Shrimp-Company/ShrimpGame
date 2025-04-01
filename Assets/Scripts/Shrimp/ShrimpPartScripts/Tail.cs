using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : PartScript
{
    public Transform tFanNode;
    public Tail Construct(ShrimpStats s, ref TFan tFan)
    {
        this.s = s;
        tFan = Instantiate(GeneManager.instance.GetTraitSO(s.tailFan.activeGene.ID).part, tFanNode).GetComponent<TFan>().Construct(s);
        SetMaterials(GeneManager.instance.GetTraitSO(s.tail.activeGene.ID).set);
        return this;
    }

    public void ChangeColours(ColourTypes colour)
    {
        tFanNode.GetChild(0).GetComponent<TFan>().ChangeColours(colour);

        SetColour(colour);
    }
}
