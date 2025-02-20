using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShrimpMovement : ShrimpActivity
{
    private ShrimpAgent agent;
    private Vector3 start;
    private GridNode destination;

    private bool simpleMove = false;
    public bool randomDestination;
    private bool simpleIfStraightPath = true;

    private float minRandDistance = 0.25f;
    private int pathfindingAttempts = 5;


    // Simple Move
    // X If it is a straight path to destination
    // X If pathfinding fails ~5 times 
    // - If the shrimp is off screen
    // - Turn smoothly


    protected override void StartActivity()
    {
        agent = shrimp.agent;
        start = shrimp.transform.position;

        if (simpleMove)
        {
            if (randomDestination) FindRandomDestination();

            SwitchToSimple();
        }
        else 
        {
            SwitchToAdvanced();
        }

        base.StartActivity();
    }


    protected override void UpdateActivity()
    {
        if (simpleMove) SimpleMove();
        else AdvancedMove();
    }


    private void SimpleMove()
    {
        float t = taskRemainingTime / taskTime;
        t = -t + 1;
        shrimp.transform.position = Vector3.Lerp(start, destination.worldPos, t);
        shrimp.transform.rotation = Quaternion.RotateTowards(shrimp.transform.rotation, Quaternion.LookRotation((destination.worldPos - shrimp.transform.position), Vector3.up), agent.turnSpeed / 2);
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
        //shrimp.transform.LookAt(destination.worldPos);
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
            if (attempts > pathfindingAttempts)  // Switch to simple if the pathfinding is taking too long
            {
                Debug.Log("Switching to Simple Move");
                SwitchToSimple();
                break;
            }

            if (randomDestination) 
            {
                FindRandomDestination();

                if (simpleIfStraightPath)
                {
                    LayerMask layer = LayerMask.GetMask("Decoration");
                    if (!Physics.Linecast(shrimp.transform.position, destination.worldPos, layer, QueryTriggerInteraction.Ignore))  // Switch to simple if the path has no obstacles
                    {
                        SwitchToSimple();
                        break;
                    }
                }
            }


            agent.Pathfinding(destination.worldPos);  // Calculate the path

        } while (agent.Status == AgentStatus.Invalid);  // Retry if the path was not found
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
