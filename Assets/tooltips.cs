using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tooltips : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.tooltips = this.gameObject;
    }

}
