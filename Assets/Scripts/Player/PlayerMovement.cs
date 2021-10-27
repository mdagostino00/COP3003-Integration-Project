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
    private Animator anim;

    // Awake is called when Unity creates the object
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // FixedUpdate is called at a fixed interval, not always once per frame.
    void FixedUpdate()
    {
        RotateTowardDirection();
        Movement();
    }

    float GetMagnitude()
    {
        float velocityX = body.velocity.x * body.velocity.x;
        float velocityY = body.velocity.y * body.velocity.y;
        float magnitude = Mathf.Sqrt(velocityX + velocityY);
        return magnitude;
    }

    void OnMove(InputValue value)
    {
        // get the player's input as a float vector
        movement = value.Get<Vector2>();
    }

    private void Movement()
    {
        // get current position
        Vector2 currentPos = body.position;
        // calculate move delta
        Vector2 adjustedMovement = movement * movementSpeed;
        // add move delta to current position
        Vector2 newPos = currentPos + adjustedMovement * Time.fixedDeltaTime;
        // move player to new position
        body.MovePosition(newPos);
    }

    private void RotateTowardDirection()
    {
        //turn off walking
        if (movement != Vector2.zero) // if we have player movement input
        {
            // rotate sprite to face direction of movement
            transform.rotation = 
                Quaternion.LookRotation(Vector3.forward, movement);
            // turn on walking animation
            anim.SetBool("walking", true);
        }
        else
        {
            //turn off walking
            anim.SetBool("walking", false);
        }
    }

}
