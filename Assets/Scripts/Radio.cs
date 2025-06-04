using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : Interactable
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseHover();
    }

    

    public override void Action()
    {
        Debug.Log("Action");
    }

    public override void OnHover()
    {
        Debug.Log("ThisWorks");
    }
}
