using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : PartScript
{
    public Transform eyesNode;

    public Head Construct(ShrimpStats s)
    {
        this.s = s;
        SetMaterials(GeneManager.instance.GetTraitSO(s.head.activeGene.ID).set);
        Instantiate(GeneManager.instance.GetTraitSO(s.eyes.activeGene.ID).part, eyesNode).GetComponent<Eyes>().Construct(s);
        return this;
    }

    public void ChangeColours(ColourTypes colour)
    {
        eyesNode.GetChild(0).GetComponent<Eyes>().ChangeColours(colour);

        SetColour(colour);
    }
}
