using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    [SerializeField] private TextMeshProUGUI notifBar;

    // Start is called before the first frame update
    void Start()
    {
        _tabletRect = tablet.GetComponent<RectTransform>();
        _input = GetComponent<PlayerInput>();
        // Sets the tablets position to the resting position. It's already at that position, but just in case
        RectTools.ChangeRectTransform(_tabletRect, _tabletRestingCoord);
        UIManager.instance.Subscribe(this);
    }

    public void OnOpenTablet()
    {
        UIManager.instance.AssignNotifBar(notifBar);
        _tabletRect.gameObject.SetActive(true);
        RectTools.ChangeRectTransform(_tabletRect, _tabletActiveCoord);
        UIManager.instance.ChangeFocus(_tabletInteraction);
        _tabletInteraction.GetComponent<CanvasGroup>().interactable = true;
        _input.SwitchCurrentActionMap("UI");
    }

    public void OnCloseTablet()
    {
        if (!UIManager.instance.subMenu)
        {
            UIManager.instance.AssignNotifBar(notifBar);
            _tabletRect.gameObject.SetActive(true);
            RectTools.ChangeRectTransform(_tabletRect, _tabletRestingCoord);
            UIManager.instance.ChangeFocus();
            _tabletInteraction.GetComponent<CanvasGroup>().interactable = false;
            _input.SwitchCurrentActionMap("Move");
        }
    }


}
