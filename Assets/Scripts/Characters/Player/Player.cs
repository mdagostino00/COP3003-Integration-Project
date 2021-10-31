using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [SerializeField]
    protected static int MAGICPOINTS_BASE = 20;
    [SerializeField]
    private int magicPointsTotal = MAGICPOINTS_BASE;
    [SerializeField]
    private int magicPoints = MAGICPOINTS_BASE;

    //[SerializeField]
    //private int inventorySlots;  // later lol

    private int experiencePoints = 0;

    public int MagicPoints { get => magicPoints; set => magicPoints = value; }
    public int MagicPointsTotal { get => magicPointsTotal; set => magicPointsTotal = value; }
    public int ExperiencePoints { get => experiencePoints; set => experiencePoints = value; }

    protected override void FixedUpdate()
    {
        RotateTowardDirection();
        Movement();
    }

    void OnMove(InputValue value)
    {
        // get the player's input as a float vector
        movement = value.Get<Vector2>();
    }

    protected override void RotateTowardDirection()
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
