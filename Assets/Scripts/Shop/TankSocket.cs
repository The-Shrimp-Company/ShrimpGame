using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSocket : MonoBehaviour
{
    [SerializeField]
    private GameObject tank;


    public void SetTankActive(bool active)
    {
        tank.SetActive(active);
    }

    public void SetTankActive()
    {
        if (Inventory.instance.RemoveItem(ItemNames.SmallTank))
        {
            tank.SetActive(true);
        }
    }

    public bool GetTankActive()
    {
        return tank.activeInHierarchy;
    }
}
