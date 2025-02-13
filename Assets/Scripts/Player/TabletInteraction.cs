using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TabletInteraction : MonoBehaviour
{
    protected RectTransform[] buttons;

    protected string _clickedButton = null;

    [SerializeField]
    private ShelfSpawn shelves;

    private void Start()
    {
        buttons = GetComponentsInChildren<RectTransform>().Where(x => x.CompareTag("Button")).ToArray();
    }

    public void MouseClick(Vector3 point)
    {
        Vector3[] corners = new Vector3[4];
        RectTransform clicked = null;
        foreach (RectTransform rect in buttons)
        {
            rect.GetLocalCorners(corners);
            if(point.x > corners[0].x + rect.localPosition.x && point.x < corners[2].x + rect.localPosition.x)
            {
                if(point.y > corners[0].y + rect.localPosition.y && point.y < corners[2].y + rect.localPosition.y)
                {
                    clicked = rect;
                    break;
                }
            }
        }

        if(clicked != null)
        {
            _clickedButton = clicked.name;
        }
    }

    protected virtual void PressedButton()
    {
        switch (_clickedButton)
        {
            case "Buy Shrimp":
                shelves.SpawnShrimp();
                break;
            case "Buy Tanks":
                shelves.SpawnNextTank();
                break;
            default:
                Debug.Log("No");
                break;
        }
        _clickedButton = null;
    }

    public void Update()
    {
        if (_clickedButton != null) PressedButton();
    }

    public virtual void Close() { }
}
