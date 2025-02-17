using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SwimmingController : MonoBehaviour
{
    ShrimpAgent _Agent;
    [SerializeField] Transform _MoveToPoint;
    [SerializeField] AnimationCurve _SpeedCurve;
    [SerializeField] float _Speed;
    private void Start()
    {
        _Agent = GetComponent<ShrimpAgent>();
        StartCoroutine(Coroutine_MoveRandom());
    }

    IEnumerator Coroutine_MoveRandom()
    {
        List<GridNode> freePoints = _Agent.tankGrid.GetFreePoints();
        GridNode start = freePoints[Random.Range(0, freePoints.Count)];
        transform.position = start.worldPos;
        while (true)
        {
            GridNode p = freePoints[Random.Range(0, freePoints.Count)];

            _Agent.Pathfinding(p.worldPos);
            while (_Agent.Status != AgentStatus.Finished)
            {
                yield return null;
            }
        }
    }
}