using PathCreation;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum AgentStatus
{
    Invalid,
    InProgress,
    Finished,
    NewPath
}

public class ShrimpAgent : MonoBehaviour
{
    public float speed;
    public float turnSpeed;
    GridNode startNode;
    GridNode endNode;
    Vector3 startPos;
    Vector3 endPos;
    [HideInInspector] public List<GridNode> totalPath;
    [HideInInspector] public List<GridNode> cornerNodes;
    [SerializeField] float nodeDetectionDistance;

    [SerializeField] bool debugPath;
    [SerializeField] UnityEngine.Color debugPathColour;

    public bool curvePath;
    [HideInInspector] public PathCreator PathCreator;
    [SerializeField] PathCreator _PathCreatorPrefab;
    [SerializeField] float _CornerSmooth;

    [HideInInspector] public TankGrid tankGrid;
    public AgentStatus Status = AgentStatus.Finished;
    private int movingPathIndex;



    private List<GridNode> RetracePath(NodeData start, NodeData current, NodeData[][][] dataSet)
    {
        cornerNodes = new List<GridNode>();
        List<GridNode> totalPath = new List<GridNode>();

        NodeData currentPointData = dataSet[current.Coords.x][current.Coords.y][current.Coords.z];
        GridNode currentPoint = tankGrid.grid[current.Coords.x][current.Coords.y][current.Coords.z];

        totalPath.Add(currentPoint);

        GridNode cameFromPoint = tankGrid.grid[current.CameFrom.x][current.CameFrom.y][current.CameFrom.z];

        Vector3 direction = (currentPoint.coords - cameFromPoint.coords);
        direction = direction.normalized;

        cornerNodes.Add(currentPoint);

        int count = 0;
        while (current.CameFrom.x != -1 && count < 10000)
        {

            currentPoint = tankGrid.grid[current.Coords.x][current.Coords.y][current.Coords.z];
            NodeData cameFromPointData = dataSet[current.CameFrom.x][current.CameFrom.y][current.CameFrom.z];
            cameFromPoint = tankGrid.grid[current.CameFrom.x][current.CameFrom.y][current.CameFrom.z];

            Vector3 dir = (currentPoint.coords - cameFromPoint.coords);
            if (dir != direction)
            {
                cornerNodes.Add(currentPoint);
                direction = dir;
            }

            totalPath.Add(cameFromPoint);
            current = dataSet[current.CameFrom.x][current.CameFrom.y][current.CameFrom.z];
        }

        currentPoint = tankGrid.grid[current.Coords.x][current.Coords.y][current.Coords.z];
        cornerNodes.Add(currentPoint);

        return totalPath;
    }


    private void AddToHeap(List<NodeData> list, int i)
    {
        int parent = (i - 1) / 2;
        if (parent > -1)
        {
            if (list[i].FScore < list[parent].FScore)
            {
                NodeData n = list[i];
                list[i] = list[parent];
                list[parent] = n;
                AddToHeap(list, parent);
            }
        }
    }


    private void RemoveFromHeap(List<NodeData> list, int i)
    {
        int smallest = i;
        int l = 2 * i + 1;
        int r = 2 * i + 2;

        if (l < list.Count && list[l].FScore < list[smallest].FScore)
        {
            smallest = l;
        }
        if (r < list.Count && list[r].FScore < list[smallest].FScore)
        {
            smallest = r;
        }
        if (smallest != i)
        {
            NodeData pom = list[i];
            list[i] = list[smallest];
            list[smallest] = pom;

            // Recursively heapify the affected sub-tree
            RemoveFromHeap(list, smallest);
        }
    }


