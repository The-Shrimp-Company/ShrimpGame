using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShrimpPurchaseContent : ContentPopulation
{
    public List<ShrimpStats> _shrimp;

    public void Populate(BuyScreen screen, ref List<ShrimpStats> shrimp)
    {
        _shrimp = shrimp;
        foreach(ShrimpStats s in _shrimp)
        {
            GameObject block = Instantiate(contentBlock, transform);
            block.GetComponent<ShrimpSelectionBlock>().screen = screen;
            block.GetComponent<ShrimpSelectionBlock>().Populate(s, this);
            contentBlocks.Add(block.GetComponent<ContentBlock>());
            block.GetComponent<Button>().onClick.AddListener(block.GetComponent<ShrimpSelectionBlock>().BuyThis);
        }
    }
}
