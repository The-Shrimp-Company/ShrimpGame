using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    static public UIManager instance = new UIManager();

    private ScreenView _currentUI = null;

    private List<PlayerUIController> _playerControllers = new List<PlayerUIController>();

    private Rect _currentRect = new Rect();

    private Camera SecondCamera;

    private GameObject Cursor;

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

        Cursor.transform.parent = null;

        foreach(PlayerUIController controller in _playerControllers)
        {
            controller.SwitchFocus();
        }
    }

    public void ChangeFocus(ScreenView newFocus)
    {
        ChangeFocus(newFocus, newFocus.GetComponent<RectTransform>());

    }

    public void ChangeFocus(ScreenView newFocus, RectTransform customRect)
     {
        if(_currentUI != null)
        {
            _currentUI.Close();
        }
        _currentUI = newFocus;
        _currentRect = customRect.rect;

        Cursor.transform.SetParent(_currentUI.transform, false);

        foreach (PlayerUIController controller in _playerControllers)
        {
            controller.SwitchFocus();
        }
    }

    public ScreenView GetFocus()
    {
        return _currentUI;
    }

    public void Subscribe(PlayerUIController newController)
    {
        _playerControllers.Add(newController);
    }

    public Rect GetCurrentRect() { return _currentRect; }

    public void SetCamera(Camera cam)
    {
        SecondCamera = cam;
    }

    public Camera GetCamera() { return SecondCamera; }

    public void SetCursor(GameObject cursor)
    {
        Cursor = cursor;
    }

    public GameObject GetCursor() { return Cursor; }
}
