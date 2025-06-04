using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool hovering = false;
    protected bool wasHovering = false;

    

    // Update is called once per frame
    protected virtual void MouseHover()
    {
        if (hovering && !wasHovering)
        {
            OnHover();
        }
        if (!hovering && wasHovering)
        {
            OnStopHover();
        }
        wasHovering = hovering;
        hovering = false;
    }

    public virtual void Show()
    {
        hovering = true;
    }

    public virtual void Action()
    {

    }

    public virtual void OnHover()
    {

    }

    protected virtual void OnStopHover()
    {

    }
}
