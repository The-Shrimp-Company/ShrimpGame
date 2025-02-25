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
    [SerializeField]
    private GameObject BuyScreen;
    [SerializeField]
    private GameObject UpgradeScreen;
    [SerializeField]
    private GameObject InventoryScreen;

    /*
    protected override void Start()
    {
        base.Start();
    }
    */


    public ShelfSpawn GetShelves() { return shelves; }
    

    public void AddMoney()
    {
        Money.instance.AddMoney(20);
    }

    public void OpenSell()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GameObject sellScreen = Instantiate(SellScreen, transform.parent.transform);
        UIManager.instance.ChangeFocus(sellScreen.GetComponent<ScreenView>());
    }

    public void OpenBuy()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GameObject buyScreen = Instantiate(BuyScreen, transform.parent.transform);
        UIManager.instance.ChangeFocus(buyScreen.GetComponent<ScreenView>());
    }

    public void OpenUpgrades()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GameObject upgradeScreen = Instantiate(UpgradeScreen, transform.parent.transform);
        UIManager.instance.ChangeFocus(upgradeScreen.GetComponent<ScreenView>());
    }

    public void OpenInventory()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GameObject inventoryScreen = Instantiate(InventoryScreen, transform.parent.transform);
        UIManager.instance.ChangeFocus(inventoryScreen.GetComponent<ScreenView>());
    }

    public override void Close()
    {

    }
}
