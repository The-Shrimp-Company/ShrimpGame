using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpMovement : ShrimpActivity
{
    private ShrimpAgent agent;
    private Vector3 start;
    private GridNode destination;
    private bool simpleMove;
    public bool randomDestination;
    private float minRandDistance = 0.25f;
    private int pathfindingAttempts = 5;


    // Simple Move
    // - If it is a straight path to destination
    // X If pathfinding fails ~5 times 
    // - If the shrimp is off screen


    public override void StartActivity()
    {
        simpleMove = false;

        agent = shrimp.agent;
        start = shrimp.transform.position;


        if (simpleMove)
        {
            SwitchToSimple();
        }
        else 
        {
            SwitchToAdvanced();
        }

        base.StartActivity();
    }


    public override void UpdateActivity()
    {
        if (simpleMove) SimpleMove();
        else AdvancedMove();
    }


    private void SimpleMove()
    {
        float t = taskRemainingTime / taskTime;
        t = -t + 1;
        shrimp.transform.position = Vector3.Lerp(start, destination.worldPos, t);
    }


    private void AdvancedMove()
    {
        float r = agent.MoveShrimp(elapsedTimeThisFrame);


        if (agent.Status == AgentStatus.Finished || agent.Status == AgentStatus.Invalid)
        {
            elapsedTimeRemaining = elapsedTimeThisFrame - r;
            taskRemainingTime = 0;
            //Debug.Log("Reached destination");
        }
    }



    private void SwitchToSimple()
    {
        simpleMove = true;
        float dist = Vector3.Distance(start, destination.worldPos);
        taskTime = shrimp.GetComponent<Shrimp>().agent.speed * dist * 500;
        shrimp.transform.LookAt(destination.worldPos);
    }


    private void SwitchToAdvanced()
    {
        if (simpleMove)
        {
            Vector3 s = shrimp.tank.tankGrid.GetClosestNode(shrimp.transform.position).worldPos;
            shrimp.transform.position = s;
            start = s;
        }

        taskTime = 100;
        simpleMove = false;

        int attempts = 0;
        do  // Look for a random free node until you find one that isn't the current node
        {
            attempts++;
            if (attempts > pathfindingAttempts)
            {
                Debug.Log("Switching to Simple Move");
                SwitchToSimple();
                break;
            }

            if (randomDestination) FindRandomDestination();
            agent.Pathfinding(destination.worldPos);

        } while (agent.Status == AgentStatus.Invalid);
    }


    private void FindRandomDestination()
    {
        GridNode n;
        do  // Look for a random free node until you find one that isn't the current node
        {
            n = shrimp.tank.GetRandomTankNode();
        } while (n == shrimp.tank.tankGrid.GetClosestNode(shrimp.transform.position) || Vector3.Distance(shrimp.transform.position, n.worldPos) < minRandDistance);
        SetDestination(n);
    }


    public void SetDestination(GridNode n) { destination = n; }
}
