using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PartScript : MonoBehaviour
{
    [SerializeField] protected GameObject[] objs;

    protected ShrimpStats s;
    protected void SetMaterials()
    {
        foreach (GameObject obj in objs)
        {
            List<Material> mat = new List<Material>();
            mat.Add(GeneManager.instance.GetTraitSO(s.pattern.activeGene.ID).texture);
            if (obj.GetComponent<MeshRenderer>() != null)
            {
                obj.GetComponent<MeshRenderer>().SetMaterials(mat);
                obj.GetComponent<MeshRenderer>().material.SetColor("_Pattern_Colour", GeneManager.instance.GetTraitSO(s.secondaryColour.activeGene.ID).color);
                obj.GetComponent<MeshRenderer>().material.SetColor("_Base_Colour", GeneManager.instance.GetTraitSO(s.primaryColour.activeGene.ID).color);
            }
            else if (obj.GetComponent<SkinnedMeshRenderer>() != null)
            {
                obj.GetComponent<SkinnedMeshRenderer>().SetMaterials(mat);
                obj.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Pattern_Colour", GeneManager.instance.GetTraitSO(s.secondaryColour.activeGene.ID).color);
                obj.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Base_Colour", GeneManager.instance.GetTraitSO(s.primaryColour.activeGene.ID).color);
            }
        }
    }
}
