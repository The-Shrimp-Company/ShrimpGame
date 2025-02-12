using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TabletInteraction : MonoBehaviour
{
    private RectTransform[] buttons;

    public ShelfSpawn shelves;

    private void Start()
    {
        buttons = GetComponentsInChildren<RectTransform>().Where(x => x.name != "Cursor" && x.name != "TabletBackground").ToArray();
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
            switch (clicked.name)
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
        }
    }
}
