using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class PlayerUIController : MonoBehaviour
{
    private ShrimpCam _cam;

    protected Rect _currentAreaRect;
    protected GameObject cursor;
    protected RectTransform _cursorRect;

    private float control;
    private float zoom;

    public void OnShrimpCamMove(InputValue input)
    {
        if (UIManager.instance.GetFocus().GetComponent<ShrimpView>() != null)
        {
            if(_cam != null)
            {
                control = input.Get<Vector2>().x;
                zoom = input.Get<Vector2>().y;
            }
        }
    }


    public void Update()
    {
        if (control != 0)
        {
            _cam.transform.parent.Rotate(0, -control, 0);
        }
        if (zoom < 0)
        {
            _cam.ChangeZoom(false);
        }
        else if (zoom > 0)
        {
            _cam.ChangeZoom(true);
        }
    }

    public virtual void SwitchFocus()
    {
        if (UIManager.instance.GetFocus() != null)
        {
            _currentAreaRect = UIManager.instance.GetCurrentRect();
        }
    }

    public void OnMoveMouse()
    {
        Vector2 pos = Mouse.current.position.value;
        RectTransform uiPanel = UIManager.instance.GetFocus().GetComponent<RectTransform>();
        float localScale = UIManager.instance.GetCanvas().GetComponent<Canvas>().scaleFactor;
        Vector2 scale = UIManager.instance.GetCanvas().GetComponent<RectTransform>().localScale;

        //Vector2 bottomCorner = new Vector2(uiPanel.anchorMin, centre.center.y * localScale - (uiPanel.rect.height * localScale) / 2);
        //ector2 topCorner = new Vector2(centre.center.x * localScale + (uiPanel.rect.width * localScale) /2, centre.center.y * localScale + (uiPanel.rect.height * localScale) / 2);

        Vector2 bottomCorner = Camera.main.WorldToScreenPoint(uiPanel.anchorMin * localScale);
        Vector2 topCorner = Camera.main.WorldToScreenPoint(uiPanel.anchorMax * localScale);

        pos.x = Mathf.Clamp(pos.x, bottomCorner.x * (1/scale.x), topCorner.x * (1/scale.x));
        pos.y = Mathf.Clamp(pos.y, bottomCorner.y * (1/scale.y), topCorner.y * (1/scale.y));

        Mouse.current.WarpCursorPosition(pos);
    }

    public void SetShrimpCam(ShrimpCam cam)
    {
        _cam = cam;
    }

    public void UnsetShrimpCam()
    {
        _cam = null;
        control = 0;
        zoom = 0;
    }
}
