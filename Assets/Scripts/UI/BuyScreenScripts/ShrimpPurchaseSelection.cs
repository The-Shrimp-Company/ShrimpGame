using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpPurchaseSelection : MonoBehaviour
{
    public ShrimpPurchaseContent _screen;

    public void Populate(BuyScreen screen, ref List<ShrimpStats> shrimp)
    {
        _screen.Populate(screen, ref shrimp);
    }
}
