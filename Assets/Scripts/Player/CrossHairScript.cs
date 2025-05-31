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
            Debug.Log("CHS Line 19");
            if (UIManager.instance.GetFocus() == null)
            {
                Debug.Log("CHS Line 22");
                toolTipText.enabled = true;
            }
            else
            {
                toolTipText.enabled = false;
                crosshair.enabled = false;
            }
        }
    }
}
