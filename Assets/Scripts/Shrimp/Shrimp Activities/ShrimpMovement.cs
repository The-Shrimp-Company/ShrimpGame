using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpMovement : ShrimpActivity
{
    private Vector3 start, destination;
    private bool simpleMove;


    public override void StartActivity()
    {
        simpleMove = true;

        start = shrimp.transform.position;

        float dist = Vector3.Distance(start, destination);
        taskTime = shrimp.GetComponent<Shrimp>().swimSpeed * dist;

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
        shrimp.transform.position = Vector3.Lerp(start, destination, t);
    }


    private void AdvancedMove()
    {

    }


    public void SetDestination(Vector3 d) { destination = d; }
}
