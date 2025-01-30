using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public void LookVertical(float rot)
    {

        if(transform.rotation.eulerAngles.x > 70 && transform.rotation.eulerAngles.x < 90 && rot < 0)
        {
            Debug.Log("Attempt 1");
            return;
        }
        if (transform.rotation.eulerAngles.x < 290 && transform.rotation.eulerAngles.x > 260 && rot > 0)
        {
            Debug.Log("Attempt 2");
            return;
        }

        transform.Rotate(-rot, 0, 0);
    }
}
