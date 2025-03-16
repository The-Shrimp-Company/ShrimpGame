using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShelfSpawn : MonoBehaviour
{
    private Transform[] _shelves;

    private TankController _saleTank;

    private int _shelfIndex = 0;

    private void Start()
    {
        _shelves = GetComponentsInChildren<Transform>();

        _shelves = _shelves.Where(x => x.parent.name == "Shelving").ToArray();

        foreach(Transform shelf in _shelves)
        {
            shelf.gameObject.SetActive(false);
        }

        SpawnNextShelf();
    }

    public void SpawnNextShelf()
    {
        foreach (Transform shelf in _shelves)
        {
            if(shelf.gameObject.activeSelf == false)
            {
                shelf.gameObject.SetActive(true);
                return;
            }
        }
    }

    public void SpawnNextTank()
    {

        GameObject fullShelfCheck = null;
        while (fullShelfCheck == null && _shelfIndex < _shelves.Length)
        {
            fullShelfCheck = _shelves[_shelfIndex].GetComponent<Shelf>().AddTank();
            if (fullShelfCheck == null)
            {
                if(_shelfIndex < _shelves.Length - 1 )
                {
                    SpawnNextShelf();
                    _shelfIndex++;
                }
                else
                {
                    Debug.Log("OOF: Out of Fhelves...");
                    break;
                }
            }
            if (_saleTank == null)
            {
                _saleTank = fullShelfCheck.GetComponentInChildren<TankController>();
                _saleTank.ToggleSaleTank();
            }
        }
    }

    public void SwitchSaleTank(TankController newTank)
    {
        // If we previously had no sale tank
        if (_saleTank == null)
        {
            _saleTank = newTank;
            _saleTank.ToggleSaleTank();
        }
        else if (_saleTank != newTank)
        {
            // Switch the old one
            _saleTank.ToggleSaleTank();
            _saleTank = newTank;
            // Switch the new one
            _saleTank.ToggleSaleTank();
        }
    }

    public void SpawnShrimp()
    {
        if(_saleTank != null)
        {
            if (Money.instance.WithdrawMoney(10))
            {
                _saleTank.SpawnShrimp();
            }
        }
    }

    public void SpawnShrimp(ShrimpStats s, float price)
    {
        if (_saleTank != null)
        {
            if (Money.instance.WithdrawMoney(price))
            {
                _saleTank.SpawnShrimp(s);
            }
        }
    }
    
}
