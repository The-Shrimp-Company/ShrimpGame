using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    public Transform cameraTransform;
    public float lookSenstivity;

    private PlayerInput _playerInput;

    private Vector2 _look;
    private float _rotY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerInput = GetComponent<PlayerInput>();
        if (!PlayerPrefs.HasKey("sensitivity"))
        {
            lookSenstivity = 0.5f * 1f;
        }
        else
        {
            lookSenstivity = 0.5f * PlayerPrefs.GetFloat("sensitivity");
        }
    }

    private void Update()
    {
        if(_playerInput.currentActionMap.name != "Move")
        {
            return;
        }
        _rotY += _look.y * lookSenstivity;
        _rotY = Mathf.Clamp(_rotY, -75, 45);
        cameraTransform.localRotation = Quaternion.Euler(-_rotY, 0, 0);

        transform.Rotate(0, _look.x * lookSenstivity, 0);
    }

    public void OnLook(InputValue Mouse)
    {
        _look = Mouse.Get<Vector2>();
    }
}
