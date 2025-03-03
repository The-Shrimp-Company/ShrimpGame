using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoStuff : MonoBehaviour
{
    public GameObject prefab;
    private GameObject obj;

    public void Click()
    {
        if (obj == null)
        {
            obj = Instantiate(prefab, transform.parent.transform);
        }
        else
        {
            Destroy(obj);
        }
    }
}
