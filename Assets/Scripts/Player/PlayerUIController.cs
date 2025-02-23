using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

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
            //cursor = UIManager.instance.GetCursor();
            //_cursorRect = cursor.GetComponent<FakeCursor>().getCursorRect();
            _currentAreaRect = UIManager.instance.GetCurrentRect();
        }
    }

    public void OnMoveMouse()
    {
        Vector2 pos = Mouse.current.position.value;
        RectTransform uiPanel = UIManager.instance.GetFocus().GetComponent<RectTransform>();
        pos.x = Mathf.Clamp(pos.x, uiPanel.position.x - uiPanel.rect.width / 2, uiPanel.position.x + uiPanel.rect.width / 2);
        pos.y = Mathf.Clamp(pos.y, uiPanel.position.y - uiPanel.rect.height / 2, uiPanel.position.y + uiPanel.rect.height / 2);
        Mouse.current.WarpCursorPosition(pos);
    }
}
