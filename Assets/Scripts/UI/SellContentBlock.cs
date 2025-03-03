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
        int stringLength = _salePrice.text.Length;
        Rect textRect = _salePrice.GetComponent<RectTransform>().rect;
        _salePrice.fontSize = textRect.height < textRect.width / stringLength ? textRect.height : textRect.width / stringLength;
    }
}
