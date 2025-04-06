using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VetScreen : ScreenView
{

    public void BuyBubbleMed()
    {
        if (Money.instance.WithdrawMoney(Items.MedBubble.value))
        {
            Inventory.instance.AddItem(Items.MedBubble);
        }
    }

    public void BuySmallMed()
    {
        if (Money.instance.WithdrawMoney(Items.MedSmallHead.value))
        {
            Inventory.instance.AddItem(Items.MedSmallHead);
        }
    }

    public void BuyVibranceMed()
    {
        if (Money.instance.WithdrawMoney(Items.MedVibrance.value))
        {
            Inventory.instance.AddItem(Items.MedVibrance);
        }
    }
}



