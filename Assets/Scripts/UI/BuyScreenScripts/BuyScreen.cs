using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScreen : ScreenView
{
    protected override void Start()
    {
        base.Start();
        shelves = transform.parent.GetComponentInChildren<TabletInteraction>().GetShelves();
    }

    public void BuyShrimp()
    {
        shelves.SpawnShrimp();
    }

    public void BuyShrimp(ShrimpStats s)
    {
        shelves.SpawnShrimp(s, EconomyManager.instance.GetShrimpValue(s));
    }
}
