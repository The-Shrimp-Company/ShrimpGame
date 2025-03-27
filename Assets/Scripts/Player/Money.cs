using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money
{
    private float _money = 75;

    static public Money instance = new Money();

    public float money
    {
        get { return _money; }
    }

    public Money()
    {
        // Put loading function here
        _money = 75;
        PlayerStats.stats.totalMoney = _money;
    }

    public void AddMoney(float moneyToAdd)
    {
        _money += moneyToAdd;
        _money = EconomyManager.instance.RoundMoney(_money);
        PlayerStats.stats.totalMoney += moneyToAdd;
        PlayerStats.stats.moneyMade += moneyToAdd;
    }

    public bool WithdrawMoney(float amountToTake)
    {
        if(_money >= amountToTake)
        {
            _money -= amountToTake;
            _money = EconomyManager.instance.RoundMoney(_money);
            PlayerStats.stats.moneySpent += amountToTake;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetMoney(float money)
    {
        _money = money;
        _money = EconomyManager.instance.RoundMoney(_money);
    }
}
