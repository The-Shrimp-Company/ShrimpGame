using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShrimpActivity
{
    public Shrimp shrimp;
    public float taskTime;
    protected float taskRemainingTime;
    private float elapsedTimeRemaining;
    public bool taskStarted, taskComplete;


    public float Activity(float elapsedTime)
    {

        if (!taskStarted)  // If the task is just starting
            StartActivity();

        elapsedTimeRemaining = elapsedTime - taskRemainingTime;
        taskRemainingTime -= elapsedTime;


        UpdateActivity();


        if (elapsedTimeRemaining > 0)  // They complete the task 
        {
            EndActivity();
            return elapsedTimeRemaining;
        }
        else  // Time runs out during the task
        {
            return 0;
        }
    }


    public virtual void StartActivity()
    {
        if (taskTime == 0) Debug.Log("Task is missing the taskTime value");

        taskRemainingTime = taskTime;  // Set the remaining time
        taskStarted = true;
    }


    public virtual void UpdateActivity()
    {

    }


    public virtual void EndActivity()
    {
        taskComplete = true;
        shrimp.agent.Status = AgentStatus.Finished;
    }
}