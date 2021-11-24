// Elijah Nieves
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    // Enemy "is an" Entity. Enemy has a subtyping relationship with Entity.
    // Rather than inheriting just to reuse the implementation and functions of its superclasses, Enemy exists as a 'type' of Entity.
    // All subtypes of Entity exist with the assumption that they can safely use any Entity method, and will almost always be required to do so.
    // Every function that can be invoked on Entity can also be invoked on Enemy
    [SerializeField]
    public Transform player; // we need the player's position
    protected float timer = 0.0f;
    protected bool isMoving = true;

    protected float moveDelay = 0.0f;       //how many seconds the enemy will wait between moving toward the player.
    protected float moveLength = 1.0f;       //how many seconds the enemy will attack chase the player before the delay

    // Update() is called every frame
    protected virtual void Update()
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

        if (isMoving)               // if the enemy should be moving
            Movement(direction);    // move them towards their target
    }

    public void moveCharacter(ref Vector2 direction)
    {
        direction.Normalize(); // really cool vector normalization function in Unity
        movement = direction; // Vector2D movement is now the normalized vector
    }
}