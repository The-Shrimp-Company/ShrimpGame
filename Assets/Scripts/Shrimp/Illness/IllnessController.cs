using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IllnessController : MonoBehaviour
{
    public List<Symptom> currentSymptoms = new List<Symptom>();

    public void AddIllness(IllnessSO i)
    {
        foreach (IllnessSymptoms s in i.symptoms)
        {
            Symptom symptom;

            switch (s)
            {
                case IllnessSymptoms.WhiteRing:
                {
                    symptom = new SymptomWhiteRing();
                    break;
                }

                case IllnessSymptoms.Discolouration:
                {
                    symptom = new SymptomDiscolouration();
                    break;
                }

                case IllnessSymptoms.LowSpeed:
                {
                    symptom = new SymptomLowSpeed();
                    break;
                }

                case IllnessSymptoms.NotEating:
                {
                    symptom = new SymptomNotEating();
                    break;
                }

                case IllnessSymptoms.Bubbles:
                {
                    symptom = new SymptomBubbles();
                    break;
                }

                default:
                {
                    symptom = new Symptom();
                    break;
                }
            }

            if (symptom != null && symptom.GetType() != typeof(Symptom))
                currentSymptoms.Add(symptom);
        }
    }

    public void UseMedicine()
    {

    }
}