using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IllnessController : MonoBehaviour
{
    [SerializeField] IllnessSO[] possibleIllness;
    public List<IllnessSO> currentIllness = new List<IllnessSO>();
    public List<Symptom> currentSymptoms = new List<Symptom>();
    private Shrimp shrimp;
    [SerializeField] float illnessCheckTime = 5f;
    private float illnessCheckTimer;
    [SerializeField] float severityBoostIfSymptomIsAlreadyPresent = 20;

    private void Start()
    {
        shrimp = GetComponent<Shrimp>();
    }

    public void UpdateIllness(float elapsedTime)
    {
        illnessCheckTimer += Time.deltaTime;
        if (illnessCheckTimer > illnessCheckTime)
        {
            foreach (IllnessSO i in possibleIllness)
            {
                if (!currentIllness.Contains(i))
                {
                    CheckForIllness(i, elapsedTime);
                }
            }

            illnessCheckTimer = 0;
        }


        foreach(Symptom s in currentSymptoms)
        {
            s.UpdateSymptom(elapsedTime);
        }
    }

    public void CheckForIllness(IllnessSO so, float elapsedTime)
    {
        float triggerChance = 1;

        switch (so.trigger)
        {
            case IllnessTriggers.RandomChance:
                {
                    triggerChance += (Random.Range(1, 100)); 
                    break;
                }
            case IllnessTriggers.WaterQuality:
                {
                    triggerChance += (-shrimp.tank.waterQuality + 100);
                    break;
                }
            case IllnessTriggers.Hunger:
                {
                    triggerChance += (-shrimp.stats.hunger + 100);
                    break;
                }
            case IllnessTriggers.HotWater:
                {
                    triggerChance += (Mathf.Clamp(shrimp.tank.waterTemperature - 50, 0, 50) * 2);
                    break;
                }
            case IllnessTriggers.ColdWater:
                {
                    triggerChance += (-Mathf.Clamp(shrimp.tank.waterTemperature - 50, -50, 0) * 2);
                    break;
                }
        }

        // Trigger chance is now a value from 1-100 based on the trigger
        if (shrimp.tank.currentIllness.ContainsKey(so))
        {
            float tankInfection = Mathf.Clamp(Mathf.InverseLerp(0, shrimp.tank.roughShrimpCapacity, shrimp.tank.currentIllness[so]) * 100, 0, 100);
            float spreadRate = (shrimp.tank.illnessSpreadRate / 100) + 1;
            triggerChance = Mathf.Clamp(triggerChance + (tankInfection * spreadRate), 0, 100);
        }

        if (Random.value < so.triggerChance.Evaluate(triggerChance / 100))
        {
            AddIllness(so);
        }
    }

    public void AddIllness(IllnessSO i)
    {
        if (currentIllness.Contains(i)) return;

        foreach (IllnessSymptoms s in i.symptoms)
        {
            Symptom symptom;

            switch (s)
            {
                case IllnessSymptoms.BodySize:
                {
                    symptom = new SymptomBodySize();
                    break;
                }

                case IllnessSymptoms.Discolouration:
                {
                    symptom = new SymptomDiscolouration();
                    break;
                }

                case IllnessSymptoms.Bubbles:
                {
                    symptom = new SymptomBubbles();
                    break;
                }

                default:
                {
                    symptom = null;
                    break;
                }
            }

            foreach (Symptom x in currentSymptoms)
            {
                if (x.GetType() == symptom.GetType())
                {
                    symptom = null;
                    x.severity += severityBoostIfSymptomIsAlreadyPresent;
                }
            }

            if (symptom != null && symptom.GetType() != typeof(Symptom))
            {
                currentSymptoms.Add(symptom);
                symptom.shrimp = shrimp;
                symptom.StartSymptom();
            }
        }

        currentIllness.Add(i);

        AddIllnessToTank(shrimp.tank, i);
    }


    public void MoveShrimp(TankController oldTank, TankController newTank)
    {
        foreach (IllnessSO i in currentIllness)
        {
            RemoveIllnessFromTank(oldTank, i);

            AddIllnessToTank(newTank, i);
        }
    }


    public void RemoveShrimp()
    {
        foreach(IllnessSO i in currentIllness)
        {
            RemoveIllnessFromTank(shrimp.tank, i);
        }
    }


    private void AddIllnessToTank(TankController t, IllnessSO i)
    {
        if (t.currentIllness.ContainsKey(i))
            t.currentIllness[i]++;
        else t.currentIllness.Add(i, 1);
    }


    private void RemoveIllnessFromTank(TankController t, IllnessSO i)
    {
        t.currentIllness[i]--;
    }


    public void UseMedicine()
    {

    }
}