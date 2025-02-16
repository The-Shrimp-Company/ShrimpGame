using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpMovement : ShrimpActivity
{
    private ShrimpAgent agent;
    private Vector3 start;
    private GridNode destination;
    private bool simpleMove;


    public override void StartActivity()
    {
        simpleMove = false;

        agent = shrimp.agent;
        start = shrimp.transform.position;

        float dist = Vector3.Distance(start, destination.worldPos);
        taskTime = shrimp.GetComponent<Shrimp>().swimSpeed * dist;

        if (!simpleMove)
        {
            agent.Pathfinding(destination.worldPos);
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
        if (shrimp.agent.Status == AgentStatus.Finished || shrimp.agent.Status == AgentStatus.Invalid)
        {

        }
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


    public void SetDestination(GridNode n) { destination = n; }
}
