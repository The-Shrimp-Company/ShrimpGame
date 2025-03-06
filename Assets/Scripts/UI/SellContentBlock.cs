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
        CustomerManager.Instance.PurchaseShrimp(_shrimp);
        Destroy(gameObject);
    }

    public void SetSalePrice()
    {
        Canvas.ForceUpdateCanvases();
        _salePrice.text = _shrimp.FindValue().ToString();
        FontTools.SizeFont(_salePrice);
    }
}
