using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PartScript : MonoBehaviour
{
    public enum AnimNames
    {
        swimming,
        eating,
        breedingM,
        idle,
        breedingF,
        fighting
    }


    [SerializeField] protected GameObject[] objs;

    [SerializeField] protected SerializedDictionary<AnimNames, AnimationClip> animations;

    protected Animation animation;

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
            }
            else if (obj.GetComponent<SkinnedMeshRenderer>() != null)
            {
                obj.GetComponent<SkinnedMeshRenderer>().SetMaterials(mat);
            }

        }

        SetColour();
    }

    protected void SetColour(ColourTypes type = ColourTypes.main)
    {
        Color primary = Color.white;
        Color secondary = Color.white;
        switch (type)
        {
            case ColourTypes.main:
            {
                primary = GeneManager.instance.GetTraitSO(s.primaryColour.activeGene.ID).colour;
                secondary = GeneManager.instance.GetTraitSO(s.secondaryColour.activeGene.ID).colour;
                break;
            }
            case ColourTypes.dead:
            {
                primary = GeneManager.instance.GetTraitSO(s.primaryColour.activeGene.ID).deadColour;
                secondary = GeneManager.instance.GetTraitSO(s.secondaryColour.activeGene.ID).deadColour;
                break;
            }
            case ColourTypes.discoloured:
            {
                primary = GeneManager.instance.GetTraitSO(s.primaryColour.activeGene.ID).discolourationColour;
                secondary = GeneManager.instance.GetTraitSO(s.secondaryColour.activeGene.ID).discolourationColour;
                break;
            }
            default:
            {
                primary = GeneManager.instance.GetTraitSO(s.primaryColour.activeGene.ID).colour;
                secondary = GeneManager.instance.GetTraitSO(s.secondaryColour.activeGene.ID).colour;
                break;
            }
        }

        foreach (GameObject obj in objs)
        {
            if (obj.GetComponent<MeshRenderer>() != null)
            {
                obj.GetComponent<MeshRenderer>().material.SetColor("_Pattern_Colour", secondary);
                obj.GetComponent<MeshRenderer>().material.SetColor("_Base_Colour", primary);
            }
            else if (obj.GetComponent<SkinnedMeshRenderer>() != null)
            {
                obj.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Pattern_Colour", secondary);
                obj.GetComponent<SkinnedMeshRenderer>().material.SetColor("_Base_Colour", primary);
            }
        }
    }


    public void StartAnimation(AnimNames anim)
    {
        if (animations.ContainsKey(anim))
        {
            if (animation == null)
            {
                animation = gameObject.GetComponent<Animation>();
            }
            if (animation.GetClipCount() > 0)
            {
                animation.Stop();
            }
            animation.AddClip(animations[anim], "current");

            animation.Play("current");


            Debug.Log(animation.isPlaying);
        }
        else
        {
            Debug.LogWarning("Animation missing");
        }
    }
}
