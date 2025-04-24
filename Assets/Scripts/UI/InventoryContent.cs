using System.Linq;
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
        for(int i = 0; i < Inventory.instance.GetItemCount(); i++)
        {
            contentBlocks[i].SetText(Inventory.GetInventory()[i].itemName);
            contentBlocks[i].GetComponent<InventoryContentBlock>().quantity.text = Inventory.GetInventory()[i].quantity.ToString();
            contentBlocks[i].GetComponent<InventoryContentBlock>().item = Inventory.GetInventory()[i];
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

    public void UpgradeAssignment(TankUpgradeController controller, UpgradeTypes type, ScreenView oldScreen, GameObject parent)
    {
        Button button = transform.parent.GetComponentInChildren<BackButton>().GetComponent<Button>();
        oldScreen.gameObject.SetActive(false);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if (oldScreen != null)
            {
                oldScreen.gameObject.SetActive(true);
            }
            Destroy(parent);
        });

        foreach (InventoryContentBlock block in contentBlocks)
        {
            if(block.item is Upgrade && ((Upgrade)block.item).upgrade.upgradeType == type)
            {
                block.ClearFunctions();
                InventoryContentBlock thisBlock = block;
                block.AssignFunction(() =>
                {
                    if (Inventory.Contains(thisBlock.item))
                    {
                        Inventory.instance.AddItem(controller.GetUpgrade(type).item);
                        controller.RemoveUpgrade(type);
                        controller.AddUpgrade(((Upgrade)thisBlock.item).upgrade);
                        Inventory.instance.RemoveItem(thisBlock.item);
                        if (oldScreen != null)
                        {
                            oldScreen.gameObject.SetActive(true);
                        }
                        Destroy(parent);
                    }
                });
            }
            else
            {
                Destroy(block.gameObject);
            }
        }
    }

    public void FoodAssignement(TankViewScript oldScreen, TankController tank, GameObject parent)
    {
        Button button = transform.parent.GetComponentInChildren<BackButton>().GetComponent<Button>();
        oldScreen.gameObject.SetActive(false);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            oldScreen.gameObject.SetActive(true);
            Destroy(parent);
        });

        foreach(ContentBlock block in contentBlocks)
        {
            if(block.GetText().text == Items.AlgaeWafer)
            {
                block.ClearFunctions();
                ContentBlock thisBlock = block;
                block.AssignFunction(() =>
                {
                    Debug.Log("Getting to algae func");
                    if (Inventory.Contains(Items.AlgaeWafer))
                    {
                        Inventory.instance.RemoveItem(Items.AlgaeWafer);
                        GameObject newFood = Instantiate(algaeWafers, tank.GetRandomSurfacePosition(), Quaternion.identity);
                        newFood.GetComponent<ShrimpFood>().CreateFood(tank);
                        thisBlock.GetComponent<InventoryContentBlock>().quantity.text = Inventory.GetItemQuant(Items.AlgaeWafer).ToString();
                        if (!Inventory.Contains(Items.AlgaeWafer))
                        {
                            Destroy(thisBlock);
                        }
                    }
                });
            }
            else if(block.GetText().text == Items.FoodPellet)
            {
                block.ClearFunctions();
                ContentBlock thisBlock = block;
                block.AssignFunction(() =>
                {
                    if (Inventory.Contains(Items.FoodPellet))
                    {
                        Inventory.instance.RemoveItem(Items.FoodPellet);
                        GameObject newFood = Instantiate(foodPellets, tank.GetRandomSurfacePosition(), Quaternion.identity);
                        newFood.GetComponent<ShrimpFood>().CreateFood(tank);
                        thisBlock.GetComponent<InventoryContentBlock>().quantity.text = Inventory.GetItemQuant(Items.FoodPellet).ToString();
                        if (!Inventory.Contains(Items.FoodPellet))
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

    public void MedAssignment(ScreenView oldScreen, Shrimp shrimp, GameObject parent)
    {
        Shrimp[] shrimpList = new Shrimp[] { shrimp };
        MedAssignment(oldScreen, shrimpList, parent);
    }

    public void MedAssignment(ScreenView oldScreen, Shrimp[] shrimp, GameObject parent)
    {
        Button button = transform.parent.GetComponentInChildren<BackButton>().GetComponent<Button>();
        oldScreen.gameObject.SetActive(false);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if(oldScreen != null)
            {
                oldScreen.gameObject.SetActive(true);
            }
            Destroy(parent);
        });

        foreach(InventoryContentBlock block in contentBlocks)
        {
            if(block.item is Medicine)
            {
                block.ClearFunctions();
                InventoryContentBlock thisBlock = block;
                Shrimp[] thisShrimp = shrimp;
                block.AssignFunction(() =>
                {
                    if(Inventory.GetItemQuant(thisBlock.item) >= thisShrimp.Length)
                    {
                        foreach (Shrimp shrimp in thisShrimp)
                        {
                            shrimp.GetComponent<IllnessController>().UseMedicine(thisBlock.item as Medicine);
                        }
                        Inventory.instance.RemoveItem(thisBlock.item, thisShrimp.Length);
                    }
                    thisBlock.GetComponent<InventoryContentBlock>().quantity.text = Inventory.GetItemQuant(thisBlock.item).ToString();
                    if (Inventory.GetItemQuant(thisBlock.item) <= 0) Destroy(thisBlock.gameObject);
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
