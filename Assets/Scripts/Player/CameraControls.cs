using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    public Transform camera;
    public float lookSenstivity;


    private Vector2 _look;
    private float _rotY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _rotY += _look.y * lookSenstivity;
        _rotY = Mathf.Clamp(_rotY, -45, 35);
        camera.localRotation = Quaternion.Euler(-_rotY, 0, 0);

        transform.Rotate(0, _look.x * lookSenstivity, 0);
    }

    public void OnLook(InputValue Mouse)
    {
        _look = Mouse.Get<Vector2>();
    }
}
