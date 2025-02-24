using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContent : ContentPopulation
{
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(contentBlock, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
