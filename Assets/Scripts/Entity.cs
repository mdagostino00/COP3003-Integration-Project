using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // fields

    protected float moveSpeed;    // multiplier that will be multiplied to the entity's movement. Make them slower or faster
    protected float dirX;         // entity's movement direction in the X direction
    protected float dirY;         // entity's movement direction in the Y direction
    private float moveX;        // entity's actual movement value in the X direction
    private float moveY;        // entity's actual movement value in the Y direction

    private BoxCollider2D boxCollider;  // for collision detection
    private Vector3 moveDelta;  // on next frame, move this amount
    private RaycastHit2D hit;

    // methods

    //public float MoveSpeed
    //{
    //    get { return moveSpeed; }
    //    set { moveSpeed = value; }
    //}

    //public float DirX
    //{
    //    get { return dirX; }
    //    set { dirX = value; }
    //}

    //public float DirY
    //{
    //    get { return dirY; }
    //    set { dirY = value; }
    //}

    protected virtual float GetMovX() {return moveSpeed * dirX;}

    protected virtual float GetMovY() {return moveSpeed * dirY;}

    // on object initialization
    protected void EntityStart()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        // sets value of movement
        moveX = GetMovX();
        moveY = GetMovY();

        // Reset moveDelta
        moveDelta = new Vector3(moveX, moveY, 0);

        // Swap sprite direction, based on going right or left
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Make this thing move
        transform.Translate(moveDelta * Time.deltaTime);
    }
}
