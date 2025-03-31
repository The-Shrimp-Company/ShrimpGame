using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TFan : PartScript
{
    public void Construct(ShrimpStats s)
    {
        this.s = s;
        SetMaterials(GeneManager.instance.GetTraitSO(s.tailFan.activeGene.ID).set);
    }

    public void ChangeColours(ColourTypes colour)
    {
        SetColour(colour);
    }
}
