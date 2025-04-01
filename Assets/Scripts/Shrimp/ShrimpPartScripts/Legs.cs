using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : PartScript
{
    public Legs Construct(ShrimpStats s)
    {
        this.s = s;
        SetMaterials(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).set);
        return this;
    }

    public void ChangeColours(ColourTypes colour)
    {
        SetColour(colour);
    }
}
