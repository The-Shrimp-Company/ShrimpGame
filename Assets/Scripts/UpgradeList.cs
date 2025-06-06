using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeList : MonoBehaviour
{
    public static UpgradeList instance;

    public List<UpgradeSO> Upgrades;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
