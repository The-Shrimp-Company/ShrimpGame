using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BuyContent : ContentPopulation
{
    [SerializeField]
    private BuyScreen screen;


    // Start is called before the first frame update
    void Start()
    {
        BuyContentBlock item = Instantiate(contentBlock, transform).GetComponentInChildren<BuyContentBlock>();
        item.SetBackground(BuyContentBlock.BackgroundSprites.Bronze);
        item.SetText("Shrimp");
        item.SetScreen(screen);
        //item.GetComponent<BuyContentBlock>().AssignFunction(screen.BuyShrimp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
