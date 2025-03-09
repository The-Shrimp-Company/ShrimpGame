using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : PartScript
{
    public void Construct(ShrimpStats s)
    {
        this.s = s;
        SetMaterials();
    }
}
