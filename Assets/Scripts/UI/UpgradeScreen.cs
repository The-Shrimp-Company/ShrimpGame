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
        if (Money.instance.WithdrawMoney(Items.SmallTank.value))
        {
            Inventory.instance.AddItem(Items.SmallTank);
        }
    }

    public void BuyShelf()
    {
        if (Money.instance.WithdrawMoney(Items.Shelf.value))
        {
            shelves.SpawnNextShelf();
        }
    }

    public void BuyAlgaeWafer()
    {
        if (Money.instance.WithdrawMoney(Items.AlgaeWafer.value))
        {
            Inventory.instance.AddItem(Items.AlgaeWafer, 10);
        }
    }

    public void BuyFoodPellet()
    {
        if (Money.instance.WithdrawMoney(Items.FoodPellet.value))
        {
            Inventory.instance.AddItem(Items.FoodPellet, 10);
        }
    }

    public void BuyHeater0()
    {
        if (Money.instance.WithdrawMoney(Items.UpHeat0.value))
        {
            Inventory.instance.AddItem(Items.UpHeat0);
        }
    }

    public void BuyHeat1()
    {
        if (Money.instance.WithdrawMoney(Items.UpHeat1.value))
        {
            Inventory.instance.AddItem(Items.UpHeat1);
        }
    }

    public void BuyFilt0()
    {
        if (Money.instance.WithdrawMoney(Items.UpFilt0.value))
        {
            Inventory.instance.AddItem(Items.UpFilt0);
        }
    }
}
