using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScreen : ScreenView
{
    protected override void Start()
    {
        shelves = transform.parent.GetComponentInChildren<TabletInteraction>().GetShelves();
    }

    public void BuyShrimp()
    {
        shelves.SpawnShrimp();
    }
}
