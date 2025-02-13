using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TankViewScript : TabletInteraction
{
    private void Start()
    {
        buttons = GetComponentsInChildren<RectTransform>().Where(x => x.CompareTag("Button")).ToArray();
    }

    protected override void PressedButton()
    {
        switch (_clickedButton)
        {
            case "Set Tank":
                Debug.Log("SetTank attempted");
                break;
            default:
                break;
        }
    }

    public override void Close()
    {
        Destroy(gameObject);
    }
}
