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
    protected void EntityUpdate()
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

        // Make sure we can move in this direction by castiong a box there first. If the box returns null, we're free to move.
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Make this thing move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            // Make this thing move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
