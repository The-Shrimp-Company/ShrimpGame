using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SellContentBlock : ContentBlock
{
    private Shrimp _shrimp;
    [SerializeField]
    private TextMeshProUGUI _salePrice;

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

    public void SetSalePrice()
    {
        _salePrice.text = _shrimp.FindValue().ToString();
        //_salePrice.fontSize = 
    }
}
