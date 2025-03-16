using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailScreen : ScreenView
{
    [SerializeField] private GameObject ShrimpSelection;

    protected override void Start()
    {
        base.Start();
        CustomerManager.Instance.EmailOpen(this);
    }

    public void OpenSelection(Request request)
    {
        ShrimpSelectionWindow screen = Instantiate(ShrimpSelection, transform).GetComponent<ShrimpSelectionWindow>();
        screen.Populate(request);
    }

    

}
