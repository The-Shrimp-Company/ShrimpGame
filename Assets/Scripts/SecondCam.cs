using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.SetCamera(GetComponent<Camera>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
