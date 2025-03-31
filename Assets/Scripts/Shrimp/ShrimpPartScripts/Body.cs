using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Body : PartScript
{
    [SerializeField] private Transform headNode, legsNode, tailNode;
    [SerializeField] private bool debug = false;

    private void Start()
    {
        if (debug)
        {
            s = ShrimpManager.instance.CreateRandomShrimp(false);

            Instantiate(GeneManager.instance.GetTraitSO(s.head.activeGene.ID).part, headNode).GetComponent<Head>().Construct(s);
            Instantiate(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).part, legsNode).GetComponent<Legs>().Construct(s);
            Instantiate(GeneManager.instance.GetTraitSO(s.tail.activeGene.ID).part, tailNode).GetComponent<Tail>().Construct(s);

            SetMaterials(GeneManager.instance.GetTraitSO(s.body.activeGene.ID).set);
        }
    }

    public void Construct(ShrimpStats s)
    {
        this.s = s;

        Instantiate(GeneManager.instance.GetTraitSO(s.head.activeGene.ID).part, headNode).GetComponent<Head>().Construct(s);
        Instantiate(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).part, legsNode).GetComponent<Legs>().Construct(s);
        Instantiate(GeneManager.instance.GetTraitSO(s.tail.activeGene.ID).part, tailNode).GetComponent<Tail>().Construct(s);

        SetMaterials(GeneManager.instance.GetTraitSO(s.body.activeGene.ID).set);
    }

    public void ChangeColours(ColourTypes colour)
    {
        headNode.GetChild(0).GetComponent<Head>().ChangeColours(colour);
        legsNode.GetChild(0).GetComponent<Legs>().ChangeColours(colour);
        tailNode.GetChild(0).GetComponent<Tail>().ChangeColours(colour);

        SetColour(colour);
    }
}
