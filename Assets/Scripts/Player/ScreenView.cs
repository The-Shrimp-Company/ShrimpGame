using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenView : MonoBehaviour
{


    [SerializeField]
    protected ShelfSpawn shelves;

    protected RectTransform[] buttons;

    protected string _clickedButton = null;
    protected bool _clickedButtonUsed = false;

    protected virtual void Start()
    {
        //buttons = GetComponentsInChildren<RectTransform>().Where(x => x.CompareTag("Button")).ToArray();
    }

    /*
    public virtual void MouseClick(Vector3 point, bool pressed)
    {
        _clickedButton = null;
        RectTransform clicked = null;

        List<RaycastResult> results = new List<RaycastResult>();
        PointerEventData ped = new PointerEventData(null);
        ped.position = point;

        UIManager.instance.GetFocus().GetComponent<GraphicRaycaster>().Raycast(ped, results);


        foreach (RaycastResult r in results)
        {
            if (r.gameObject.CompareTag("Button") && pressed)
            {
                clicked = r.gameObject.GetComponent<RectTransform>();
                break;
            }
            if (r.gameObject.CompareTag("UI"))
            {
                break;
            }
        }

        if (clicked != null)
        {
            _clickedButton = clicked.name;
        }
    }

    protected virtual void PressedButton()
    {
        Debug.Log("Base Pressed Button Being Called, this probably isn't intended behaviour");
    }

    public virtual void Update()
    {
        if (_clickedButton != null) PressedButton();
    }
    */

    public virtual void Close()
    {
        Destroy(gameObject);
    }
}
