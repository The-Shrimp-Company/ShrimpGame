using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SellContentBlock : ContentBlock
{
    private Shrimp _shrimp;

    public void SetShrimp(Shrimp shrimp)
    {
        _shrimp = shrimp;
    }

    public void SellShrimp()
    {
        Money.instance.AddMoney(_shrimp.FindValue());
        _shrimp.tank.shrimpToRemove.Add(_shrimp);
        Destroy(gameObject);
    }
}
