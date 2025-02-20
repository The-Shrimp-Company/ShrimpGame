using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIController : MonoBehaviour
{

    protected Rect _currentAreaRect;
    protected GameObject cursor;
    protected RectTransform _cursorRect;


    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SwitchFocus()
    {
        if (UIManager.instance.GetFocus() != null)
        {
            RectTransform[] temp = UIManager.instance.GetFocus().GetComponentsInChildren<RectTransform>().Where(x => x.CompareTag("Cursor")).ToArray();
            cursor = UIManager.instance.GetCursor();
            _cursorRect = cursor.GetComponent<RectTransform>();
            _currentAreaRect = UIManager.instance.GetCurrentRect();
        }
    }

    public void OnMoveMouse(InputValue Mouse)
    {
        Vector2 mouseMove = Mouse.Get<Vector2>();
        // Move the fake cursor to follow the players mouse movements
        _cursorRect.localPosition += new Vector3(mouseMove.x, mouseMove.y, 0);
        Vector3 mouseClamp = _cursorRect.localPosition;

        // Clamp the cursor to the bounds of the tablet
        mouseClamp.x = Mathf.Clamp(mouseClamp.x, _currentAreaRect.x, _currentAreaRect.x + _currentAreaRect.width);
        mouseClamp.y = Mathf.Clamp(mouseClamp.y, _currentAreaRect.y, _currentAreaRect.y + _currentAreaRect.height);
        _cursorRect.localPosition = mouseClamp;
    }

    public void OnClick(InputValue click)
    {
        if (UIManager.instance.GetFocus() != null)
        {
            UIManager.instance.GetFocus().MouseClick(_cursorRect.transform.position, click.isPressed);
        }
    }
}
