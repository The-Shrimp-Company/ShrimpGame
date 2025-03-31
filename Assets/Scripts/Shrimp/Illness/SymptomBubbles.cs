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
        if (shrimp.symptomBubbleParticles != null)
        {
            particleGO = GameObject.Instantiate(shrimp.symptomBubbleParticles, shrimp.transform.position, shrimp.transform.rotation, shrimp.particleParent);
            particleSystem = particleGO.transform.GetChild(0).GetComponent<ParticleSystem>();
            emissionModule = particleSystem.emission;
            particleSystem.collision.AddPlane(shrimp.tank.waterLevel);
        }

        base.StartSymptom();
    }

    public override void UpdateSymptom()
    {
        emissionModule.rateOverTime = 0.75f + (severity / 400);

        base.UpdateSymptom();
    }

    public override void EndSymptom()
    {
        particleGO.GetComponent<DestroyAfter>().enabled = true;
        particleSystem.Stop();

        base.EndSymptom();
    }
}



// Bubble collisions with top
// Speed up when illness is more developed