using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Legs : PartScript
{
    public Transform bodyNode;
    [SerializeField] private bool debug = false;

    private Tail tail;
    private Head head;
    private Body body;
    private TFan tFan;

    private void Start()
    {
        if (debug)
        {
            s = ShrimpManager.instance.CreateRandomShrimp(false);

            body = Instantiate(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).part, bodyNode).GetComponent<Body>().Construct(s, ref head, ref tail, ref tFan);

            SetAnimation(AnimNames.swimming);

            SetMaterials(GeneManager.instance.GetTraitSO(s.body.activeGene.ID).set);
        }
    }

    public void Construct(ShrimpStats s)
    {
        this.s = s;

        body = Instantiate(GeneManager.instance.GetTraitSO(s.body.activeGene.ID).part, bodyNode).GetComponent<Body>().Construct(s, ref head, ref tail, ref tFan);

        SetMaterials(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).set);

        SetAnimation(AnimNames.swimming);
    }

    public void ChangeColours(ColourTypes colour)
    {
        SetColour(colour);
        tail.ChangeColours(colour);
        head.ChangeColours(colour);
        body.ChangeColours(colour);
        tFan.ChangeColours(colour);
    }

    public void SetAnimation(AnimNames anim)
    {
        head.StartAnimation(anim);
        tail.StartAnimation(anim);
        //tFan.StartAnimation(anim);

        StartAnimation(anim);
    }

    public Transform GetHeadNode()
    {
        return head.transform;
    }

    public Transform GetTFanNode()
    {
        return tFan.transform;
    }
}
