using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlayerMovement : MonoBehaviour
{
    CharacterController CC;


    [Header("Movement Modifier")]
    public float Speed;


    private Vector2 move = new Vector2(0, 0);






    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>();
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
}
