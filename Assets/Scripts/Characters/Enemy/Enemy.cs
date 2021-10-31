using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField]
    public Transform player; // we need the player's position

    // Update() is called every frame
    void Update()
    {
        direction = player.position - transform.position; // find direction vector from enemy to player
        //Debug.Log(direction);
        RotateTowardDirection(); // rotate enemy sprite to face player
        moveCharacter(ref direction); // normalize the direction vector and set this to the movement vector
    }

    // Use FixedUpdate() for physics
    protected override void FixedUpdate()
    {
        //targetOBJ = GameObject.FindGameObjectWithTag("Player");     //find player position
        //movement = targetOBJ.transform.position - transform.position;       //change the movement vector to point toward the player's position

        Movement(direction);
    }

    public void moveCharacter(ref Vector2 direction)
    {
        direction.Normalize(); // really cool vector normalizion function in Unity
        movement = direction; // Vector2D movement is now the normalized vector
    }
}