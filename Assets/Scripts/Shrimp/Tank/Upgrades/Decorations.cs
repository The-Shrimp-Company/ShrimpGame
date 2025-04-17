using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Decorations : TankUpgrade
{
    public override void CreateUpgrade(UpgradeSO u, TankController t)
    {
        base.CreateUpgrade(u, t);
    }


    public override void UpdateUpgrade(float elapsedTime)
    {
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
