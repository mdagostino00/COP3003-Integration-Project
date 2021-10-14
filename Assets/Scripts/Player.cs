using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // fields

    private float x;  // movement values
    private float y;

    private BoxCollider2D boxCollider;  // for collision detection
    private Vector3 moveDelta;  // on next frame, move this amount
    private RaycastHit2D hit;

    // methods

    // on object initialization
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // runs every frame
    private void FixedUpdate()
    {
        // gets input from player (wasd or arrows by default)
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        // Reset moveDelta
        moveDelta = new Vector3(x, y, 0);

        // Swap sprite direction, based on going right or left
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }else if(moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // check to see if we can move in this direction by casting a box first.
        // if box returns null, we can move
        hit = Physics2D.BoxCast(
            transform.position, 
            boxCollider.size, 
            0, 
            new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking")
            );
        if (hit.collider == null)
        {
            // make player move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }


        hit = Physics2D.BoxCast(
            transform.position,
            boxCollider.size,
            0,
            new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking")
            );
        if (hit.collider == null)
        {
            // make player move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
