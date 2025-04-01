using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUpgrade : MonoBehaviour
{
    public TankController tank;

    public virtual void CreateUpgrade(TankController t)
    {
        tank = t;
    }


    public virtual void UpdateUpgrade(float elapsedTime)
    {

    }


    public virtual void RemoveUpgrade()
    {

    }
}
