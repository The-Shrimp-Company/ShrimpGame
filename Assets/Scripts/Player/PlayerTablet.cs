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
    private RectTransform _tabletRestingCoord;
    [SerializeField]
    private RectTransform _tabletActiveCoord;
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
        RectTools.ChangeRectTransform(_tabletRect, _tabletRestingCoord);
        UIManager.instance.Subscribe(this);
    }

    public void OnOpenTablet()
    {
        RectTools.ChangeRectTransform(_tabletRect, _tabletActiveCoord);
        UIManager.instance.ChangeFocus(_tabletInteraction);

        _input.SwitchCurrentActionMap("UI");
    }

    public void OnCloseTablet() 
    {
        RectTools.ChangeRectTransform(_tabletRect, _tabletRestingCoord);
        UIManager.instance.ChangeFocus();

        _input.SwitchCurrentActionMap("Move");
    }

    

}
