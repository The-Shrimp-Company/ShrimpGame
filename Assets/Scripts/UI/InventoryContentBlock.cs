using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryContentBlock : ContentBlock
{
    public Item item;

    public TextMeshProUGUI quantity;

    public override void SetText(string textToSet)
    {
        Canvas.ForceUpdateCanvases();
        text.text = textToSet;

        quantity.text = "x" + quantity.text;
    }
}
