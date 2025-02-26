using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    private TankSocket[] _tanks;

    private ShelfSpawn shelves;

    private void OnDisable()
    {
        shelves = GetComponentInParent<ShelfSpawn>();

        _tanks = GetComponentsInChildren<TankSocket>();

        foreach (TankSocket tank in _tanks)
        {
            tank.SetTankActive(false);
        }
    }

    public GameObject AddTank()
    {
        foreach(TankSocket tank in _tanks)
        {
            if (!tank.GetTankActive())
            {
                tank.SetTankActive(true);
                return tank.gameObject;
            }
        }
        return null;
    }

    public ShelfSpawn GetShelves() { return shelves; }

    
}
