using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    private Vector2 movement;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateTowardDirection();
        Movement();

    }

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void Movement()
    {
        Vector2 currentPos = body.position;
        Vector2 adjustedMovement = movement * movementSpeed;
        Vector2 newPos = currentPos + adjustedMovement * Time.fixedDeltaTime;
        body.MovePosition(newPos);
    }

    private void RotateTowardDirection()
    {
        if(movement != Vector2.zero)
        {
            transform.rotation = 
                Quaternion.LookRotation(Vector3.back, movement);
        }
    }
}
