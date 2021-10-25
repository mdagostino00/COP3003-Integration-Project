using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    // base health and magic
    [SerializeField]
    private static int HEALTH_BASE = 100;
    [SerializeField]
    private static int MAGICPOINTS_BASE = 20;

    // player's changing health and magic total
    [SerializeField]
    private int healthTotal = HEALTH_BASE;
    [SerializeField]
    private int magicPointsTotal = MAGICPOINTS_BASE;
    
    // important health bar stuff
    private int health;
    private int magicPoints;
    private int inventorySlots;

    // possible skill point based level-up system
    // private int skillPoints;
    // private int attack;
    // private int defense;
    // private int speed;
    // private int magic;

    // leveling stuff
    [SerializeField]
    private int level = 1;
    private int experiencePoints = 0;

    // passive modifiers
    [SerializeField]
    private float walkSpeedMultiplier = 1.0f; // how fast player should walk
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
    public int MagicPoints { get => magicPoints; set => magicPoints = value; }
    public float SpeedMultiplier { get => walkSpeedMultiplier; set => walkSpeedMultiplier = value; }
    public int HealthTotal { get => healthTotal; set => healthTotal = value; }
    public int MagicPointsTotal { get => magicPointsTotal; set => magicPointsTotal = value; }
    public int Level { get => level; set => level = value; }
    public float DefenseMod { get => defenseMod; set => defenseMod = value; }
    public int ExperiencePoints { get => experiencePoints; set => experiencePoints = value; }

    public int HealthReduce(int damageValue)
    {
        damageValue = (int)(damageValue * DefenseMod); // multiply damage value by player defense
        this.health -= damageValue; // subtract modified damage from health
        return damageValue; // return if print damage needed
    }

    public int HealthHeal(int healValue, char healType)
    {
        switch (healType)
        {
            // choose small, medium, large, or x-large potion types.
            // default to 1 if no type or less than 1
            case 's':
                healValue = (int)(healValue * 0.6);
                break;
            case 'm':
                break;
            case 'l':
                healValue = (int)(healValue * 1.3);
                break;
            case 'x':
                healValue = (int)(healValue * 1.5);
                break;
            default:
                healValue = 1;
                Debug.Log("This healing item doesn't have a healType");
                break;
        }
        if (healValue < 1)
            healValue = 1;
        return healValue; // return value if heal number needed
    }
}
