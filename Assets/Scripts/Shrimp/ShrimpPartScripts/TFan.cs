using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFan : PartScript
{
    public TFan Construct(ShrimpStats s)
    {
        this.s = s;
        SetMaterials(GeneManager.instance.GetTraitSO(s.tailFan.activeGene.ID).set);
        return this;
    }

    public void ChangeColours(ColourTypes colour)
    {
        SetColour(colour);
    }
}
