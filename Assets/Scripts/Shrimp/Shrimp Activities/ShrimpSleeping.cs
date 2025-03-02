using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpSleeping : ShrimpActivity
{
    private float bobSpeed = 0.5f;
    private float bobMagnitude = 0.1f;

    protected override void UpdateActivity()
    {
        shrimp.transform.position = shrimp.transform.position + shrimp.transform.up * Mathf.Sin(Time.time * bobSpeed) * (bobMagnitude / 5000);
    }
}
