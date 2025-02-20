using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellScreenView : ScreenView
{
    public override void Close()
    {
        Destroy(gameObject);
    }
}
