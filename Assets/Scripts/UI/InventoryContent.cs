using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContent : ContentPopulation
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        CreateContent(Inventory.instance.GetItemCount());
        foreach (ContentBlock block in contentBlocks)
        {
            block.SetText("Tank");
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
