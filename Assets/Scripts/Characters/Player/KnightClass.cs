using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightClass : Player
{
    public HealthBar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Knight has been created!");
        

    }

    void TakeDamage(int damage)
    {
        healthbar.SetHealth(damage);
    }
}
