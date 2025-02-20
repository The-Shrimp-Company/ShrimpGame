using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabletInteraction : ScreenView
{


    [SerializeField]
    private GameObject SellScreen;

    /*
    protected override void Start()
    {
        base.Start();
    }
    */

    

    protected override void PressedButton()
    {
        _clickedButtonUsed = true;
        switch (_clickedButton)
        {
            case "Buy Shrimp":
                shelves.SpawnShrimp();
                break;
            case "Buy Tanks":
                shelves.SpawnNextTank();
                break;
            case "Add Money":
                Money.instance.AddMoney(20);
                break;
            case "Sell":
                GameObject sellScreen = Instantiate(SellScreen, transform.parent.transform);
                UIManager.instance.ChangeFocus(sellScreen.GetComponent<ScreenView>());
                break;
            default:
                _clickedButtonUsed = false;
                break;
        }
        if (_clickedButtonUsed)
        {
            _clickedButton = null;
        }
    }




}
