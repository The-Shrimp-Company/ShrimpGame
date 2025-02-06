using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(ShrimpAgent))]
public class ShrimpMoveRandom : MonoBehaviour
{
    ShrimpAgent agent;

    private void Start()
    {
        agent = GetComponent<ShrimpAgent>();
        StartCoroutine(C_MoveRandom());
    }
    private void Update()
    {
        Debug.Log(agent.Status);
    }
    IEnumerator C_MoveRandom()
    {
        List<GridNode> freePoints = WorldManager.Instance.GetFreePoints();
        GridNode start = freePoints[Random.Range(0, freePoints.Count)];
        transform.position = start.worldPos;
        while (true)
        {
            GridNode p = freePoints[Random.Range(0, freePoints.Count)];
            agent.Pathfinding(p.worldPos);
            while (agent.Status != AgentStatus.Finished && agent.Status != AgentStatus.Invalid)
            {
                yield return null;
            }
        }
    }
}