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
    private Eyes eyes;


    private void Start()
    {
        if (debug)
        {
            s = ShrimpManager.instance.CreateRandomShrimp(false);

            SetMaterials(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).set);

            body = Instantiate(GeneManager.instance.GetTraitSO(s.body.activeGene.ID).part, bodyNode).GetComponent<Body>().Construct(s, ref tFan, ref tail, ref head, ref eyes);

            SetAnimation(AnimNames.swimming);

            body.GetComponent<LODGroup>().localReferencePoint = body.transform.InverseTransformPoint(transform.TransformPoint(GetComponent<LODGroup>().localReferencePoint));
            body.GetComponent<LODGroup>().size = GetComponent<LODGroup>().size;
            tail.GetComponent<LODGroup>().localReferencePoint = tail.transform.InverseTransformPoint(transform.TransformPoint(GetComponent<LODGroup>().localReferencePoint));
            tail.GetComponent<LODGroup>().size = GetComponent<LODGroup>().size;
            tFan.GetComponent<LODGroup>().localReferencePoint = tFan.transform.InverseTransformPoint(transform.TransformPoint(GetComponent<LODGroup>().localReferencePoint));
            tFan.GetComponent<LODGroup>().size = GetComponent<LODGroup>().size;
            head.GetComponent<LODGroup>().localReferencePoint = head.transform.InverseTransformPoint(transform.TransformPoint(GetComponent<LODGroup>().localReferencePoint));
            head.GetComponent<LODGroup>().size = GetComponent<LODGroup>().size;
            eyes.GetComponent<LODGroup>().localReferencePoint = eyes.transform.InverseTransformPoint(transform.TransformPoint(GetComponent<LODGroup>().localReferencePoint));
            eyes.GetComponent<LODGroup>().size = GetComponent<LODGroup>().size;
        }
    }

    

    public void Construct(ShrimpStats s)
    {
        this.s = s;

        SetMaterials(GeneManager.instance.GetTraitSO(s.legs.activeGene.ID).set);

        body = Instantiate(GeneManager.instance.GetTraitSO(s.body.activeGene.ID).part, bodyNode).GetComponent<Body>().Construct(s, ref tFan, ref tail, ref head, ref eyes);

        SetAnimation(AnimNames.swimming);

        body.GetComponent<LODGroup>().localReferencePoint = body.transform.InverseTransformPoint(transform.TransformPoint(GetComponent<LODGroup>().localReferencePoint));
        body.GetComponent<LODGroup>().size = GetComponent<LODGroup>().size;
        tail.GetComponent<LODGroup>().localReferencePoint = tail.transform.InverseTransformPoint(transform.TransformPoint(GetComponent<LODGroup>().localReferencePoint));
        tail.GetComponent<LODGroup>().size = GetComponent<LODGroup>().size;
        tFan.GetComponent<LODGroup>().localReferencePoint = tFan.transform.InverseTransformPoint(transform.TransformPoint(GetComponent<LODGroup>().localReferencePoint));
        tFan.GetComponent<LODGroup>().size = GetComponent<LODGroup>().size;
        head.GetComponent<LODGroup>().localReferencePoint = head.transform.InverseTransformPoint(transform.TransformPoint(GetComponent<LODGroup>().localReferencePoint));
        head.GetComponent<LODGroup>().size = GetComponent<LODGroup>().size;
        eyes.GetComponent<LODGroup>().localReferencePoint = eyes.transform.InverseTransformPoint(transform.TransformPoint(GetComponent<LODGroup>().localReferencePoint));
        eyes.GetComponent<LODGroup>().size = GetComponent<LODGroup>().size;
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
        /*
        head.StartAnimation(anim);
        StartAnimation(anim);
        tail.StartAnimation(anim);
        //tFan.StartAnimation(anim);
        */
    }
}
