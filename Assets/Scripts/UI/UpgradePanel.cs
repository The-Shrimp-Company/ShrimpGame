using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    [Header("Heater")]
    [SerializeField] private Button heaterButton;
    [SerializeField] private Slider heaterSlider;
    [SerializeField] private Button heaterRepair;
    [SerializeField] private TextMeshProUGUI currentTemp;

    [Header("Filter")]
    [SerializeField] private Button filterButton;
    [SerializeField] private Button filterRepair;

    [Header("Decor")]
    [SerializeField] private Button DecorButton;

    [HideInInspector] private TankController tank;
    [HideInInspector] public TankController Tank
    { 
        set
        {
            tank = value;

            if (tank.GetComponent<TankUpgradeController>().CheckForUpgrade(UpgradeTypes.Heater))
                heaterButton.GetComponentInChildren<TextMeshProUGUI>().text = tank.GetComponent<TankUpgradeController>().GetUpgrade(UpgradeTypes.Heater).upgrade.upgradeName;

            if (tank.GetComponent<TankUpgradeController>().CheckForUpgrade(UpgradeTypes.Filter))
                filterButton.GetComponentInChildren<TextMeshProUGUI>().text = tank.GetComponent<TankUpgradeController>().GetUpgrade(UpgradeTypes.Filter).upgrade.upgradeName;
        }
        private get { return tank; }
    }


    public void Update()
    {
        if (tank != null)
        {
            currentTemp.text = tank.waterTemperature.ToString();
        }
    }

    public void ChangeTemp()
    {
        ((Heater)tank.GetComponent<TankUpgradeController>().GetUpgrade(UpgradeTypes.Heater)).SetTargetTemperature(heaterSlider.value);
    }


}
