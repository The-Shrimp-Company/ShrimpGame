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
    private float _rotX;

    private float myDelta;

    private void Start()
    {
        myDelta = Time.time;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerInput = GetComponent<PlayerInput>();
        if (!PlayerPrefs.HasKey("Sensitivity"))
        {
            lookSenstivity = 10f;
            PlayerPrefs.SetFloat("Sensitivity", 10f);
        }
        else
        {
            lookSenstivity = PlayerPrefs.GetFloat("Sensitivity");
        }
    }

    private void Update()
    {

        // If the player is in a menu that stops their movement
        if (UIManager.instance.GetScreen())
        {
            _look = Vector2.zero;
        }

        if (_playerInput.currentActionMap.name != "Move")
        {
            return;
        }
        _look *= Time.time - myDelta;

        _rotY += _look.y * lookSenstivity;
        _rotY = Mathf.Clamp(_rotY, -75, 45);
        _rotX += _look.x * lookSenstivity;
        cameraTransform.localRotation = Quaternion.Euler(-_rotY, 0, 0);

        transform.Rotate(0, _look.x * lookSenstivity, 0);
        transform.rotation = Quaternion.Euler(0, _rotX, 0);

        

        cameraTransform.position = new Vector3(cameraTransform.position.x, 2.3f, cameraTransform.position.z);
        _look = Vector2.zero;

        myDelta = Time.time;
    }

    

    public void OnLook(InputValue Mouse)
    {
        _look += Mouse.Get<Vector2>();
    }
}
