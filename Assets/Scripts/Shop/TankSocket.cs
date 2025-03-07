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
        if (Inventory.instance.RemoveItem(Items.items[0]))
        {
            tank.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            shelves.SwitchSaleTank(tank.GetComponent<TankController>());
            Inventory.instance.activeTanks.Add(tank.GetComponent<TankController>());
            tank.GetComponent<TankController>().tankName = "Tank " + Inventory.instance.activeTanks.Count;
        }
    }

    public bool GetTankActive()
    {
        return tank.activeInHierarchy;
    }
}
