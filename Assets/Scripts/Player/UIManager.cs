using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    static public UIManager instance = new UIManager();

    private TabletInteraction _currentUI = null;

    private List<PlayerTablet> _playerControllers = new List<PlayerTablet>();

    private Rect _currentRect = new Rect();

    public UIManager()
    {

    }

    public void ChangeFocus()
    {
        if(_currentUI != null)
        {
            _currentUI.Close();
        }
        _currentUI = null;
    }

    public void ChangeFocus(TabletInteraction newFocus)
    {
        ChangeFocus(newFocus, newFocus.GetComponent<RectTransform>());

    }

    public void ChangeFocus(TabletInteraction newFocus, RectTransform customRect)
    {
        if(_currentUI != null)
        {
            _currentUI.Close();
        }
        _currentUI = newFocus;
        _currentRect = customRect.rect;
        foreach (PlayerTablet controller in _playerControllers)
        {
            controller.SwitchFocus();
        }
    }

    public TabletInteraction GetFocus()
    {
        return _currentUI;
    }

    public void Subscribe(PlayerTablet newController)
    {
        _playerControllers.Add(newController);
    }

    public Rect GetCurrentRect() { return _currentRect; }
}
