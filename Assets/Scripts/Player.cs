using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;  // for collision detection

    private Vector3 moveDelta;  // on next frame, move this amount

    // on object initialization
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // runs every frame
    private void FixedUpdate()
    {
        // gets input from player (wasd or arrows by default)
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

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

        // make player move
        transform.Translate(moveDelta * Time.deltaTime);
    }
}
