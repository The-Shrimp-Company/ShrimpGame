using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : TankUpgrade
{
    [Header("Filter")]
    public float filterSpeed = 5;

    public override void CreateUpgrade(UpgradeSO u, TankController t)
    {
        base.CreateUpgrade(u, t);
    }


    public override void UpdateUpgrade(float elapsedTime)
    {
        if (working)
        {
            tank.waterQuality = Mathf.Clamp(tank.waterQuality + ((filterSpeed / 5) * elapsedTime), 0, 100);
        }

        base.UpdateUpgrade(elapsedTime);
    }


    public override void RemoveUpgrade()
    {
        base.RemoveUpgrade();
    }


    public override void FixUpgrade()
    {
        base.FixUpgrade();
    }


    public override void BreakUpgrade()
    {
        base.BreakUpgrade();
    }
}