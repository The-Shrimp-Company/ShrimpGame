using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShrimpPurchaseContent : ContentPopulation
{
    public void Populate(BuyScreen screen)
    {
        for(int i = 0; i < 10; i++)
        {
            ShrimpStats s = ShrimpManager.instance.CreateRandomShrimp();
            GameObject block = Instantiate(contentBlock, transform);
            block.GetComponent<ShrimpSelectionBlock>().screen = screen;
            block.GetComponent<ShrimpSelectionBlock>().Populate(s);
            contentBlocks.Add(block.GetComponent<ContentBlock>());
            block.GetComponent<Button>().onClick.AddListener(block.GetComponent<ShrimpSelectionBlock>().BuyThis);
        }
    }
}
