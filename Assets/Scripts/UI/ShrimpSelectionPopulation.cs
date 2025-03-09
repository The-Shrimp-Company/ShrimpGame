using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpSelectionPopulation : ContentPopulation
{
    // Start is called before the first frame update
    void Start()
    {
        CreateContent();
    }

    protected void CreateContent()
    {
        foreach ( Shrimp shrimp in ShrimpManager.instance.allShrimp)
        {
            GameObject block = Instantiate(contentBlock, transform);
            block.GetComponent<ShrimpSelectionBlock>().Populate(shrimp);
            contentBlocks.Add(block.GetComponent<ContentBlock>());
        }
    }
}
