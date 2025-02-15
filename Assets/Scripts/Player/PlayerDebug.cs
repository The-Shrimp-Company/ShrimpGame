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
        
    }

    /* Old Function to interact with tanks on click
    public void OnSpawnShrimp()
    {
        GameObject target = lookCheck.LookCheck(1, "Tanks");

        if (target != null)
        {

            TankController tankController = target.GetComponent<TankController>();

            shelves.SwitchSaleTank(tankController);
        }
    }
    */

    /*
    public void OnAddMoney()
    {
        Money.instance.AddMoney(5);
    }
    */
}
