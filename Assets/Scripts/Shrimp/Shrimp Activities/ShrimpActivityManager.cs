using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShrimpActivityManager : MonoBehaviour
{
    public static ShrimpActivityManager instance;


    public void Awake()
    {
        instance = this;
    }


    public ShrimpActivity GetRandomActivity(Shrimp shrimp)
    {
        int i = Random.Range(0, 4);
        if (i == 0) return new ShrimpMovement();
        if (i == 1) return new ShrimpSleeping();
        if (i == 2) return new ShrimpBreeding();
        if (i == 3) return new ShrimpEating();
        return (new ShrimpActivity());
    }


    public void AddActivity(Shrimp shrimp, ShrimpActivity activity = null, bool randomActivity = true)
    {
        if (shrimp != null)
        {
            if (randomActivity) activity = GetRandomActivity(shrimp);

            if (activity is ShrimpMovement)
            {
                ShrimpMovement movement = (ShrimpMovement)activity;  // Casts the activity to the derrived shrimpMovement activity
                movement.randomDestination = true;
            }


            else if (activity is ShrimpSleeping)
            {
                ShrimpSleeping sleeping = (ShrimpSleeping)activity;

                sleeping.taskTime = Random.Range(4, 8);
            }


            else if (activity is ShrimpBreeding)
            {
                // Find other shrimp
                List<Shrimp> validShrimp = new List<Shrimp>();
                foreach (Shrimp s in shrimp.tank.shrimpInTank)
                {
                    if (s.stats.gender != shrimp.stats.gender)  // Get all shrimp of the opposite gender, also excludes this shrimp
                    {
                        // Other logic for who can breed here
                        // Once every molt for female
                        if (s.stats.canBreed &&
                            s.stats.canBreed)
                        {
                            validShrimp.Add(s);
                        }
                    }
                }

                if (validShrimp.Count == 0)  // If there are no valid shrimp
                {
                    AddActivity(shrimp, GetRandomActivity(shrimp));
                    return;  // Cancel this and find a different activity
                }

                int i = Random.Range(0, validShrimp.Count);
                Shrimp otherShrimp = validShrimp[i];

                ShrimpBreeding otherBreeding = new ShrimpBreeding();
                otherBreeding.instigator = false;
                otherBreeding.shrimp = otherShrimp;
                otherBreeding.otherShrimp = shrimp;
                otherShrimp.shrimpActivities.Add(otherBreeding);



                ShrimpBreeding breeding = (ShrimpBreeding)activity;
                breeding.instigator = true;
                breeding.otherShrimp = otherShrimp;
            }


            else if (activity is ShrimpEating)
            {
                if (shrimp.tank.foodInTank.Count == 0)  // If there are no valid shrimp
                {
                    AddActivity(shrimp, GetRandomActivity(shrimp));
                    return;  // Cancel this and find a different activity
                }

                int i = Random.Range(0, shrimp.tank.foodInTank.Count);
                ShrimpFood food = shrimp.tank.foodInTank[i];

                if (food.shrimpEating != null)  // If a shrimp is already eating it
                {
                    AddActivity(shrimp, GetRandomActivity(shrimp));
                    return;  // Cancel this and find a different activity
                }

                food.shrimpEating = shrimp;

                ShrimpEating eating = (ShrimpEating)activity;
                eating.food = food;
            }


            else
            {
                Debug.Log("Activity logic is missing");
            }

            activity.shrimp = shrimp;
            activity.CreateActivity();
            shrimp.shrimpActivities.Add(activity);
        }
    }
}
