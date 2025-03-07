using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeContent : ContentPopulation
{
    [SerializeField]
    private UpgradeScreen upgradeScreen;

    public void Start()
    {
        CreateContent(Items.items.Length);

    }
}
