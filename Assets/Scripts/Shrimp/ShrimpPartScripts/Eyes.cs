using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : PartScript
{
    public void Construct(ShrimpStats s)
    {
        this.s = s;
        SetMaterials(GeneManager.instance.GetTraitSO(s.eyes.activeGene.ID).set);
    }
}
