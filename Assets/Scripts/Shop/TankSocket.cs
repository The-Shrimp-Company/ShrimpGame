using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSocket : MonoBehaviour
{
    [SerializeField]
    private GameObject tank;

    private ShelfSpawn shelves;

    private void OnEnable()
    {
        shelves = GetComponentInParent<Shelf>().GetShelves();
    }

    public void SetTankActive(bool active)
    {
        GetComponent<BoxCollider>().enabled = !active;
        tank.SetActive(active);
        if (active == true)
        {
            shelves.SwitchSaleTank(tank.GetComponent<TankController>());
        }
    }

    public void SetTankActive()
    {
        if (Inventory.instance.RemoveItem(ItemNames.SmallTank))
        {
            tank.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            shelves.SwitchSaleTank(tank.GetComponent<TankController>());
        }
    }

    public bool GetTankActive()
    {
        return tank.activeInHierarchy;
    }
}
