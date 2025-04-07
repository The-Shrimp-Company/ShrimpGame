using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Items
{
    public static Item SmallTank = new Item("Small Tank", 50);
    public static Item Shelf = new Item("Shelf", 100);
    public static Item AlgaeWafer = new Item("Algae Wafer", 10);
    public static Item FoodPellet = new Item("Food Pellet", 5);
    public static Item MedSmallHead = new Medicine("Small Head Pills", 50, IllnessSymptoms.BodySize, 30);
    public static Item MedBubble = new Medicine("Bubble Be Gone", 50, IllnessSymptoms.Bubbles, 30);
    public static Item MedVibrance = new Medicine("Vibrancee", 50, IllnessSymptoms.Discolouration, 30);
}


public class Item
{

    public string itemName;
    public int value;
    public int quantity;

    public Item(string newName, int newValue, int newquantity = 0)
    {
        itemName = newName;
        value = newValue;
        quantity = newquantity;
    }

    public static implicit operator string(Item item)
    {
        return item.itemName;
    }

}


public class Inventory
{
    private List<Item> newInventory = new List<Item>();

    public static Inventory instance = new Inventory();

    public List<TankController> activeTanks { get; private set; } = new List<TankController>();

    public Inventory()
    {
        
    }

    public void AddItem(Item newItem, int quantity = 1)
    {

        for(int i = 0; i < newInventory.Count; i++)
        {
            if (newInventory[i] == newItem.itemName)
            {
                newInventory[i].quantity += quantity;
                return;
            }
        }
        newItem.quantity = quantity;
        newInventory.Add(newItem);
    }

    public bool RemoveItem(Item item, int quantity = 1)
    {
        foreach(Item i in newInventory)
        {
            if(i.itemName == item.itemName)
            {
                if(i.quantity >= quantity)
                {
                    i.quantity -= quantity;
                    if(i.quantity <= 0)
                    {
                        newInventory.Remove(i);
                    }
                    return true;
                }
            }
        }
        return false;

    }

    public int GetItemCount() { return  newInventory.Count; }

    public static int GetItemQuant(Item itemCheck)
    {
        foreach(Item i in instance.newInventory)
        {
            if(i.itemName == itemCheck.itemName)
            {
                return i.quantity;
            }
        }
        return 0;
    }


    public static List<Item> GetInventory()
    {
        return instance.newInventory;
    }

    public static bool Contains(Item itemCheck)
    {
        foreach(Item i in instance.newInventory)
        {
            if(itemCheck.itemName == i.itemName)
            {
                return true;
            }
        }
        return false;
    }
}
