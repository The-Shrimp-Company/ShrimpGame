using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShrimpActivity
{
    public Shrimp shrimp;
    public float taskTime;     // How long the task takes
    protected float 
        taskRemainingTime,     // How much time the task will need before it is finished
        elapsedTimeThisFrame,  // How much time they have to use in this frame
        elapsedTimeRemaining;  // How much time is left once they have finished their actions
    public bool taskStarted, taskComplete;


    public float Activity(float elapsedTime)
    {

        if (!taskStarted)  // If the task is just starting
            StartActivity();

        elapsedTimeRemaining = elapsedTime - taskRemainingTime;
        taskRemainingTime -= elapsedTime;
        elapsedTimeThisFrame = elapsedTime;


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


    protected virtual void StartActivity()
    {
        if (taskTime == 0) Debug.Log("Task is missing the taskTime value");

        taskRemainingTime = taskTime;  // Set the remaining time
        taskStarted = true;
    }


    protected virtual void UpdateActivity()
    {

    }


    protected virtual void EndActivity()
    {
        taskComplete = true;
        shrimp.agent.Status = AgentStatus.Finished;
    }
}