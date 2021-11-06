using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//elijah made this by just conglomerating functions from michael's player classes into a useful base class


public class Entity : MonoBehaviour
{
    // fields
    protected Vector2 direction;
    protected Vector2 movement;
    protected Rigidbody2D body;
    protected Animator anim;

    // base health
    [SerializeField]
    protected static int HEALTH_BASE = 100;
    [SerializeField]
    private int healthTotal = HEALTH_BASE;
    [SerializeField]
    private int health;

    [SerializeField]
    private int level = 1;

    // passive modifiers
    [SerializeField]
    private float walkSpeedMultiplier = 1.0f; // how fast entity should walk
    //[SerializeField]
    // private float runSpeedMultiplier = 1.2f; // runSpeed for when it's implemented
    [SerializeField]
    private float attackMod = 1.0f; // mod for physical attacks
    //[SerializeField]
    // private float magicMod = 1.0f; // mod for magic-based attacks
    [SerializeField]
    private float defenseMod = 1.0f; // physical defense modifier
    //[SerializeField]
    // private float defenseModMagic = 1.0f; // possible alt defmod for magic-property attacks
    //[SerializeField]
    //private float attackSpeed; // how long until player can attack again

    public int Health { get => health; set => health = value; }
    public int HealthTotal { get => healthTotal; set => healthTotal = value; }
    public float SpeedMultiplier { get => walkSpeedMultiplier; set => walkSpeedMultiplier = value; }
    public int Level { get => level; set => level = value; }
    public float DefenseMod { get => defenseMod; set => defenseMod = value; }
    public float AttackMod { get => attackMod; set => attackMod = value; }

    // Awake is called when Unity creates the object
    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // FixedUpdate is called at a fixed interval, not always once per frame.
    protected virtual void FixedUpdate()
    {
        RotateTowardDirection();
        Movement();
        Debug.Log("This won't print if override is successful.");
    }

    // gets the magnitude of the velocity vector
    float GetMagnitude()
    {
        float velocityX = body.velocity.x * body.velocity.x;
        float velocityY = body.velocity.y * body.velocity.y;
        float magnitude = Mathf.Sqrt(velocityX + velocityY);
        return magnitude;
    }

    protected void Movement()
    {
        // get current position
        Vector2 currentPos = body.position;
        // calculate move delta
        Vector2 adjustedMovement = movement * walkSpeedMultiplier;
        // add move delta to current position
        Vector2 newPos = currentPos + adjustedMovement * Time.fixedDeltaTime;
        // move player to new position
        body.MovePosition(newPos);
    }

    protected void Movement(Vector2 direction)
    {
        body.MovePosition((Vector2)transform.position + (direction * walkSpeedMultiplier * Time.deltaTime));
    }

    protected virtual void RotateTowardDirection() // virtual because we might need to handle this function depending on enemy
    {
        //turn off walking
        if (movement != Vector2.zero) // if we have any movement
        {
            // rotate sprite to face direction of movement using quaterions
            transform.rotation =
                Quaternion.LookRotation(Vector3.forward, movement);
        }
    }

    protected void RotateTowardDirection(ref Vector2 direction)
    {
        //rotate the sprite without quaterions (rotation is off by 90)
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //body.rotation = angle;

        //rotate the sprite with quaterions (very cool!)
        transform.rotation =
                Quaternion.LookRotation(Vector3.forward, movement);
    }

    public int DamageCalculate(int baseDamage)
    {
        int modifiedDamage = (int)(baseDamage * attackMod);
        return modifiedDamage;
    }

    public int HealthReduce(int damageValue)
    {
        //damageValue = (int)(damageValue * DefenseMod); // multiply damage value by player defense
        this.health -= damageValue; // subtract modified damage from health
        return damageValue; // return if print damage needed
    }

    public int HealthHeal(int healValue)
    {
        if (healValue < 1)
        {
            healValue = 1;
            Debug.Log("This healing item healed for less than 1 HP\n");
        }
        int healMax = healthTotal - health;
        if (healValue > healMax)
        {
            healValue = healMax;
        }
        this.health += healValue;
        return healValue; // return value if heal number needed
    }

    public int HealthHeal(int healValue, char healType)
    {
        switch (healType)
        {
            // choose small, medium, large, or x-large potion types.
            // default to 1 if no type or less than 1
            case 's':
                healValue = (int)(healValue * 0.7);
                break;
            case 'm':
                //healValue = (int)(healValue * 1.0);
                break;
            case 'l':
                healValue = (int)(healValue * 1.4);
                break;
            case 'x':
                healValue = (int)(healValue * 2.0);
                break;
            default:
                healValue = 1;
                Debug.Log("This healing item doesn't have a healType\n");
                break;
        }
        if (healValue < 1)
        {  // minimum healing value = 1
            healValue = 1;
            Debug.Log("This healing item healed for less than 1 HP\n");
        }

        int healMax = healthTotal - health;  //check to see if healValue is more than max health
        if (healValue > healMax)
        {
            healValue = healMax;  // set healValue to most health that can be healed
        }

        this.health += healValue; // heal the player
        return healValue; // return value if heal number needed
    }
}