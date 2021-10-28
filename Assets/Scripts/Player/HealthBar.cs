using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : PlayerStats
{
    public Slider slider;

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

}
