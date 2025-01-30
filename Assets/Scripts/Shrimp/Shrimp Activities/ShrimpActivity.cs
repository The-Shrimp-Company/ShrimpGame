using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShrimpActivity
{
    public float taskTime;
    protected float taskRemainingTime;
    private float elapsedTimeRemaining;
    public bool taskComplete;

    public float StartActivity(float elapsedTime)
    {
        if (taskTime == 0)
            Debug.Log("Task is missing the taskTime value");

        if (taskRemainingTime == 0)  // If the task is just starting
            taskRemainingTime = taskTime;  // Set the remaining time

        elapsedTimeRemaining = elapsedTime - taskRemainingTime;
        taskRemainingTime -= elapsedTime;


        UpdateActivity();


        if (elapsedTimeRemaining > 0)  // They complete the task 
        {
            taskComplete = true;
            return elapsedTimeRemaining;
        }
        else  // Time runs out during the task
        {
            return 0;
        }
    }

    public virtual void UpdateActivity()
    {

    }
}