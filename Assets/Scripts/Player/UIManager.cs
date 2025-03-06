using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager
{
    static public UIManager instance = new UIManager();

    private ScreenView _currentUI = null;

    private List<PlayerUIController> _playerControllers = new List<PlayerUIController>();

    private Rect _currentRect = new Rect();

    private Camera SecondCamera;

    private GameObject _cursor;

    private Transform MainCanvas;

    private TextMeshProUGUI notifBar;

    private string _currentText = "Notifications Online";

    public UIManager()
    {

    }

    public void ChangeFocus()
    {
        _cursor.transform.SetParent(null);

        _cursor.transform.SetAsLastSibling();

        _cursor.SetActive(false);

        MainCanvas.GetComponentInChildren<TabletInteraction>().gameObject.GetComponent<CanvasGroup>().interactable = true;

        
        

        Cursor.lockState = CursorLockMode.Locked;

        if (_currentUI != null)
        {
            _currentUI.Close();
        }
        _currentUI = null;


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
        _cursor.transform.SetParent(newFocus.transform);
        _cursor.transform.SetAsLastSibling();

        if (_currentUI != null)
        {
            _currentUI.Close();
        }
        _currentUI = newFocus;
        _currentRect = customRect.rect;

        _cursor.transform.localScale = Vector3.one;

        Cursor.visible = false;


        Cursor.lockState = CursorLockMode.Confined;

        _cursor.SetActive(true);

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
        _cursor = cursor;
        Cursor.visible = false;
        _cursor.SetActive(false);
    }

    public GameObject GetCursor() { return _cursor; }

    public void SetCanvas(Transform transform)
    {
        MainCanvas = transform;
    }

    public Transform GetCanvas() { return MainCanvas; }

    public void AssignNotifBar(TextMeshProUGUI notif)
    {
        notifBar = notif;
        notifBar.text = _currentText;
    }

    public void SendNotification(string notif)
    {
        _currentText = notif;
        notifBar.text = notif;
    }
}
