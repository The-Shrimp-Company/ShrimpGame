using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellContent : ContentPopulation
{
    // Start is called before the first frame update
    void Awake()
    {
        CreateContent();
    }

    protected void CreateContent()
    {
        foreach (Shrimp shrimp in ShrimpManager.instance.allShrimp)
        {
            SellContentBlock tempBlock = Instantiate(contentBlock, transform).GetComponent<SellContentBlock>();
            tempBlock.SetText(shrimp.name);
            tempBlock.SetShrimp(shrimp);
            tempBlock.SetSalePrice();
            contentBlocks.Add(tempBlock);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
