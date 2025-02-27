using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class FakeCursor : MonoBehaviour
{
    private RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        UIManager.instance.SetCursor(gameObject);
    }

    private void Update()
    {
        rect.position = Mouse.current.position.value / rect.localScale;
    }
}
