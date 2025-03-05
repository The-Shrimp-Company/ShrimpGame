using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentTankContentBlock : ContentBlock
{
    private TankController _tank;
    private Shrimp _shrimp;
    private PlayerInteraction player;

    public void Start()
    {

        player = GameObject.Find("Player").GetComponent<PlayerInteraction>();
    }

    public void SetTank(TankController tank)
    {
        _tank = tank;
    }

    public void SetShrimp(Shrimp shrimp) { _shrimp = shrimp; }

    public void Click()
    {
        _tank.MoveShrimp(_shrimp);
        player.OnExitView();
    }
}
