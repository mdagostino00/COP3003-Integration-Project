// Elijah Nieves

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSpikeTrap : Enemy
{
    protected Vector2 right;
    protected Vector2 left;

    void OnCollisionEnter2D(Collision2D col)  // if they hit a wall
    {
        direction *= -1;        // reverse the direction

    }

    protected override void Awake()
    {
        base.Awake();

        // initialize vectors
        right = new Vector2(1f, 0f);
        left = new Vector2(-1f, 0f);

        this.direction = right;         // set it to move right

        enemyFSM.SetCurrentState(EnemyFSMStateType.MOVEMENT);           //set it to move
    }

    // Use Update() for non-physics (timer)
    protected override void Update()
    {
        enemyFSM.Update();              // dont allow it to change directions
    }
}
