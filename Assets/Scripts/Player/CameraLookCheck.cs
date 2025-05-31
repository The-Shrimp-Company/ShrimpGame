using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraLookCheck : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI toolTip;

    public GameObject LookCheck(float Distance, string layer)
    {
        RaycastHit hit;

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        LayerMask layerMask = LayerMask.GetMask(layer);


        if(Physics.Raycast(transform.position, fwd, out hit, Distance, layerMask))
        {
            return hit.collider.gameObject;
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
                    if (hit.collider.GetComponent<ToolTip>())
                    {
                        toolTip.text = hit.collider.GetComponent<ToolTip>().toolTip;
                        return;
                    }
                }
            }

            toolTip.text = "";
        }
    }
}
