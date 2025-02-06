using PathCreation;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class NodeData
{
    public float GScore;
    public float FScore;
    public Vector3Int CameFrom;
    public Vector3Int Coords;
    public float TimeToReach;

    public NodeData(GridNode point)
    {
        GScore = Mathf.Infinity;
        FScore = Mathf.Infinity;
        CameFrom = new Vector3Int(-1, -1, -1);
        Coords = point.coords;
    }
}

[RequireComponent(typeof(ShrimpAgent))]
public class ShrimpMoveControl : MonoBehaviour
{
    ShrimpAgent _Agent;
    [SerializeField] Transform _MoveToPoint;

    private void Start()
    {
        _Agent = GetComponent<ShrimpAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AgentStatus status = _Agent.Pathfinding(_MoveToPoint.position);
            Debug.Log(status);
        }
    }
}