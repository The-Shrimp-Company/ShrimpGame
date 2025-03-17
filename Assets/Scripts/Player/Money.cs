using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money
{
    private float _money = 0;

    static public Money instance = new Money();

    public float money
    {
        get { return _money; }
    }

    public Money()
    {
        // Put loading function here
        _money = 0;
    }

    public void AddMoney(float moneyToAdd)
    {
        _money += moneyToAdd;
        _money = EconomyManager.instance.RoundMoney(_money);
    }

    public bool WithdrawMoney(float amountToTake)
    {
        if(_money >= amountToTake)
        {
            _money -= amountToTake;
            _money = EconomyManager.instance.RoundMoney(_money);
            return true;
        }
        else
        {
            return false;
        }
    }
}
