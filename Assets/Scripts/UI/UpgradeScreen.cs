using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreen : ScreenView
{
    protected override void Start()
    {
        shelves = transform.parent.GetComponentInChildren<TabletInteraction>().GetShelves();
        base.Start();
    }

    public void BuyTanks()
    {
        if (Money.instance.WithdrawMoney(100))
        {
            Inventory.instance.AddItem(Items.items[0]);
        }
    }

    public void BuyShelf()
    {
        if (Money.instance.WithdrawMoney(200))
        {
            shelves.SpawnNextShelf();
        }
    }

    public void BuyAlgaeWafer()
    {
        if (Money.instance.WithdrawMoney(10))
        {
            Inventory.instance.AddItem(Items.items[2], 10);
        }
    }

    public void BuyFoodPellet()
    {
        if (Money.instance.WithdrawMoney(5))
        {
            Inventory.instance.AddItem(Items.items[3], 10);
        }
    }
}
