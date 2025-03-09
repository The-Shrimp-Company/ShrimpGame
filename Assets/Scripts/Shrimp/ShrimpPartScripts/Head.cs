using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : PartScript
{
    [SerializeField] private Transform eyesNode;

    public void Construct(ShrimpStats s)
    {
        this.s = s;
        Instantiate(GeneManager.instance.GetTraitSO(s.eyes.activeGene.ID).part, eyesNode).GetComponent<Eyes>().Construct(s);
        SetMaterials();
    }
}
