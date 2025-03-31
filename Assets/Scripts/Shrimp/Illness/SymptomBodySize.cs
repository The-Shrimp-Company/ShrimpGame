using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymptomBodySize : Symptom
{
    Transform bodyPart;
    Vector3 startSize = Vector3.one;


    public override void StartSymptom()
    {
        symptom = IllnessSymptoms.BodySize;

        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            bodyPart = shrimp.shrimpBody.headNode;
        }
        else if (rand == 1)
        {
            bodyPart = shrimp.shrimpBody.tailNode.GetChild(0).GetComponent<Tail>().tFanNode;
        }

        base.StartSymptom();
    }

    public override void UpdateSymptom(float elapsedTime)
    {
        if (bodyPart != null)
        {
            float size = 1.25f + ((severity / 400) * 3);
            bodyPart.localScale = new Vector3(size, size, size);
        }

        base.UpdateSymptom(elapsedTime);
    }

    public override void EndSymptom()
    {
        if (bodyPart != null)
        {
            bodyPart.localScale = startSize;
        }

        base.EndSymptom();
    }
}