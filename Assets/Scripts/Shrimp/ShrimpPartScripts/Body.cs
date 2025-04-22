using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Body : PartScript
{
    public Transform headNode, tailNode;


    public Body Construct(ShrimpStats s, ref Head head, ref Tail tail, ref TFan tFan)
    {
        this.s = s;

        head = Instantiate(GeneManager.instance.GetTraitSO(s.head.activeGene.ID).part, headNode).GetComponent<Head>().Construct(s);
        tail = Instantiate(GeneManager.instance.GetTraitSO(s.tail.activeGene.ID).part, tailNode).GetComponent<Tail>().Construct(s, ref tFan);


        SetMaterials(GeneManager.instance.GetTraitSO(s.body.activeGene.ID).set);

        return this;
    }

    public void ChangeColours(ColourTypes colour)
    {

        SetColour(colour);
    }

    
}
