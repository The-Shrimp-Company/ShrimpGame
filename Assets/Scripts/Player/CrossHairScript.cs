using UnityEngine;
using UnityEngine.UI;

public class CrossHairScript : PlayerUIController
{
    public Image crosshair;

    private void Start()
    {
        UIManager.instance.Subscribe(this);
    }

    public override void SwitchFocus()
    {
        if (UIManager.instance.GetFocus() == null)
        {
            crosshair.enabled = true;
        }
        else
        {
            crosshair.enabled = false;
        }
    }
}
