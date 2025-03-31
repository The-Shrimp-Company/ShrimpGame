using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Items
{
    /// <summary>
    /// 0 = Small Tank, 1 = Shelf, 2 = Algae Wafer, 3 = Food Pellet
    /// </summary>
    public static Item[] items = { new Item("Small Tank", 50), new Item("Shelf", 100), new Item("AlgaeWafer", 10), new Item("FoodPellet", 5) };
}


public struct Item
{
    public Item(string newName, int newValue)
    {
        name = newName;
        value = newValue;
    }

    public static implicit operator string(Item item)
    {
        return item.name;
    }

    public string name;
    public int value;
}


public class Inventory
{
    private Dictionary<Item, int> inventory = new Dictionary<Item, int>();

    public static Inventory instance = new Inventory();

    public List<TankController> activeTanks { get; private set; } = new List<TankController>();

    public Inventory()
    {
        
    }

    public void AddItem(Item newItem, int quantity = 1)
    {
        if (inventory.ContainsKey(newItem))
        {
            inventory[newItem] += quantity;
        }
        else
        {
            inventory.Add(newItem, quantity);
        }
    }

    public bool RemoveItem(Item item, int quantity = 1)
    {
        if (inventory.ContainsKey(item))
        {
            if (inventory[item] >= quantity)
            {
                inventory[item] -= quantity;
                if (inventory[item] == 0)
                {
                    inventory.Remove(item);
                }
                return true;
            }
        }
        return false;
    }

    public int GetItemCount() { return  inventory.Count; }

    public Dictionary<Item, int> GetInventory()
    {
        return inventory;
    }
}
