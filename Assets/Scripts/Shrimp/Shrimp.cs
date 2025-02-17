using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrimp : MonoBehaviour
{
    public ShrimpStats stats;
    public List<ShrimpActivity> shrimpActivities = new List<ShrimpActivity>();
    public TankController tank;
    private int minActivitiesInQueue = 2;
    public ShrimpAgent agent;
    [SerializeField]
    private Transform camDock;


    public void Start()
    {
        transform.position = tank.GetRandomTankPosition();
        agent.tankGrid = tank.tankGrid;

        if (shrimpActivities.Count == 0)
        {
            AddActivity(GetRandomActivity());
            AddActivity(GetRandomActivity());
            AddActivity(GetRandomActivity());
        }
    }


    public void UpdateShrimp(float elapsedTime)
    {
        float timeRemaining = elapsedTime;  // The time since the last update
        do
        {
            if (shrimpActivities.Count > 0 && shrimpActivities[0] != null)
            {
                timeRemaining = shrimpActivities[0].Activity(timeRemaining);
                if (timeRemaining != 0)
                {
                    EndActivity();
                }
            }
            else
            {
                timeRemaining = 0;
            }
        }
        while (timeRemaining > 0);
    }


    public void EndActivity()
    {
        shrimpActivities.RemoveAt(0);

        if (shrimpActivities.Count <= minActivitiesInQueue)  // If the shrimp has less 
        {
            AddActivity(GetRandomActivity());
        }
    }


    private ShrimpActivity GetRandomActivity()
    {
        int i = Random.Range(0, 2);
        if (i == 0) return new ShrimpMovement();
        if (i == 1) return new ShrimpSleeping();
        return (new ShrimpActivity());
    }


    private void AddActivity(ShrimpActivity activity)
    {
        if (activity is ShrimpMovement)
        {
            ShrimpMovement movement = (ShrimpMovement) activity;  // Casts the activity to the derrived shrimpMovement activity
            movement.randomDestination = true;
        }


        else if (activity is ShrimpSleeping)
        {
            ShrimpSleeping sleeping = (ShrimpSleeping) activity;

            sleeping.taskTime = Random.Range(4, 8);
        }


        else
        {
            Debug.Log("Activity logic is missing");
        }


        activity.shrimp = this;
        shrimpActivities.Add(activity);
    }


    public void ChangeTank(TankController t)
    {
        tank = t;
        agent.tankGrid = tank.tankGrid;
    }

    public void SetCam()
    {
        UIManager.instance.GetCamera().transform.position = camDock.position;
        UIManager.instance.GetCamera().transform.LookAt(transform.position);
    }
}