using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfRef : MonoBehaviour
{
    [SerializeField] private ShelfSpawn shelves;

    public ShelfSpawn GetShelves()
    {
        return shelves;
    }
}
