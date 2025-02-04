using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShrimpBreeding : ShrimpActivity
{
    private float breedDistance = 3;
    private float breedTime = 5;

    public Shrimp otherShrimp;
    private ShrimpBreeding otherBreeding;  // The breeding script for the other shrimp
    public bool instigator;  // If this is the shrimp that started it, they will move to the other shrimp
    private bool female;  // Whether this is the female shrimp or the male shrimp
    private bool breeding;  // If they are close enough and have started breeding
    private Vector3 startPos;  // Where the instigator shrimp started moving from

    public override void StartActivity()
    {
        if (shrimp.stats.gender == false)  // Is female
            female = true;

        breeding = false;

        if (instigator)
        {
            startPos = shrimp.transform.position;
        }

        base.StartActivity();
    }

    public override void UpdateActivity()
    {
        if (!breeding)
        {
            float dist = Vector3.Distance(startPos, otherShrimp.transform.position);

            if (dist > breedDistance)
            {
                if (instigator)
                {
                    taskTime = shrimp.GetComponent<Shrimp>().swimSpeed * dist;

                    float t = taskRemainingTime / taskTime;
                    t = -t + 1;
                    shrimp.transform.position = Vector3.Lerp(startPos, otherShrimp.transform.position, t);
                }
            }

            else
            {
                breeding = true;
                taskTime = breedTime;
            }
        }
    }

    public override void EndActivity()
    {
        if (female) LayEggs();

        base.EndActivity();
    }

    public void StartBreeding()
    {

    }

    private void LayEggs()
    {

    }
}