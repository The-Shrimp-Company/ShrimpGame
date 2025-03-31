using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymptomDiscolouration : Symptom
{
    public override void StartSymptom()
    {
        symptom = IllnessSymptoms.Discolouration;

        if (shrimp.shrimpBody != null)
        {
            shrimp.shrimpBody.ChangeColours(ColourTypes.discoloured);
        }

        base.StartSymptom();
    }

    public override void UpdateSymptom(float elapsedTime)
    {
        base.UpdateSymptom(elapsedTime);
    }

    public override void EndSymptom()
    {
        if (shrimp.shrimpBody != null)
        {
            shrimp.shrimpBody.ChangeColours(ColourTypes.main);
        }

        base.EndSymptom();
    }
}