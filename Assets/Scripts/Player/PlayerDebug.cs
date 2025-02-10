using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebug : MonoBehaviour
{
    public ShelfSpawn shelves;

    private CameraLookCheck lookCheck;

    // Start is called before the first frame update
    void Start()
    {
        enabled = Debug.isDebugBuild;
        if (enabled)
        {
            lookCheck = GetComponentInChildren<CameraLookCheck>();
        }
    }

    public void OnTestingFunctions()
    {
        shelves.SpawnNextTank();
    }

    public void OnSpawnShrimp()
    {
        if (Money.instance.WithdrawMoney(10))
        {
            GameObject target = lookCheck.LookCheck(1, "Tanks");

            if (target != null)
            {
                target.GetComponent<TankController>().SpawnShrimp();
            }
        }
        else
        {
            Debug.Log("Nuh uh uh");
        }
        
    }

    public void OnAddMoney()
    {
        Money.instance.AddMoney(5);
    }
}
