using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpSelectionWindow : MonoBehaviour
{
    public ShrimpSelectionPopulation screen;

    public void Populate(Request request)
    {
        screen.Populate(request);
    }
}
