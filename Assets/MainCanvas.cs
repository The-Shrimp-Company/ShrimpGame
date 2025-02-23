using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.SetCanvas(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
