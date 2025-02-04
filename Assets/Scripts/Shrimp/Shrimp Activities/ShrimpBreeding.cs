using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShrimpBreeding : ShrimpActivity
{
    private float breedDistance = 3;
    private float waitDistance = 6;
    private float breedTime = 5;

    public Shrimp otherShrimp;
    private ShrimpBreeding otherBreeding;  // The breeding script for the other shrimp
    private bool otherIsReady;  // If the other shimp has finished other tasks and is ready to breed

    public bool instigator;  // If this is the shrimp that started it, they will move to the other shrimp
    private bool female;  // Whether this is the female shrimp or the male shrimp
    private bool breeding;  // If they are close enough and have started breeding
    private Vector3 startPos;  // Where the instigator shrimp started moving from


    public override void StartActivity()
    {
        if (shrimp.stats.gender == false)  // Is female
            female = true;

        breeding = false;
        taskTime = Mathf.Infinity;  // The task time will not properly start until they start breeding

        if (instigator)
        {
            startPos = shrimp.transform.position;
        }

        base.StartActivity();
    }


    public override void UpdateActivity()
    {
        if (!breeding && instigator)
        {
            float dist = Vector3.Distance(startPos, otherShrimp.transform.position);

            if (!otherIsReady)
            {
                if (dist > waitDistance)
                {
                    MoveToOther(dist);  // Move over and wait for other
                }
            }

            else  // If the other shimp has finished other tasks and is ready
            {
                if (dist > breedDistance)
                {
                    MoveToOther(dist);
                }

                else
                {
                    breeding = true;
                    taskTime = breedTime;

                    otherBreeding.breeding = true;
                    otherBreeding.taskTime = breedTime;
                }
            }
        }

        else if (!breeding && !instigator)  // Other shimp is waiting for the instigator
        {
            
        }

        else  // If they are breeding
        {
            // Whatever happens while they are breeding
        }
    }


    private void MoveToOther(float dist)
    {
        float moveTime = shrimp.GetComponent<Shrimp>().swimSpeed * dist;

        float t = taskRemainingTime / moveTime;
        t = -t + 1;
        shrimp.transform.position = Vector3.Lerp(startPos, otherShrimp.transform.position, t);
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