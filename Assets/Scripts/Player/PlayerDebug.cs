using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebug : MonoBehaviour
{
    public ShelfSpawn shelves;

    // Start is called before the first frame update
    void Start()
    {
        enabled = Debug.isDebugBuild;
    }

    public void OnTestingFunctions()
    {
        shelves.SpawnNextTank();
    }
}
