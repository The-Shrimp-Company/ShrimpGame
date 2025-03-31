using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymptomBubbles : Symptom
{
    private GameObject particleGO;
    private ParticleSystem particleSystem;
    private ParticleSystem.EmissionModule emissionModule;

    public override void StartSymptom()
    {
        symptom = IllnessSymptoms.Bubbles;

        if (shrimp.symptomBubbleParticles != null)
        {
            particleGO = GameObject.Instantiate(shrimp.symptomBubbleParticles, shrimp.transform.position, shrimp.transform.rotation, shrimp.particleParent);
            particleSystem = particleGO.transform.GetChild(0).GetComponent<ParticleSystem>();
            emissionModule = particleSystem.emission;
            particleSystem.collision.AddPlane(shrimp.tank.waterLevel);
        }

        base.StartSymptom();
    }

    public override void UpdateSymptom(float elapsedTime)
    {
        emissionModule.rateOverTime = 1.0f + (severity / 200);

        base.UpdateSymptom(elapsedTime);
    }

    public override void EndSymptom()
    {
        particleGO.GetComponent<DestroyAfter>().enabled = true;
        particleSystem.Stop();

        base.EndSymptom();
    }
}