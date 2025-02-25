using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemNames
{
    public static string SmallTank = "Small Tank";
}



public class Inventory
{
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    public static Inventory instance = new Inventory();

    

    public Inventory()
    {
        
    }

    public void AddItem(string newItem, int quantity = 1)
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

    public bool RemoveItem(string item, int quantity = 1)
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

    public Dictionary<string, int> GetInventory()
    {
        return inventory;
    }
}
