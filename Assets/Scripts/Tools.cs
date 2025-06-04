using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    public static void SetLayerRecursively(this Transform parent, int layer)
    {
        parent.gameObject.layer = layer;

        for (int i = 0, count = parent.childCount; i < count; i++)
        {
            parent.GetChild(i).SetLayerRecursively(layer);
        }
    }

    public static List<GameObject> FindDescendants(this GameObject obj)
    {
        List<GameObject> list = new List<GameObject>();

        foreach(Transform child in obj.transform)
        {
            list.Add(child.gameObject);
            list.AddRange(child.gameObject.FindDescendants());
        }

        return list;
    }
}
