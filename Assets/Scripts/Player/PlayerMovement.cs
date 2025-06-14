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


    private void Update()
    {
        // If the player is in a menu that stops their movement
        if (UIManager.instance.GetScreen() /* && UIManager.instance.GetScreen().locksPlayerMovement */)  
        {
            move = Vector2.zero;
        }


        CC.SimpleMove(transform.TransformVector(move.x, 0, move.y));

        if (move != Vector2.zero) PlayerStats.stats.timeSpentMoving += Time.deltaTime;
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
}