    public AgentStatus Pathfinding(Vector3 goal, bool supressMovement = false)
    {
        startPos = transform.position;
        endPos = goal;
        startNode = tankGrid.GetClosestNode(transform.position);
        endNode = tankGrid.GetClosestNode(goal);
        if (startNode == endNode || startNode.invalid || endNode.invalid)
        {
            Status = AgentStatus.Invalid;
            return Status;
        }

        NodeData[][][] dataSet = new NodeData[tankGrid.grid.Length][][];
        for (int i = 0; i < dataSet.Length; i++)
        {
            dataSet[i] = new NodeData[tankGrid.grid[i].Length][];
            for (int j = 0; j < dataSet[i].Length; j++)
            {
                dataSet[i][j] = new NodeData[tankGrid.grid[i][j].Length];
            }
        }
        List<NodeData> openSet = new List<NodeData>();

        NodeData startPoint = new NodeData(startNode);
        dataSet[startNode.coords.x][startNode.coords.y][startNode.coords.z] = startPoint;
        startPoint.GScore = 0;

        openSet.Add(startPoint);


        while (openSet.Count > 0)
        {
            NodeData current = openSet[0];


            if (current.Coords == endNode.coords)
            {
                totalPath = RetracePath(startPoint, current, dataSet);
                if (!supressMovement)
                {
                    Status = AgentStatus.InProgress;
                    StartMoving();
                }
                return Status;
            }

            openSet.RemoveAt(0);
            RemoveFromHeap(openSet, 0);

            GridNode currentPoint = tankGrid.grid[current.Coords.x][current.Coords.y][current.Coords.z];

            for (int i = 0; i < currentPoint.neighbours.Count; i++)
            {
                Vector3Int indexes = currentPoint.neighbours[i];
                GridNode neighbour = tankGrid.grid[indexes.x][indexes.y][indexes.z];
                NodeData neighbourData = dataSet[indexes.x][indexes.y][indexes.z];

                bool neighbourPassed = true;
                if (neighbourData == null)
                {
                    neighbourData = new NodeData(neighbour);
                    dataSet[indexes.x][indexes.y][indexes.z] = neighbourData;
                    neighbourPassed = false;
                }

                if (!neighbour.invalid)
                {
                    float tenativeScore = current.GScore + tankGrid.pointDistance;
                    if (tenativeScore < neighbourData.GScore)
                    {
                        neighbourData.CameFrom = current.Coords;
                        neighbourData.GScore = tenativeScore;
                        neighbourData.FScore = neighbourData.GScore + (endNode.worldPos - neighbour.worldPos).sqrMagnitude;
                        if (!neighbourPassed)
                        {
                            openSet.Add(neighbourData);
                            AddToHeap(openSet, openSet.Count - 1);
                        }
                    }
                }
            }
        }

        Status = AgentStatus.Invalid;

        return Status;
    }


    public void FindNewPath()
    {
        if (Status != AgentStatus.NewPath)
        {
            StopAllCoroutines();
            StartCoroutine(C_FindNewPath());
        }
    }


    IEnumerator C_FindNewPath()
    {
        Status = AgentStatus.NewPath;

        GridNode p = tankGrid.GetClosestNode(transform.position);

        while (Status == AgentStatus.NewPath)
        {
            Status = Pathfinding(endPos);
            if (Status == AgentStatus.Invalid)
            {
                Status = AgentStatus.NewPath;
                yield return new WaitForSeconds(0.2f);
            }
        }
    }


    public void CreateBezierPath()
    {
        if (PathCreator == null)
        {
            PathCreator = Instantiate(_PathCreatorPrefab, Vector3.zero, Quaternion.identity);
        }

        List<Vector3> points = new List<Vector3>();


        points.Add(cornerNodes[cornerNodes.Count - 1].worldPos);
        for (int i = cornerNodes.Count - 2; i >= 0; i--)
        {
            points.Add(cornerNodes[i].worldPos);
        }
        points.Add(cornerNodes[0].worldPos);


        BezierPath bezierPath = new BezierPath(points, false, PathSpace.xyz);
        bezierPath.ControlPointMode = BezierPath.ControlMode.Free;
        int cornerIndex = cornerNodes.Count - 1;


        bezierPath.SetPoint(1, cornerNodes[cornerIndex].worldPos, true);
        for (int i = 2; i < bezierPath.NumPoints - 2; i += 3)
        {
            Vector3 position = bezierPath.GetPoint(i + 1) + (cornerNodes[cornerIndex].worldPos - bezierPath.GetPoint(i + 1)) * _CornerSmooth;
            bezierPath.SetPoint(i, position, true);
            if (cornerIndex > 0)
            {
                position = bezierPath.GetPoint(i + 2) + (cornerNodes[cornerIndex - 1].worldPos - bezierPath.GetPoint(i + 2)) * _CornerSmooth;
                bezierPath.SetPoint(i + 2, position, true);
            }
            cornerIndex--;
        }
        bezierPath.SetPoint(bezierPath.NumPoints - 2, cornerNodes[0].worldPos, true);


        bezierPath.NotifyPathModified();
        PathCreator.bezierPath = bezierPath;
    }


