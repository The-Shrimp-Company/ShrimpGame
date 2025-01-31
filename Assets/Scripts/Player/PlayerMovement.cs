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
    private Vector2 mouse = new Vector2(0, 0);
    private Vector2 oldLook = new Vector2(0, 0);


    [Header("Look Modifier")]
    public int LookMultiplier;




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

        Debug.Log(oldLook + "turning to" + mouse);
        //rotY = Mathf.Clamp(mouse.y * LookMultiplier.y, -LookCap, LookCap);
        rotY = (mouse.y + oldLook.y) / 2;
        //rotX = Mathf.Clamp(mouse.x * LookMultiplier.x, -LookCap, LookCap);
        rotX = (mouse.x + oldLook.x) / 2;
        oldLook = mouse;

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
        mouse = Mouse.Get<Vector2>();
        mouse *= LookMultiplier;
    }

    public void OnMove(InputValue Move)
    {
        move = Move.Get<Vector2>();
        move *= Speed;
    }
}
