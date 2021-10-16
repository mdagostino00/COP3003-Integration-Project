using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWEntity : Entity
{
    //methods
    protected override float GetMovX() { return moveSpeed * Input.GetAxisRaw("Horizontal"); }

    protected override float GetMovY() { return moveSpeed * Input.GetAxisRaw("Vertical"); }

    // Start is called before the first frame update
    void Start()
    {
        EntityStart();
        moveSpeed = 1F;                                 // multiplier that will be multiplied to the entity's movement. Make them slower or faster
    }

    void FixedUpdate()
    {
        EntityUpdate();
    }
}
