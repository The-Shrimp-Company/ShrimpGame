using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [HideInInspector] public TankSocket[] _tanks;

    private ShelfSpawn shelves;

    private void OnDisable()
    {
        shelves = GetComponentInParent<ShelfSpawn>();

        _tanks = GetComponentsInChildren<TankSocket>();
    }

    public GameObject AddTank(TankTypes type)
    {
        foreach(TankSocket tank in _tanks)
        {
            if (!tank.TankExists())
            {
                tank.AddTank(type, true);
                tank.tank.tankName = "Tank " + Inventory.instance.activeTanks.Count;
                shelves.SwitchDestinationTank(tank.tank);
                return tank.gameObject;
            }
        }
        return null;
    }

    public ShelfSpawn GetShelves() { return shelves; }
}
