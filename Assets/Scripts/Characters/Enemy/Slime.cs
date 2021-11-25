// Elijah Nieves
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    // Slime "is an" Enemy. Slime has a subtyping relationship with Enemy.
    // Rather than inheriting just to reuse the implementation and functions of its superclasses, Slime exists as a 'type' of Enemy.
    // All subtypes of Enemy exist with the assumption that they can safely use any Enemy method, and will almost always be required to do so.
    // Every function that can be invoked on Enemy can also be invoked on Slime
    protected override void Awake()
    {
        base.Awake();

        // set movement values
        moveDelay = 0.75f;
        moveLength = 0.5f;

        // make it so it does not immediately move
        isMoving = false;
    }

    // Use Update() for non-physics (timer)
    protected override void Update()
    {
        timer += Time.deltaTime;        // each frame, add how much time has passed.

        if (isMoving == false)          // if the enemy is not moving
        {
            base.Update();              // only let the slime change directions while it is getting ready to jump

            if (timer >= moveDelay)     // and they have been delayed long enough
            {
                timer = 0.0f;           // reset the timer
                isMoving = true;        // set them to start moving
            }
        }

        if (isMoving == true)           // if the enemy is moving
            if (timer >= moveLength)    // and if they have been moving for long enough
            {
                timer = 0.0f;           // reset the timer
                isMoving = false;       // set them to stop moving
            }
    }
}
