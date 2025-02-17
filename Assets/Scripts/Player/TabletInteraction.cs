using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TabletInteraction : MonoBehaviour
{
    protected RectTransform[] buttons;

    protected string _clickedButton = null;
    protected bool _clickedButtonUsed = false;

    [SerializeField]
    protected ShelfSpawn shelves;

    private void Start()
    {
        buttons = GetComponentsInChildren<RectTransform>().Where(x => x.CompareTag("Button")).ToArray();
    }


    public virtual void MouseClick(Vector3 point) 
    {
        _clickedButton = null;
        RectTransform clicked = null;

        List<RaycastResult> results = new List<RaycastResult>();
        PointerEventData ped = new PointerEventData(null);
        ped.position = point;

        UIManager.instance.GetFocus().GetComponent<GraphicRaycaster>().Raycast(ped, results);


        foreach (RaycastResult r in results)
        {
            if (r.gameObject.CompareTag("Button"))
            {
                clicked = r.gameObject.GetComponent<RectTransform>();
                break;
            }
        }

        if(clicked != null)
        {
            _clickedButton = clicked.name;
        }
    }

    protected virtual void PressedButton()
    {
        _clickedButtonUsed = true;
        switch (_clickedButton)
        {
            case "Buy Shrimp":
                shelves.SpawnShrimp();
                break;
            case "Buy Tanks":
                shelves.SpawnNextTank();
                break;
            case "Add Money":
                Money.instance.AddMoney(20);
                break;
            default:
                _clickedButtonUsed = false;
                break;
        }
        if (_clickedButtonUsed)
        {
            _clickedButton = null;
        }
    }

    public virtual void Update()
    {
        if (_clickedButton != null) PressedButton();
    }

    public virtual void Close() { }
}
