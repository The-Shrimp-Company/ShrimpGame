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
            s = ShrimpManager.instance.CreateRandomShrimp();

            Instantiate(GeneManager.instance.GetTraitSO(s.head.activeGene.ID).part, headNode).GetComponent<Head>().Construct(s);
            Instantiate(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).part, legsNode).GetComponent<Legs>().Construct(s);
            Instantiate(GeneManager.instance.GetTraitSO(s.tail.activeGene.ID).part, tailNode).GetComponent<Tail>().Construct(s);

            SetMaterials();
        }
    }

    public void Construct(ShrimpStats s)
    {
        this.s = s;

        Instantiate(GeneManager.instance.GetTraitSO(s.head.activeGene.ID).part, headNode).GetComponent<Head>().Construct(s);
        Instantiate(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).part, legsNode).GetComponent<Legs>().Construct(s);
        Instantiate(GeneManager.instance.GetTraitSO(s.tail.activeGene.ID).part, tailNode).GetComponent<Tail>().Construct(s);

        SetMaterials();
    }
}
