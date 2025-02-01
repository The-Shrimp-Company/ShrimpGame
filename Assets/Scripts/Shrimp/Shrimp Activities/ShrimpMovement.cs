using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpMovement : ShrimpActivity
{
    private Vector3 start, destination;


    public override void StartActivity()
    {
        base.StartActivity();

        start = shrimp.transform.position;
    }


    public override void UpdateActivity()
    {
        float t = taskRemainingTime / taskTime;
        t = -t + 1;
        shrimp.transform.position = Vector3.Lerp(start, destination, t);
    }


    public void SetDestination(Vector3 d) { destination = d; }
}
