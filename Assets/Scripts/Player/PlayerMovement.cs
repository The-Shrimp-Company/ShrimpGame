using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlayerMovement : MonoBehaviour
{
    CharacterController CC;

    Camera cam;


    [Header("Movement Modifier")]
    public float Speed;


    private Vector2 move = new Vector2(0, 0);






    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        CC.SimpleMove(transform.TransformVector(move.x, 0, move.y));
        //CC.Move(transform.TransformVector(move.x * Time.deltaTime, 0, move.y * Time.deltaTime));
    }


    public void OnMove(InputValue Move)
    {
        move = Move.Get<Vector2>();
        move *= Speed;
    }

    public void OnSlowMove(InputValue Move)
    {
        move = Move.Get<Vector2>() / 2;
        move *= Speed;
    }


    public void OnCrouch(InputValue Crouch)
    {
        if (Crouch.isPressed)
        {
            cam.transform.localPosition = new Vector3(0, 0, 0);
        }else if (!Crouch.isPressed)
        {
            cam.transform.localPosition = Vector3.up/2;
        }
    }
}
