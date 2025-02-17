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


    // Simple Move
    // - If it is a straight path to destination
    // - If pathfinding fails ~5 times
    // - If the shrimp is off screen


    public override void StartActivity()
    {
        simpleMove = false;

        agent = shrimp.agent;
        start = shrimp.transform.position;

        if (randomDestination) FindRandomDestination();

        if (simpleMove)
        {
            float dist = Vector3.Distance(start, destination.worldPos);
            taskTime = shrimp.GetComponent<Shrimp>().swimSpeed * dist;
        }
        else 
        {
            do  // Look for a random free node until you find one that isn't the current node
            {
                if (randomDestination) FindRandomDestination();
                agent.Pathfinding(destination.worldPos);
            } while (agent.Status == AgentStatus.Invalid);

            taskTime = 100;//agent.GetPathLength();
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
            Debug.Log("Reached destination");
        }
    }


    private void OnDrawGizmos()
    {
        // Set the color with custom alpha.
        Gizmos.color = new Color(1f, 1f, 0f, 1f); // Red with custom alpha

        // Draw the sphere.
        Gizmos.DrawSphere(destination.worldPos, 1f);
    }

    private void SwitchToSimple()
    {

    }


    private void SwitchToAdvanced()
    {
        Vector3 s = shrimp.tank.tankGrid.GetClosestNode(shrimp.transform.position).worldPos;
        shrimp.transform.position = s;
        start = s;
        agent.Pathfinding(destination.worldPos);
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
