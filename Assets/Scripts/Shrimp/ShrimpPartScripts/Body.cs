using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Body : PartScript
{
    public Transform headNode, legsNode, tailNode;
    [SerializeField] private bool debug = false;

    private Legs legs;
    private Tail tail;
    private TFan tFan;
    private Head head;

    private void Start()
    {
        if (debug)
        {
            s = ShrimpManager.instance.CreateRandomShrimp(false);

            head = Instantiate(GeneManager.instance.GetTraitSO(s.head.activeGene.ID).part, headNode).GetComponent<Head>().Construct(s);
            legs = Instantiate(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).part, legsNode).GetComponent<Legs>().Construct(s);
            tail = Instantiate(GeneManager.instance.GetTraitSO(s.tail.activeGene.ID).part, tailNode).GetComponent<Tail>().Construct(s, ref tFan);

            SetAnimation(AnimNames.swimming);

            SetMaterials(GeneManager.instance.GetTraitSO(s.body.activeGene.ID).set);
        }
    }

    public void Construct(ShrimpStats s)
    {
        this.s = s;

        head = Instantiate(GeneManager.instance.GetTraitSO(s.head.activeGene.ID).part, headNode).GetComponent<Head>().Construct(s);
        legs = Instantiate(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).part, legsNode).GetComponent<Legs>().Construct(s);
        tail = Instantiate(GeneManager.instance.GetTraitSO(s.tail.activeGene.ID).part, tailNode).GetComponent<Tail>().Construct(s, ref tFan);

        SetAnimation(AnimNames.swimming);

        SetMaterials(GeneManager.instance.GetTraitSO(s.body.activeGene.ID).set);
    }

    public void ChangeColours(ColourTypes colour)
    {
        head.ChangeColours(colour);
        legs.ChangeColours(colour);
        tail.ChangeColours(colour);

        SetColour(colour);
    }

    public void SetAnimation(AnimNames anim)
    {
        head.StartAnimation(anim);
        legs.StartAnimation(anim);
        tail.StartAnimation(anim);
        //tFan.StartAnimation(anim);
    }
}
