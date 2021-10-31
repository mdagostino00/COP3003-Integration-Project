using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLegacy : MonoBehaviour
{
    // fields

    private float x;  // movement values
    private float y;

    private float runSpeed = 1.0f; //speed multiplier
    private float moveLimit = 0.7f; //to limit diagonal movement speed

    private Vector3 moveDelta;  // on next frame, move this amount
    private Rigidbody2D body;
    //private BoxCollider2D boxCollider;  // for collision detection
    //private RaycastHit2D hit;

    // methods

    // on object initialization
    private void Start()
    {
        //boxCollider = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
    }

    //runs every frame
    private void Update()
    {
        // gets input from player (wasd or arrows by default)
        x = Input.GetAxisRaw("Horizontal"); //1 right, -1 left
        y = Input.GetAxisRaw("Vertical"); //1 up, -1 down
    }

    // doesn't run every frame, use for physics
    private void FixedUpdate()
    {

        // Reset moveDelta
        //moveDelta = new Vector3(x, y, 0);

        // Swap sprite direction, based on going right or left
        if (x > 0)
        {
            transform.localScale = Vector3.one;
        }else if(x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (x != 0 && y != 0) //check diagonal movement
        {
            x *= moveLimit;
            y *= moveLimit;
        }

        // check to see if we can move in this direction by casting a box first.
        // if box returns null, we can move
        /*hit = Physics2D.BoxCast(
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
            //Player.velocity(0, moveDelta.y * Time.deltaTime * runSpeed, 0);
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }*/


        /*hit = Physics2D.BoxCast(
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
            //Player.velocity(moveDelta.x * Time.deltaTime * runSpeed, 0, 0);
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }*/

        body.velocity = new Vector3(x * runSpeed * Time.deltaTime, 
            y * runSpeed * Time.deltaTime, 
            0
            );
    }
}
