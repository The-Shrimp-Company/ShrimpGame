using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyContent : ContentPopulation
{
    [SerializeField]
    private BuyScreen screen;

    // Start is called before the first frame update
    void Start()
    {
        GameObject item = Instantiate(contentBlock, transform);
        item.GetComponent<BuyContentBlock>().SetBackground(BuyContentBlock.BackgroundSprites.Silver);
        item.GetComponent<BuyContentBlock>().SetText("Shrimp");
        item.GetComponent<BuyContentBlock>().AssignFunction(screen.BuyShrimp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
