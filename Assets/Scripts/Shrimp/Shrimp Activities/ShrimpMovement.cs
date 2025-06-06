using UnityEngine;

public class ShrimpMovement : ShrimpActivity
{
    private ShrimpAgent agent;
    private Vector3 start;
    private GridNode destinationNode;
    public Vector3 destinationPos;

    public bool simpleMove = false;
    public bool randomDestination;
    private bool simpleIfStraightPath = true;

    private float minRandDistance = 0.25f;
    private int pathfindingAttempts = 5;

    public bool endWhenDestinationReached = true;

    private bool debugMovement = false;


    // Simple Move
    // X If it is a straight path to destination
    // X If pathfinding fails ~5 times 
    // - If the shrimp is off screen
    // - Turn smoothly

    public override void CreateActivity()
    {
        activityName = "Moving";
    }


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
        shrimp.transform.position = Vector3.Lerp(start, destinationPos, t);
        if (destinationPos - shrimp.transform.position != Vector3.zero)
            shrimp.agent.shrimpModel.rotation = Quaternion.RotateTowards(shrimp.agent.shrimpModel.rotation, Quaternion.LookRotation((destinationPos - shrimp.transform.position), Vector3.up), agent.simpleTurnSpeed * elapsedTimeThisFrame);
    }


    private void AdvancedMove()
    {
        float r = agent.MoveShrimp(elapsedTimeThisFrame);


        if ((agent.Status == AgentStatus.Finished || agent.Status == AgentStatus.Invalid) && endWhenDestinationReached)
        {
            elapsedTimeRemaining = elapsedTimeThisFrame - r;
            taskRemainingTime = 0;
            if (debugMovement) Debug.Log("Reached destination");
        }
    }



    public void SwitchToSimple()
    {
        simpleMove = true;
        float dist = Vector3.Distance(start, destinationPos);
        taskTime = shrimp.GetComponent<Shrimp>().agent.speed * dist * 500;
    }


    public void SwitchToAdvanced()
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
                if (debugMovement) Debug.Log("Switching to Simple Move");
                SwitchToSimple();
                break;
            }

            if (randomDestination) 
            {
                FindRandomDestination();

                if (simpleIfStraightPath)
                {
                    LayerMask layer = LayerMask.GetMask("Decoration");
                    if (!Physics.Linecast(shrimp.transform.position, destinationNode.worldPos, layer, QueryTriggerInteraction.Ignore))  // Switch to simple if the path has no obstacles
                    {
                        SwitchToSimple();
                        break;
                    }
                }
            }

            if (agent == null)
            {
                Debug.Log(agent + " - " + destinationNode);
            }

            agent.Pathfinding(destinationNode.worldPos);  // Calculate the path


            if (debugMovement && agent.Status == AgentStatus.Invalid)
                Debug.LogError("Path Invalid - Start Node" + shrimp.transform.position + " - End Node" + destinationNode);

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


    public void SetDestination(GridNode n) 
    { 
        destinationNode = n; 
        destinationPos = destinationNode.worldPos;
    }
}
