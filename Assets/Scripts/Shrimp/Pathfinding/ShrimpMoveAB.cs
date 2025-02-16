using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShrimpAgent))]
public class ShrimpMoveAB : MonoBehaviour
{
    ShrimpAgent agent;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;

    private void Start()
    {
        agent = GetComponent<ShrimpAgent>();
        transform.position = pointA.position;
        StartCoroutine(C_MoveAB());
    }

    private void Update()
    {
        Debug.Log(agent.Status);
    }

    IEnumerator C_MoveAB()
    {
        yield return null;
        while (true)
        {
            agent.Pathfinding(pointB.position);
            while (agent.Status == AgentStatus.Invalid)
            {
                Transform pom1 = pointA;
                pointA = pointB;
                pointB = pom1;
                transform.position = pointA.position;
                agent.Pathfinding(pointB.position);
                yield return new WaitForSeconds(0.2f);
            }
            while (agent.Status != AgentStatus.Finished)
            {
                yield return null;
            }
            Transform pom = pointA;
            pointA = pointB;
            pointB = pom;
            yield return null;
        }
    }
}