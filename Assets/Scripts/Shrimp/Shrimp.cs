using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrimp : MonoBehaviour
{
    public ShrimpStats stats;
    public List<ShrimpActivity> shrimpActivities = new List<ShrimpActivity>();

    public void UpdateShrimp(float elapsedTime)
    {
        float timeRemaining = elapsedTime;  // The time since the last update
        do
        {
            if (shrimpActivities[0] != null)
            {
                Debug.Log(timeRemaining);
                timeRemaining = shrimpActivities[0].StartActivity(elapsedTime);
                if (timeRemaining != 0)
                {
                    shrimpActivities.RemoveAt(0);
                }
            }
            else
            {
                timeRemaining = 0;
            }
        }
        while (timeRemaining > 0);
    }
}