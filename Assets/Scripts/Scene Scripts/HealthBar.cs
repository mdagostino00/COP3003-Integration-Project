// Robert McNiven
// This is for the healthbar movement, the set health is in the entity class.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Creating a slider object that I can change absed on the current health
    public Slider slider;
    public Player m_player;

    void Update()
    {
        SetHealth(m_player);
    }

    public void SetMaxHealth(Player player)
    {
        slider.maxValue = player.HealthTotal;
        slider.value = player.CurrentHealth;
    }

    public void SetHealth(Player player)
    {
        slider.value = player.CurrentHealth;
    }
    /*
    public void SetMaxHealth()
    {
        slider.maxValue = HEALTH_BASE;
        slider.value = HEALTH_BASE;
    }

    public void SetHealth(int damage)
    {
        HEALTH_BASE -= damage;
        slider.value = HEALTH_BASE;
    }
    */
}
