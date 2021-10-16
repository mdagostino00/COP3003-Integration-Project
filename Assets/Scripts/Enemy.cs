using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    void Start()
    {
        EntityStart();
        moveSpeed = 0.25F;    // multiplier that will be multiplied to the entity's movement. Make them slower or faster
        dirX = -1;         // entity's movement direction in the X direction
        dirY = 1;         // entity's movement direction in the Y direction
    }

    void FixedUpdate()
    {
        EntityUpdate();
    }
}
