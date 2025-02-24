using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyContentBlock : ContentBlock
{


    public enum BackgroundSprites : int
    {
        Common,
        Bronze,
        Silver,
        Gold,
        Diamond
    }

    

    public void SetBackground(BackgroundSprites sprite)
    {
        GetComponent<Image>().sprite = backSprites[(int)sprite];
    }

    
}
