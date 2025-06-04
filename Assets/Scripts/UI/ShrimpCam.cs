using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpCam : MonoBehaviour
{
    private bool currentActive = false;
    private LayerMask layerMask;
    private float CamZoom = 1;
    public float camSwitchSpeed = 0.2f;
    public Ease camSwitchEase;

    public void Awake()
    {
        layerMask = LayerMask.GetMask("Decoration");
    }

    public ShrimpCam SetCam()
    {
        CamZoom = 1;

        DOTween.Kill(Camera.main);
        Camera.main.transform.DOMove(GetTargetCameraPosition(), camSwitchSpeed).SetEase(camSwitchEase).OnComplete(SetActive);
        Camera.main.transform.DORotate(GetTargetCameraRotation(), camSwitchSpeed).SetEase(camSwitchEase);

        return this;
    }

    private void SetActive()
    {
        currentActive = true;
    }

    public void Deactivate()
    {
        currentActive = false;
    }

    public void Update()
    {
        if (currentActive)
        {
            Camera.main.transform.position = GetTargetCameraPosition();

            Camera.main.transform.LookAt(transform.parent.position);
        }
    }

    private Vector3 GetTargetCameraPosition()
    {
        Vector3 x = transform.parent.position;
        Vector3 y = transform.position;
        Vector3 target;
            
        RaycastHit hit;
        if (Physics.SphereCast(x, 0.01f, (y - x).normalized, out hit, (y - x).magnitude * CamZoom, layerMask, QueryTriggerInteraction.Collide))
        {
            target = x + (y - x) * (hit.distance / (y - x).magnitude) * CamZoom;
        }
        else
        {
            target = x + (y - x) * CamZoom;
        }

        return target;
    }

    private Vector3 GetTargetCameraRotation()
    {
        return (transform.position - transform.parent.position).normalized;
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
