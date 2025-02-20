using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class RectTools
{
    /// <summary>
    /// Will effectively set the given rect transform to be identical to the new rect transform
    /// </summary>
    /// <param name="obj">Transform to change</param>
    /// <param name="newTransform">Transform to change to</param>
    static public void ChangeRectTransform(RectTransform obj, RectTransform newTransform)
    {
        obj.anchorMin = newTransform.anchorMin;
        obj.anchorMax = newTransform.anchorMax;
        obj.pivot = newTransform.pivot;
        obj.sizeDelta = newTransform.sizeDelta;
        obj.position = newTransform.position;
        //obj.ForceUpdateRectTransforms();
    }
}
