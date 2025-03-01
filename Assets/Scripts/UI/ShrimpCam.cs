using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpCam : MonoBehaviour
{
    private bool currentActive = false;
    private LayerMask layerMask;
    private float CamZoom = 1;

    public void Awake()
    {
        layerMask = LayerMask.GetMask("Decoration");
    }

    public ShrimpCam SetCam()
    {
        currentActive = true;
        return this;
    }

    public void Deactivate()
    {
        currentActive = false;
    }

    public void Update()
    {
        if (currentActive)
        {
            Vector3 x = transform.parent.position;
            Vector3 y = transform.position;
            
            RaycastHit hit;
            if (Physics.SphereCast(x, 0.01f, (y - x).normalized, out hit, (y - x).magnitude * CamZoom, layerMask, QueryTriggerInteraction.Collide))
            {
                Camera.main.transform.position = x + (y - x) * (hit.distance / (y - x).magnitude) * CamZoom;
                Camera.main.transform.LookAt(x);
            }
            else
            {
                Camera.main.transform.position = x + (y - x) * CamZoom;
                Camera.main.transform.LookAt(x);
            }
        }
    }

    public void ChangeZoom(bool input)
    {
        if (input)
        {
            if(CamZoom > 0.5)
            {
                CamZoom -= 0.01f;
            }
        }
        else
        {
            if(CamZoom < 1)
            {
                CamZoom += 0.01f;
            }
        }
    }
}
