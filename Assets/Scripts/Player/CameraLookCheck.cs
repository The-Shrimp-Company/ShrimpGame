using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookCheck : MonoBehaviour
{
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
}
