using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUpgradeController : MonoBehaviour
{
    private Dictionary<UpgradeTypes, TankUpgrade> upgradeScripts = new Dictionary<UpgradeTypes, TankUpgrade>();
    [SerializeField] Dictionary<UpgradeTypes, Transform> upgradeNodes = new Dictionary<UpgradeTypes, Transform>();


    public void UpdateUpgrades(float elapsedTime)
    {
        foreach (KeyValuePair<UpgradeTypes, TankUpgrade> upgrade in upgradeScripts)
        {
            if (upgrade.Value != null)
            {
                upgrade.Value.UpdateUpgrade(elapsedTime);
            }
        }
    }


    public void AddUpgrade(GameObject upgradePrefab, UpgradeTypes upgradeType)
    {
        if (!upgradeNodes.ContainsKey(upgradeType))
        {
            Debug.LogWarning("Upgrade Nodes is missing the key " + upgradeType.ToString());
            return;
        }


        if (upgradeNodes[upgradeType].childCount != 0)
        {
            RemoveUpgrade(upgradeType);
        }


        GameObject newUpgrade = GameObject.Instantiate(upgradePrefab, upgradeNodes[upgradeType].position, upgradeNodes[upgradeType].rotation, upgradeNodes[upgradeType]);
        TankUpgrade upgradeScript = newUpgrade.GetComponent<TankUpgrade>();

        if (!upgradeScripts.ContainsKey(upgradeType))
            upgradeScripts.Add(upgradeType, upgradeScript);
        else
            upgradeScripts[upgradeType] = upgradeScript;

        upgradeScript.CreateUpgrade(GetComponent<TankController>());
    }


    public void RemoveUpgrade(UpgradeTypes upgradeType)
    {
        // Add it back to the inventory?



        if (upgradeScripts.ContainsKey(upgradeType))
        {
            upgradeScripts[upgradeType].RemoveUpgrade();
            upgradeScripts[upgradeType] = null;
        }

        if (upgradeNodes.ContainsKey(upgradeType))
        {
            if (upgradeNodes[upgradeType].childCount != 0)
                Destroy(upgradeNodes[upgradeType].GetChild(0));
        }
    }
}


public enum UpgradeTypes
{
    Filter,
    Decorations
}