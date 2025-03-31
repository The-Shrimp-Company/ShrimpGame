using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PartScript : MonoBehaviour
{
    [SerializeField] protected GameObject[] objs;

    protected ShrimpStats s;
    protected void SetMaterials(TraitSet trait)
    {
        foreach (GameObject obj in objs)
        {
            List<Material> mat = new List<Material>();

            switch (trait)
            {
                case TraitSet.Cherry:
                    mat.Add(GeneManager.instance.GetTraitSO(s.pattern.activeGene.ID).cherryPattern);
                    break;
                case TraitSet.Anomalis:
                    mat.Add(GeneManager.instance.GetTraitSO(s.pattern.activeGene.ID).anomalisPattern);
                    break;
                case TraitSet.Caridid:
                    mat.Add(GeneManager.instance.GetTraitSO(s.pattern.activeGene.ID).carididPattern);
                    break;
                case TraitSet.Nylon:
                    mat.Add(GeneManager.instance.GetTraitSO(s.pattern.activeGene.ID).nylonPattern);
                    break;
            }

            if (obj.GetComponent<MeshRenderer>() != null)
            {
                obj.GetComponent<MeshRenderer>().SetMaterials(mat);
                obj.GetComponent<MeshRenderer>().material.SetColor("_Pattern_Colour", GeneManager.instance.GetTraitSO(s.secondaryColour.activeGene.ID).colour);
                obj.GetComponent<MeshRenderer>().material.SetColor("_Base_Colour", GeneManager.instance.GetTraitSO(s.primaryColour.activeGene.ID).colour);
            }
            else if (obj.GetComponent<SkinnedMeshRenderer>() != null)
            {
                obj.GetComponent<SkinnedMeshRenderer>().SetMaterials(mat);
                obj.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Pattern_Colour", GeneManager.instance.GetTraitSO(s.secondaryColour.activeGene.ID).colour);
                obj.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Base_Colour", GeneManager.instance.GetTraitSO(s.primaryColour.activeGene.ID).colour);
            }
        }
    }
}
