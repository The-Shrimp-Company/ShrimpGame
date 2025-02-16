using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTablet : PlayerUIController
{
    [SerializeField]
    private GameObject tablet;
    private RectTransform _tabletRect;
    [SerializeField]
    private float _tabletRestingCoord;
    [SerializeField]
    private float _tabletActiveCoord;
    [SerializeField]
    private TabletInteraction _tabletInteraction;
    private PlayerInput _input;

    // Start is called before the first frame update
    void Start()
    {
        _tabletRect = tablet.GetComponent<RectTransform>();
        _input = GetComponent<PlayerInput>();
        _cursorRect = cursor.GetComponent<RectTransform>();
        // Sets the tablets position to the resting position. It's already at that position, but just in case
        _tabletRect.position = new Vector3(_tabletRect.position.x, _tabletRestingCoord, 0);
        UIManager.instance.Subscribe(this);
    }

    public void OnOpenTablet()
    {
        // Switches the tablet position based on the current position. I love the ternary operator
        _tabletRect.position = new Vector3(_tabletRect.position.x, _tabletActiveCoord, 0);
        UIManager.instance.ChangeFocus(_tabletInteraction);

        _input.SwitchCurrentActionMap("UI");
    }

    public void OnCloseTablet() 
    {
        _tabletRect.position = new Vector3(_tabletRect.position.x, _tabletRestingCoord, 0);
        UIManager.instance.ChangeFocus();

        _input.SwitchCurrentActionMap("Move");
    }

    

}
