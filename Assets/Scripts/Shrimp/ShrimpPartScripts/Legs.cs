using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Legs : PartScript
{
    public Transform bodyNode;
    [SerializeField] private bool debug = false;

    [HideInInspector]
    public Body body;
    private Tail tail;
    private TFan tFan;
    private Head head;

    private void Start()
    {
        if (debug)
        {
            s = ShrimpManager.instance.CreateRandomShrimp(false);

            SetMaterials(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).set);

            body = Instantiate(GeneManager.instance.GetTraitSO(s.body.activeGene.ID).part, bodyNode).GetComponent<Body>().Construct(s, ref tFan, ref tail, ref head);

            SetAnimation(AnimNames.swimming);
        }
    }

    public void Construct(ShrimpStats s)
    {
        this.s = s;

        SetMaterials(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).set);

        body = Instantiate(GeneManager.instance.GetTraitSO(s.body.activeGene.ID).part, bodyNode).GetComponent<Body>().Construct(s, ref tFan, ref tail, ref head);

        SetAnimation(AnimNames.swimming);
    }

    public void ChangeColours(ColourTypes colour)
    {
        head.ChangeColours(colour);
        body.ChangeColours(colour);
        tail.ChangeColours(colour);

        SetColour(colour);
    }

    public void SetAnimation(AnimNames anim)
    {
        head.StartAnimation(anim);
        StartAnimation(anim);
        tail.StartAnimation(anim);
        //tFan.StartAnimation(anim);
    }
}
