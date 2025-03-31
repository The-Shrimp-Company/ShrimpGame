using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.UI;

public class InventoryContent : ContentPopulation
{
    [SerializeField] private GameObject algaeWafers, foodPellets;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        CreateContent(Inventory.instance.GetItemCount());
        int index = 0;
        for(int i = 0; i < Items.items.Length; i++)
        {
            if (Inventory.instance.GetInventory().ContainsKey(Items.items[i]))
            {
                contentBlocks[index].SetText(Items.items[i]);
                contentBlocks[index].GetComponent<InventoryContentBlock>().quantity.text = Inventory.instance.GetInventory()[Items.items[i]].ToString();
                index++;
            }
        }
    }

    public void TankAssignment(GameObject tankSocket)
    {
        transform.parent.GetComponentInChildren<BackButton>().gameObject.SetActive(false);

        foreach(ContentBlock block in contentBlocks)
        {
            if (!block.GetText().text.Contains("Tank"))
            {
                Destroy(block.gameObject);
            }
            else
            {
                TankSocket socket = tankSocket.GetComponent<TankSocket>();
                if (block.GetText().text.Contains("Small")) block.AssignFunction(socket.AddSmallTank);
                else if (block.GetText().text.Contains("Large")) block.AssignFunction(socket.AddLargeTank);
                else block.AssignFunction(socket.AddSmallTank);

                block.AssignFunction(UIManager.instance.GetCanvas().GetComponent<MainCanvas>().LowerScreen);
            }
        }
    }

    public void FoodAssignement(TankViewScript oldScreen, TankController tank, GameObject parent)
    {
        Button button = transform.parent.GetComponentInChildren<BackButton>().GetComponent<Button>();
        oldScreen.gameObject.SetActive(false);
        UnityEventTools.RemovePersistentListener(button.onClick, 0);
        button.onClick.AddListener(() =>
        {
            oldScreen.gameObject.SetActive(true);
            Destroy(parent);
        });

        foreach(ContentBlock block in contentBlocks)
        {
            if(block.GetText().text == Items.items[2])
            {
                block.ClearFunctions();
                ContentBlock thisBlock = block;
                block.AssignFunction(() =>
                {
                    Debug.Log("Getting to algae func");
                    if (Inventory.instance.GetInventory()[Items.items[2]] > 0)
                    {
                        Inventory.instance.GetInventory()[Items.items[2]]--;
                        GameObject newFood = Instantiate(algaeWafers, tank.GetRandomSurfacePosition(), Quaternion.identity);
                        newFood.GetComponent<ShrimpFood>().CreateFood(tank);
                        thisBlock.GetComponent<InventoryContentBlock>().quantity.text = Inventory.instance.GetInventory()[Items.items[2]].ToString();
                        if (Inventory.instance.GetInventory()[Items.items[2]] <= 0)
                        {
                            Destroy(thisBlock);
                        }
                    }
                });
            }
            else if(block.GetText().text == Items.items[3])
            {
                block.ClearFunctions();
                ContentBlock thisBlock = block;
                block.AssignFunction(() =>
                {
                    if (Inventory.instance.GetInventory()[Items.items[3]] > 0)
                    {
                        Inventory.instance.GetInventory()[Items.items[3]]--;
                        GameObject newFood = Instantiate(foodPellets, tank.GetRandomSurfacePosition(), Quaternion.identity);
                        newFood.GetComponent<ShrimpFood>().CreateFood(tank);
                        thisBlock.GetComponent<InventoryContentBlock>().quantity.text = Inventory.instance.GetInventory()[Items.items[2]].ToString();
                        if (Inventory.instance.GetInventory()[Items.items[3]] <= 0)
                        {
                            Destroy(thisBlock);
                        }
                    }
                });
            }
            else
            {
                Destroy(block.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