    //private void StartMoving()
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(Coroutine_CharacterFollowPath());
    //}


    //IEnumerator Coroutine_CharacterFollowPath()
    //{
    //    Status = AgentStatus.InProgress;
    //    for (int i = totalPath.Count - 1; i >= 0; i--)
    //    {
    //        SetPathColor();
    //        float length = (transform.position - totalPath[i].worldPos).magnitude;
    //        float l = 0;
    //        while (l < length)
    //        {
    //            SetPathColor();
    //            Vector3 forwardDirection = (totalPath[i].worldPos - transform.position).normalized;
    //            if (curvePath)
    //            {
    //                transform.position += transform.forward * Time.deltaTime * speed;
    //                transform.forward = Vector3.Slerp(transform.forward, forwardDirection, Time.deltaTime * turnSpeed);
    //            }
    //            else
    //            {
    //                transform.forward = forwardDirection;
    //                transform.position = Vector3.MoveTowards(transform.position, totalPath[i].worldPos, Time.deltaTime * speed);
    //            }
    //            l += Time.deltaTime * speed;
    //            yield return new WaitForFixedUpdate();
    //        }
    //    }

    //    Status = AgentStatus.Finished;
    //}


    //IEnumerator Coroutine_CharacterFollowPathCurve()
    //{
    //    Status = AgentStatus.InProgress;
    //    CreateBezierPath();

    //    float length = PathCreator.path.length;
    //    float l = 0;

    //    while (l < length)
    //    {
    //        SetPathColor();
    //        transform.position += transform.forward * Time.deltaTime * speed;
    //        Vector3 forwardDirection = (PathCreator.path.GetPointAtDistance(l, EndOfPathInstruction.Stop) - transform.position).normalized;
    //        transform.forward = Vector3.Slerp(transform.forward, forwardDirection, Time.deltaTime * turnSpeed);
    //        l += Time.deltaTime * speed;
    //        yield return new WaitForFixedUpdate();
    //    }

    //    Status = AgentStatus.Finished;
    //}


    public void StartMoving()
    {
        Status = AgentStatus.InProgress;
        movingPathIndex = totalPath.Count - 1;
    }


    public void StopMoving()
    {
        Status = AgentStatus.Finished;
    }


    public float MoveShrimp(float elapsedTime)
    {
        Status = AgentStatus.InProgress;
        float usedTime = 0;
        //Debug.Log(movingPathIndex);
        for (int i = movingPathIndex; i >= 0; i--)
        {
            SetPathColor();
            float length = (transform.position - totalPath[i].worldPos).magnitude;
            float l = 0;
            while (Vector3.Distance(transform.position, totalPath[i].worldPos) > nodeDetectionDistance)
            {
                SetPathColor();
                Vector3 forwardDirection = (totalPath[i].worldPos - transform.position).normalized;
                if (curvePath)
                {
                    transform.position += transform.forward * Time.deltaTime * speed;
                    transform.forward = Vector3.Slerp(transform.forward, forwardDirection, Time.deltaTime * turnSpeed);
                }
                else
                {
                    transform.forward = forwardDirection;
                    transform.position = Vector3.MoveTowards(transform.position, totalPath[i].worldPos, Time.deltaTime * speed);
                }

                l += Time.deltaTime * speed;

                usedTime += Time.deltaTime;
                if (usedTime >= elapsedTime)
                {
                    if (Vector3.Distance(transform.position, totalPath[i].worldPos) <= nodeDetectionDistance) movingPathIndex--;
                    if (movingPathIndex == 0)
                    {
                        Status = AgentStatus.Finished;
                        return usedTime;
                    }

                    return 0;
                }
            }

            // Reach the node
            movingPathIndex--;
        }

        Status = AgentStatus.Finished;
        return usedTime;
    }


    public float GetPathLength()
    {
        float dist = 0;
        Vector3 lastPos = transform.position;
        for (int i = totalPath.Count - 1; i >= 0; i--)
        {
            dist += Vector3.Distance(lastPos, totalPath[i].worldPos);
            lastPos = totalPath[i].worldPos;
        }
        return dist;
    }


    public void SetPathColor()
    {
        if (debugPath)
        {
            if (totalPath != null)
            {
                for (int j = totalPath.Count - 2; j >= 0; j--)
                {
                    Debug.DrawLine(totalPath[j + 1].worldPos, totalPath[j].worldPos, debugPathColour, 1);
                }
            }
        }
    }
}