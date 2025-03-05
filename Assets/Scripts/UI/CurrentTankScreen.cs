using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTankScreen : ScreenView
{

    [SerializeField]
    private CurrentTanksContent content;


    public void SetShrimp(Shrimp shrimp)
    {
        content.SetShrimp(shrimp);
    }


}
