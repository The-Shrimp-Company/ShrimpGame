using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymptomDiscolouration : Symptom
{
    public override void StartSymptom()
    {
        symptom = IllnessSymptoms.Discolouration;

        if (shrimp.shrimpLegs != null)
        {
            shrimp.shrimpLegs.ChangeColours(ColourTypes.discoloured);
        }

        base.StartSymptom();
    }

    public override void UpdateSymptom(float elapsedTime)
    {
        base.UpdateSymptom(elapsedTime);
    }

    public override void EndSymptom()
    {
        if (shrimp.shrimpLegs != null)
        {
            shrimp.shrimpLegs.ChangeColours(ColourTypes.main);
        }

        base.EndSymptom();
    }
}