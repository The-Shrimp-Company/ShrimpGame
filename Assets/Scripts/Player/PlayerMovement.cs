using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    CharacterController CC;
    CameraControls Cam;

    float rotX = 0;
    float rotY = 0;


    [Header("Movement Modifier")]
    public float Speed;


    private Vector2 move = new Vector2(0, 0);


    [Header("Look Modifier")]
    public Vector2 LookMultiplier;
    public float LookCap;




    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
        Cam = GetComponentInChildren<CameraControls>();


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Look Controls
        transform.Rotate(0, rotX * Time.deltaTime, 0);
        Cam.LookVertical(rotY * Time.deltaTime);
    }


    private void FixedUpdate()
    {
        CC.SimpleMove(transform.TransformVector(move.x, 0, move.y));
    }


    public void OnLook(InputValue Mouse)
    {
        Vector2 mouse = Mouse.Get<Vector2>();
        rotY = Mathf.Clamp(mouse.y * LookMultiplier.y, -LookCap, LookCap);
        rotX = Mathf.Clamp(mouse.x * LookMultiplier.x, -LookCap, LookCap);
    }

    public void OnMove(InputValue Move)
    {
        move = Move.Get<Vector2>();
        move *= Speed;
    }
}
