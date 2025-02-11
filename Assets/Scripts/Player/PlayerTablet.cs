using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTablet : MonoBehaviour
{
    [SerializeField]
    private GameObject tablet;
    private RectTransform _tabletRect;
    [SerializeField]
    private float _tabletRestingCoord;
    [SerializeField]
    private float _tabletActiveCoord;
    [SerializeField]
    private RectTransform _tabletBackgroundRect;
    [SerializeField]
    private GameObject cursor;
    [SerializeField]
    private TabletInteraction _tabletInteraction;
    private RectTransform _cursorRect;
    private PlayerInput _input;

    // Start is called before the first frame update
    void Start()
    {
        _tabletRect = tablet.GetComponent<RectTransform>();
        _input = GetComponent<PlayerInput>();
        _cursorRect = cursor.GetComponent<RectTransform>();
        // Sets the tablets position to the resting position. It's already at that position, but just in case
        _tabletRect.position = new Vector3(_tabletRect.position.x, _tabletRestingCoord, 0);
    }

    public void OnOpenTablet()
    {
        // Switches the tablet position based on the current position. I love the ternary operator
        _tabletRect.position = new Vector3(_tabletRect.position.x, _tabletRect.position.y == _tabletRestingCoord ? _tabletActiveCoord : _tabletRestingCoord , 0);
        string nextMap = _input.currentActionMap.name == "Move" ? "UI" : "Move";
        _input.SwitchCurrentActionMap(nextMap);
    }


    public void OnMoveMouse(InputValue Mouse)
    {
        Vector2 mouseMove = Mouse.Get<Vector2>();
        // Move the fake cursor to follow the players mouse movements
        _cursorRect.localPosition += new Vector3(mouseMove.x, mouseMove.y, 0);
        Vector3 mouseClamp = _cursorRect.localPosition;

        // Clamp the cursor to the bounds of the tablet
        mouseClamp.x = Mathf.Clamp(mouseClamp.x, _tabletBackgroundRect.rect.x, _tabletBackgroundRect.rect.x + _tabletBackgroundRect.rect.width);
        mouseClamp.y = Mathf.Clamp(mouseClamp.y, _tabletBackgroundRect.rect.y, _tabletBackgroundRect.rect.y + _tabletBackgroundRect.rect.height);
        _cursorRect.localPosition = mouseClamp;
    }

    public void OnClick()
    {
        _tabletInteraction.MouseClick(_cursorRect.localPosition);
    }
}
