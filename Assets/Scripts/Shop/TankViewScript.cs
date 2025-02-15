using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TankViewScript : TabletInteraction
{

    private TankController tank;

    private void Start()
    {
        buttons = GetComponentsInChildren<RectTransform>().Where(x => x.CompareTag("Button")).ToArray();
        shelves = GetComponentInParent<ShelfSpawn>();
        tank = GetComponentInParent<TankController>();
    }

    protected override void PressedButton()
    {
        Debug.Log("Hello?");
        switch (_clickedButton)
        {
            case "Set Tank":
                Debug.Log("Yeah");
                shelves.SwitchSaleTank(tank);
                break;
            default:
                break;
        }
        _clickedButton = null;
    }



    public override void Close()
    {
        Destroy(gameObject);
    }
}
