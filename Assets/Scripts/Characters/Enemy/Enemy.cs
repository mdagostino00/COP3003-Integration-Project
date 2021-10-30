using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : TestBase
{
    [SerializeField]
    private GameObject targetOBJ;       //object that the enemy should follo. usually player.

    protected void Awake()
    {
        baseAwake();
    }

    protected void FixedUpdate()
    {
        targetOBJ = GameObject.FindGameObjectWithTag("Player");     //find player position
        movement = targetOBJ.transform.position - transform.position;       //change the movement vector to point toward the player's position

        baseFixedUpdate();
    }

}