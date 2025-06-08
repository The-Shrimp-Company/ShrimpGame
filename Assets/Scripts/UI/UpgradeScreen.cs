using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreen : ScreenView
{
    protected override void Start()
    {
        shelves = transform.parent.GetComponent<ShelfRef>().GetShelves();
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


    public void BuyFilt1()
    {
        if (Money.instance.WithdrawMoney(Items.UpFilt1.value))
        {
            Inventory.instance.AddItem(Items.UpFilt1);
        }
    }

    public void BuyDecor(string decor)
    {
        if (Money.instance.WithdrawMoney(30))
        {
            switch (decor)
            {
                case "GM":
                    Inventory.instance.AddItem(Items.DecorGM);
                    break;
                case "DL":
                    Inventory.instance.AddItem(Items.DecorDL);
                    break;
                case "RG":
                    Inventory.instance.AddItem(Items.DecorRG);
                    break;
                case "WR":
                    Inventory.instance.AddItem(Items.DecorWR);
                    break;
                case "LP":
                    Inventory.instance.AddItem(Items.DecorLP);
                    break;
                default:
                    Debug.LogWarning("Missing decoration, something is wrong");
                    break;
            }
        }
    }
}
