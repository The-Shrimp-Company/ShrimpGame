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
    [SerializeField]
    private GameObject EmailScreen;
    [SerializeField]
    private GameObject SettingsScreen;
    [SerializeField]
    private GameObject SaveScreen;
    [SerializeField]
    private GameObject VetScreen;

    


    public ShelfSpawn GetShelves() { return shelves; }
    


    public void OpenSell()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GameObject sellScreen = Instantiate(SellScreen, transform.parent.transform);
        UIManager.instance.ChangeFocus(sellScreen.GetComponent<ScreenView>());
        PlayerStats.stats.timesSellingAppOpened++;
    }

    public void OpenBuy()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GameObject buyScreen = Instantiate(BuyScreen, transform.parent.transform);
        UIManager.instance.ChangeFocus(buyScreen.GetComponent<ScreenView>());
        PlayerStats.stats.timesShrimpShopAppOpened++;
    }

    public void OpenUpgrades()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GameObject upgradeScreen = Instantiate(UpgradeScreen, transform.parent.transform);
        UIManager.instance.ChangeFocus(upgradeScreen.GetComponent<ScreenView>());
        PlayerStats.stats.timesItemShopAppOpened++;
    }

    public void OpenInventory()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GameObject inventoryScreen = Instantiate(InventoryScreen, transform.parent.transform);
        UIManager.instance.ChangeFocus(inventoryScreen.GetComponent<ScreenView>());
        PlayerStats.stats.timesInventoryAppOpened++;
    }

    public void OpenEmails()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GameObject emailScreen = Instantiate(EmailScreen, transform.parent.transform);
        UIManager.instance.ChangeFocus(emailScreen.GetComponent<ScreenView>());
        PlayerStats.stats.timesMailAppOpened++;
    }

    public void OpenSettings()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GameObject settingsScreen = Instantiate(SettingsScreen, transform.parent.transform);
        UIManager.instance.ChangeFocus(settingsScreen.GetComponent<ScreenView>());
        PlayerStats.stats.timesSettingsAppOpened++;
    }

    public void OpenSave()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GameObject saveScreen = Instantiate(SaveScreen, transform.parent.transform);
        UIManager.instance.ChangeFocus(saveScreen.GetComponent<ScreenView>());
    }

    public void OpenVet()
    {
        GetComponent<CanvasGroup>().interactable = false;
        GameObject vetScreen = Instantiate(VetScreen, transform.parent.transform);
        UIManager.instance.ChangeFocus(vetScreen.GetComponent<ScreenView>());
    }

    public override void Close(bool switchTab)
    {

    }
}
