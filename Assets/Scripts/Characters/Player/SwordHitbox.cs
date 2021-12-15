using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public Rigidbody2D hitbox;
    public BoxCollider2D col;
    public Collision2D enemyCol;
    public ContactFilter2D conFilter;
    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        Debug.Log("Hitbox Start!");
        col.enabled = false;

        Debug.Log(conFilter.isFiltering);
    }

    void Awake()
    {
        Debug.Log("Hitbox Awake!");
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (col.IsTouching(col, conFilter))
        {
            var entity = GetComponent<Enemy>();
            int damage = entity.HealthReduce(20);
            Debug.Log("Enemy hit!");
        }
        Debug.Log("Sword Attack Swing!");
    }
}
