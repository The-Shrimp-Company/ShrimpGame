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
        item = (Upgrade)Items.UpFilt0;
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
        if (tank.shrimpInTank.Count != 0)
        {
            Email email = new Email();
            email.title = "Filter on " + tank.tankName + " has broken down";
            email.subjectLine = "Please fix it";
            email.mainText = "The shrimp could die if the water quality decreases too much";
            EmailManager.SendEmail(email);
        }

        base.BreakUpgrade();
    }
}