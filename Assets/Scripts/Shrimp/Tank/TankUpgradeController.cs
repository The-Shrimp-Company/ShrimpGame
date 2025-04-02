using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUpgradeController : MonoBehaviour
{
    [SerializeField] Filter filter;

    [SerializeField] Dictionary<UpgradeTypes, Transform> upgradeNodes = new Dictionary<UpgradeTypes, Transform>();

    public void UpdateUpgrades(float elapsedTime)
    {
        if (filter != null) filter.UpdateUpgrade(elapsedTime);
    }

    /*
    public void AddUpgrade(UpgradeTypes upgradeType)
    {
        GameObject upgradePrefab;

        GameObject newUpgrade = GameObject.Instantiate(upgradeNodes[upgradeType]);
    }
    */


    public void RemoveUpgrade()
    {

    }
}


public enum UpgradeTypes
{

}