using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraLookCheck : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI toolTip;
    private CrossHairSwitch crosshair;

    private void Start()
    {
        crosshair = toolTip.transform.parent.GetComponent<CrossHairSwitch>();
        if (crosshair == null) Debug.LogWarning("CameraLookCheck cannot find crosshair");
    }

    public GameObject LookCheck(float Distance, string layer = "")
    {
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if(layer != "")
        {
            LayerMask layerMask = LayerMask.GetMask(layer);


            if (Physics.Raycast(transform.position, fwd, out hit, Distance, layerMask))
            {
                return hit.collider.gameObject;
            }
        }
        else
        {
            if(Physics.Raycast(transform.position, fwd, out hit, Distance))
            {
                return hit.collider.gameObject;
            }
        }
        


        return null;
    }

    public void Update()
    {
        if (toolTip.enabled)
        {
            RaycastHit hit;

            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if(Physics.Raycast(transform.position, fwd, out hit, 3f))
            {
                if (hit.collider)
                {
                    if (hit.collider.GetComponent<Interactable>())
                    {
                        hit.collider.GetComponent<Interactable>().Show();
                    }
                    if (hit.collider.GetComponent<ToolTip>())
                    {
                        toolTip.text = hit.collider.GetComponent<ToolTip>().toolTip;
                        crosshair.hovering = true;
                        return;
                    }
                }
            }

            //toolTip.text = "";
            crosshair.hovering = false;
        }
    }
}
