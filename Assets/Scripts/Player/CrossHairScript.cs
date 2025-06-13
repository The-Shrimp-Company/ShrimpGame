using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairScript : PlayerUIController
{
    public TextMeshProUGUI toolTipText;
    public Image crosshair;

    private void Start()
    {
        UIManager.instance.Subscribe(this);
    }

    public override void SwitchFocus()
    {
        if (!toolTipText.IsDestroyed())
        {
            if (UIManager.instance.GetScreen() == null)
            {
                toolTipText.enabled = true;
                crosshair.enabled = true;
            }
            else
            {
                toolTipText.enabled = false;
                crosshair.enabled = false;
            }
        }
    }
}
