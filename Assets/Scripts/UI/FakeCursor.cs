using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.instance.SetCursor(gameObject);
    }

    
}
