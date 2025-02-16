using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CrossHairScript : PlayerUIController
{
    UnityEngine.UI.Image crosshair;

    private void Start()
    {
        UIManager.instance.Subscribe(this);
        crosshair = GetComponentInChildren<UnityEngine.UI.Image>();
    }

    public override void SwitchFocus()
    {
        if(UIManager.instance.GetFocus() == null)
        {
            crosshair.gameObject.SetActive(true);
        }
        else
        {
            crosshair.gameObject.SetActive(false);
        }
    }
}
