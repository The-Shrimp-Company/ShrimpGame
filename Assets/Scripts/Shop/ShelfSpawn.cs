using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShelfSpawn : MonoBehaviour
{
    [HideInInspector] public Shelf[] _shelves;

    private TankController _destinationTank;

    private int _shelfIndex = 0;

    private int _shelfCount = 0;

    public int shelvesToSpawnAtGameStart = 1;
    public int tanksToSpawnAtGameStart = 4;

    public GameObject smallTankPrefab;
    public GameObject largeTankPrefab;

    private void Start()
    {
        _shelves = GetComponentsInChildren<Shelf>();

        _shelves = _shelves.Where(x => x.transform.parent.name == "Shelving").ToArray();

        foreach(Shelf shelf in _shelves)
        {
            shelf.gameObject.SetActive(false);
        }

        if (SaveManager.loadingGameFromFile)
        {
            LoadShelves();
        }
        else
        {
            for (int i = 0; i < shelvesToSpawnAtGameStart; i++)
            {
                SpawnNextShelf();
            }

            for (int i = 0; i < tanksToSpawnAtGameStart; i++)
            {
                SpawnNextTank();
            }
        }
    }

    public Shelf SpawnNextShelf()
    {
        foreach (Shelf shelf in _shelves)
        {
            if(shelf.gameObject.activeSelf == false)
            {
                shelf.gameObject.SetActive(true);
                _shelfCount++;
                PlayerStats.stats.shelfCount = _shelfCount;
                return shelf;
            }
        }

        return null;
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
            if (_destinationTank == null)
            {
                _destinationTank = fullShelfCheck.GetComponentInChildren<TankController>();
                Inventory.instance.activeTanks.Add(_destinationTank);
                _destinationTank.ToggleDestinationTank();
            }
            else
            {
                Inventory.instance.activeTanks.Add(fullShelfCheck.GetComponentInChildren<TankController>());
            }
        }
    }

    public void SwitchDestinationTank(TankController newTank)
    {
        // If we previously had no sale tank
        if (_destinationTank == null)
        {
            _destinationTank = newTank;
            _destinationTank.ToggleDestinationTank();
        }
        else if (_destinationTank != newTank)
        {
            // Switch the old one
            _destinationTank.ToggleDestinationTank();
            _destinationTank = newTank;
            // Switch the new one
            _destinationTank.ToggleDestinationTank();
        }
    }

    public void SpawnShrimp()
    {
        if(_destinationTank != null)
        {
            if (Money.instance.WithdrawMoney(10))
            {
                _destinationTank.SpawnShrimp();
            }
        }
    }

    public bool SpawnShrimp(ShrimpStats s, float price)
    {
        if (_destinationTank != null)
        {
            if (Money.instance.WithdrawMoney(price))
            {
                _destinationTank.SpawnShrimp(s);
                PlayerStats.stats.shrimpBought++;
                return true;
            }
        }
        return false;
    }
    

    private void LoadShelves()
    {
        int s = PlayerStats.stats.shelfCount;
        for (int i = 0; i < s; i++)
        {
            Shelf newShelf = SpawnNextShelf();
            ShelfSaveData data = SaveManager.CurrentSaveData.shelves[i];

            foreach (TankSocketSaveData sData in data.tanks)
            {
                newShelf._tanks[sData.socketNumber].LoadTank(sData);
            }
        }
    }
}

[System.Serializable]
public enum TankTypes
{
    Small,
    Large
}