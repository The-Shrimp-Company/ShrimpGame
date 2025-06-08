using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;

public class UIManager : MonoBehaviour
{
    // Old Values
    static public UIManager instance;

    private ScreenView _currentUI = null;

    public bool subMenu = false;

    private List<PlayerUIController> _playerControllers = new List<PlayerUIController>();

    private Rect _currentRect = new Rect();

    private Camera SecondCamera;

    private GameObject _cursor;

    public PlayerInput input;

    public GameObject tooltips { set; private get; }

    private Transform MainCanvas;

    private TextMeshProUGUI notifBar;

    private string _currentText = "Notifications Online";
    // Old Values


    private Stack<ScreenView> _screenStack = new Stack<ScreenView>();


    public void Awake()
    {
        if(instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    public void OpenScreen(ScreenView newScreen)
    {
        newScreen.Open(false);

        if(_screenStack.Count != 0)
        {
            _screenStack.Peek().gameObject.SetActive(false);
        }

        

        _screenStack.Push(newScreen);

        SetPeripherals();

        foreach (PlayerUIController controller in _playerControllers)
        {
            controller.SwitchFocus();
        }
    }

    public void CloseScreen()
    {
        if(_screenStack.Count == 0)
        {
            Debug.LogWarning("You've called close screen with an empty screen stack. That shouldn't happen, probably fix it.");
            return;
        }
        else
        {
            ScreenView oldScreen = _screenStack.Pop();
            if(oldScreen != null)
            {
                oldScreen.Close(false);
            }
        }

        if(_screenStack.Count != 0)
        {
            _screenStack.Peek().gameObject.SetActive(true);
        }

        SetPeripherals();

        foreach (PlayerUIController controller in _playerControllers)
        {
            controller.SwitchFocus();
        }
    }

    public void SwitchScreen(ScreenView newScreen)
    {
        if (_screenStack.Count == 0)
        {
            OpenScreen(newScreen);
            Debug.Log("You've called switch screen from an empty screen stack. Are you sure you meant to do that?");
        }
        else
        {
            if (_screenStack.Count == 0)
            {
                Debug.LogWarning("You've called close screen with an empty screen stack. That shouldn't happen, probably fix it.");
                return;
            }
            else
            {
                ScreenView oldScreen = _screenStack.Pop();
                if (oldScreen != null)
                {
                    oldScreen.Close(false);
                }
            }
            OpenScreen(newScreen);
        }
    }

    public void ClearScreens()
    {
        for(int i = 0; i <= _screenStack.Count; i++)
        {
            CloseScreen();
        }
        Debug.Log(_screenStack.Count);
    }

    /// <summary>
    /// Function to handle setting all the strange settings based on wether there is
    /// currently a screen open.
    /// </summary>
    private void SetPeripherals()
    {
        if(_screenStack.Count == 0)
        {
            _cursor.SetActive(false);
            MainCanvas.GetComponentInChildren<TabletInteraction>().gameObject.GetComponent<CanvasGroup>().interactable = true;
            tooltips.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            input.SwitchCurrentActionMap("Move");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            tooltips.SetActive(false);
            _cursor.SetActive(true);
        }
        Cursor.visible = false;
    }

    public ScreenView GetScreen()
    {
        if(_screenStack.Count > 0)
        {
            return _screenStack.Peek();
        }
        else
        {
            return null;
        }
    }

    public int CheckLevel()
    {
        return _screenStack.Count;
    }


    /// <summary>
    /// Adds the given controller to the notification list, so when focus is switched, the necessary components will be alerted
    /// </summary>
    /// <param name="newController"></param>
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
        if(notifBar == null)
        {
            return;
        }
        notifBar.GetComponent<AudioSource>().Play();
        _currentText = notif;
        notifBar.text = notif;
    }
}
