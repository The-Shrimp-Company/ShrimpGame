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
    public static Item MedSmallHead = new Medicine("Small Head Pills", 20, IllnessSymptoms.BodySize, 30, 2);
    public static Item MedBubble = new Medicine("Bubble Be Gone", 20, IllnessSymptoms.Bubbles, 30, 2);
    public static Item MedVibrance = new Medicine("Vibrancee", 20, IllnessSymptoms.Discolouration, 30, 2);
    public static Item UpHeat0 = new Upgrade("H001");
    public static Item UpHeat1 = new Upgrade("H002");
    public static Item UpFilt0 = new Upgrade("F001");
    public static Item UpFilt1 = new Upgrade("F002");
    public static Item DecorGM = new Upgrade("D001");
    public static Item DecorDL = new Upgrade("D002");
    public static Item DecorRG = new Upgrade("D003");
    public static Item DecorWR = new Upgrade("D004");
    public static Item DecorLP = new Upgrade("D005");
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
