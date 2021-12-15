// Michael D'Agostino
// Elijah made this by just conglomerating functions from Michael's old player classes into a useful base class
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Entity : MonoBehaviour
{
    // Entity utilizes terms from MonoBehaviour, a critical class from the Unity library. Because Entity has not overridden anything from MonoBehaviour, Entity is a subtype of MonoBehaviour
    // Almost all base classes made for a Unity project will inherit MonoBehaviour, as MonoBehaviour is the class that enables object to use the Unity engine. 

    // fields
    protected Vector2 direction;
    protected Vector2 movement;
    public Rigidbody2D body;
    public Animator anim;

    // base health
    [SerializeField]
    protected static int HEALTH_BASE = 100;
    [SerializeField]
    private int healthTotal = HEALTH_BASE;
    [SerializeField]
    private int currentHealth;

    [SerializeField]
    protected static int MAGICPOINTS_BASE = 20;
    [SerializeField]
    private int magicPointsTotal = MAGICPOINTS_BASE;
    [SerializeField]
    private int magicPoints = MAGICPOINTS_BASE;

    [SerializeField]
    private int level = 1;
    private int experiencePoints = 0;

    // passive modifiers
    [SerializeField]
    public float walkSpeedMultiplier = 1.0f; // how fast entity should walk
    [SerializeField]
    public float runSpeedMultiplier = 1.3f; // runSpeed for when it's implemented
    [SerializeField]
    private float attackMod = 1.0f; // mod for physical attacks
    [SerializeField]
    private float defenseMod = 1.0f; // physical defense modifier
    //[SerializeField]
    //private float attackSpeed; // how long until player can attack again

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int HealthTotal { get => healthTotal; set => healthTotal = value; }
    public float SpeedMultiplier { get => walkSpeedMultiplier; set => walkSpeedMultiplier = value; }
    public int Level { get => level; set => level = value; }
    public float DefenseMod { get => defenseMod; set => defenseMod = value; }
    public float AttackMod { get => attackMod; set => attackMod = value; }
    public int MagicPoints { get => magicPoints; set => magicPoints = value; }
    public int MagicPointsTotal { get => magicPointsTotal; set => magicPointsTotal = value; }
    public int ExperiencePoints { get => experiencePoints; set => experiencePoints = value; }

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
        this.currentHealth -= damageValue; // subtract modified damage from health
        return damageValue; // return if print damage needed
    }

    public int HealthHeal(int healValue)
    {
        if (healValue < 1)
        {
            healValue = 1;
            Debug.Log("This healing item healed for less than 1 HP\n");
        }
        int healMax = healthTotal - currentHealth;
        if (healValue > healMax)
        {
            healValue = healMax;
        }
        this.currentHealth += healValue;
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

        int healMax = healthTotal - currentHealth;  //check to see if healValue is more than max health
        if (healValue > healMax)
        {
            healValue = healMax;  // set healValue to most health that can be healed
        }

        this.currentHealth += healValue; // heal the player
        return healValue; // return value if heal number needed
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SwordHitbox" && collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
            Debug.Log("Hit");
        }
    }
}