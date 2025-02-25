using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreen : ScreenView
{
    protected override void Start()
    {
        shelves = transform.parent.GetComponentInChildren<TabletInteraction>().GetShelves();
    }

    public void BuyTanks()
    {
        Inventory.instance.AddItem(ItemNames.SmallTank);
    }
}
