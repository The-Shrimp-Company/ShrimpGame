using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : TankUpgrade
{
    [Header("Filter")]
    public float filterEffectiveness;


    public override void CreateUpgrade(TankController t)
    {
        base.CreateUpgrade(t);
    }


    public override void UpdateUpgrade(float elapsedTime)
    {
        tank.waterQuality = Mathf.Clamp(tank.waterQuality + (filterEffectiveness * elapsedTime), 0, 100);

        base.UpdateUpgrade(elapsedTime);
    }


    public override void RemoveUpgrade()
    {
        base.RemoveUpgrade();
    }
}
